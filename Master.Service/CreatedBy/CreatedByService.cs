using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Data.Entities;
using Warehouse.Model.CreatedBy;

namespace Master.Service
{
    public class CreatedByService : ICreatedByService
    {
        #region Fields

        private readonly WarehouseDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CreatedBy User => GetUser();

        public CreatedByService(WarehouseDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
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
            var res= await _context.CreatedBies
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
    }
}