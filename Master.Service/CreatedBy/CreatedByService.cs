using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Data.EF;
using Warehouse.Data.Entities;
using Warehouse.Data.Repositories;
using Warehouse.Model.CreatedBy;

namespace Master.Service
{
    public class CreatedByService : ICreatedByService
    {
        #region Fields

        private readonly WarehouseDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IRepositoryEF<CreatedBy> _createdByrepositoryEF;
        private readonly IConfiguration _config;

        public CreatedBy User => GetUser();

        public CreatedByService(WarehouseDbContext context, IHttpContextAccessor httpContext, IRepositoryEF<CreatedBy> repositoryEF
            , IConfiguration config)
        {
            _context = context;
            _httpContext = httpContext;
            _createdByrepositoryEF = repositoryEF;
            _config = config;
        }

        #endregion Fields

        #region List

        private CreatedBy GetUser()
        {
            var id = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (id is null)
                return null;
            var res = _context.CreatedBies.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id.Value));
            res.Password = "";
            return res;
        }

        public async Task<ApiResult<CreatedBy>> GetByIdAsyn(string id)
        {
            var item = await _context.CreatedBies
                            .OrderByDescending(p => p.AccountName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var userViewModel = new CreatedBy()
            {
                AccountName = item.AccountName,
                FullName = item.FullName,
                Id = item.Id,
                Avarta = item.Avarta,
                DateCreate = item.DateCreate,
                DateRegister = item.DateRegister,
                Email = item.Email,
                Password = item.Password,
                Role = item.Role
            };
            return new ApiSuccessResult<CreatedBy>(userViewModel);
        }

        public async Task<IEnumerable<CreatedBy>> GetAll()
        {
            var res = await _context.CreatedBies
                            .OrderByDescending(p => p.AccountName)
                            .ToListAsync();
            return res;
        }

        public async Task<ApiResult<Pagination<CreatedBy>>> GetAllPaging(GetCreatedByPagingRequest request)
        {
            var query = _context.CreatedBies.AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.AccountName.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CreatedBy()
                {
                    AccountName = x.AccountName,
                    Avarta = x.Avarta,
                    Id = x.Id,
                    Role = x.Role,
                    Password = x.Password,
                    Email = x.Email,
                    DateRegister = x.DateRegister,
                    DateCreate = x.DateCreate,
                    FullName = x.FullName,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new Pagination<CreatedBy>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<CreatedBy>>(pagedResult);
        }

        public async Task<CreatedBy?> GetById(string? id)
        {
            var item = await _context.CreatedBies
                            .OrderByDescending(p => p.AccountName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<ApiResult<string>> Authencate(CreatedByModel request)
        {
            var user = await _context.CreatedBies.FirstOrDefaultAsync(c => c.AccountName.Equals(request.AccountName));
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            if (ValidateAdmin(request.AccountName, request.Password))
            {
                var users = _createdByrepositoryEF.Get(a => a.AccountName == request.AccountName).SingleOrDefault();
                if (users != null)
                {
                    var claims = new[]
                    {
                                new Claim("AccountName", users.AccountName),
                                new Claim("Email", users.Email),
                                new Claim("Id", users.Id),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Issuer"],
                        claims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: creds);

                    return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }
            return new ApiErrorResult<string>("Lỗi đăng nhập!");
        }

        public async Task<RepositoryResponse> Create(CreatedByModel model)
        {
            CreatedBy item = new CreatedBy()
            {
                AccountName = model.AccountName,
                FullName = model.FullName,
                Avarta = model.Avarta,
                DateCreate = model.DateCreate,
                DateRegister = model.DateRegister,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
            };
            item.Id = Guid.NewGuid().ToString();

            _context.CreatedBies.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, CreatedByModel model)
        {
            var item = await _context.CreatedBies.FindAsync(id);
            item.AccountName = model.AccountName;
            item.FullName = model.FullName;
            item.Avarta = model.Avarta;
            item.DateCreate = model.DateCreate;
            item.DateRegister = model.DateRegister;
            item.Email = model.Email;
            item.Password = model.Password;
            item.Role = model.Role;

            _context.CreatedBies.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.CreatedBies.FindAsync(id);

            _context.CreatedBies.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method

        #region Unitiels

        public bool ValidateAdmin(string username, string password)
        {
            var admin = _createdByrepositoryEF.Get(a => a.AccountName.Equals(username)).SingleOrDefault();
            return admin != null && new PasswordHasher<CreatedBy>().VerifyHashedPassword(new CreatedBy(), admin.Password, password) == PasswordVerificationResult.Success;
        }

        #endregion Unitiels
    }
}