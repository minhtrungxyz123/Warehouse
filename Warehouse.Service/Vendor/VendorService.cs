using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Vendor;

namespace Warehouse.Service.Vendor
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

        public async Task<IEnumerable<Data.Entities.Vendor>> GetAll()
        {
            return await _context.Vendors
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<Pagination<Data.Entities.Vendor>> GetAllPaging(string? search, int pageIndex, int pageSize)
        {
            var query = _context.Vendors.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search)
                || x.Name.Contains(search));
            }
            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagination = new Pagination<Data.Entities.Vendor>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            return pagination;
        }

        public async Task<Data.Entities.Vendor?> GetById(string? id)
        {
            var item = await _context.Vendors
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(VendorModel model)
        {
            Data.Entities.Vendor item = new Data.Entities.Vendor()
            {
                Name = model.Name,
                Address = model.Address,
                Code = model.Code,
                ContactPerson = model.ContactPerson,
                Email = model.Email,
                Phone = model.Phone,
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
            item.Address = model.Address;
            item.Code = model.Code;
            item.ContactPerson = model.ContactPerson;
            item.Email = model.Email;
            item.Phone = model.Phone;
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