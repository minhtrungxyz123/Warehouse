using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Vendor;

namespace Warehouse.Service
{
    public class VendorService : IVendorService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public VendorService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Data.Entities.Vendor>> GetByIdAsyn(string id)
        {
            var item = await _context.Vendors
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var userViewModel = new Data.Entities.Vendor()
            {
                Name = item.Name,
                Address = item.Address,
                Code = item.Code,
                ContactPerson = item.ContactPerson,
                Email = item.Email,
                Phone = item.Phone,
                Inactive = item.Inactive,
                Id = item.Id
            };
            return new ApiSuccessResult<Data.Entities.Vendor>(userViewModel);
        }

        public async Task<IEnumerable<Data.Entities.Vendor>> GetAll()
        {
            return await _context.Vendors
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Data.Entities.Vendor>>> GetAllPaging(GetVendorPagingRequest request)
        {
            var query = _context.Vendors.AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new Data.Entities.Vendor()
                {
                    Name = x.Name,
                    ContactPerson = x.ContactPerson,
                    Phone = x.Phone,
                    Email = x.Email,
                    Code = x.Code,
                    Address = x.Address,
                    Inactive = x.Inactive,
                    Id = x.Id
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new Pagination<Data.Entities.Vendor>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Data.Entities.Vendor>>(pagedResult);
        }

        public async Task<Data.Entities.Vendor?> GetById(string? id)
        {
            var item = await _context.Vendors
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public IList<Data.Entities.Vendor> GetMvcListItems(bool showHidden = true)
        {
            var query = from p in _context.Vendors.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Inactive select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(VendorModel model)
        {
            Data.Entities.Vendor item = new Data.Entities.Vendor()
            {
                Name = model.Name,
                Phone = model.Phone,
                Address = model.Address,
                Code = model.Code,
                Email = model.Email,
                ContactPerson = model.Phone,
                Inactive = model.Inactive
            };
            item.Id = Guid.NewGuid().ToString();

            _context.Vendors.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, VendorModel model)
        {
            var item = await _context.Vendors.FindAsync(id);
            item.Name = model.Name;
            item.Phone = model.Phone;
            item.Address = model.Address;
            item.Code = model.Code;
            item.Email = model.Email;
            item.ContactPerson = model.Phone;
            item.Inactive = model.Inactive;

            _context.Vendors.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Vendors.FindAsync(id);

            _context.Vendors.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}