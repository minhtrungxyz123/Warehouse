using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.WareHouseItem;

namespace Warehouse.Service.WareHouseItem
{
    public class WareHouseItemService : IWareHouseItemService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public WareHouseItemService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<IEnumerable<Data.Entities.WareHouseItem>> GetAll()
        {
            return await _context.WareHouseItems
                            .OrderByDescending(p => p.Description)
                            .ToListAsync();
        }

        public async Task<Pagination<Data.Entities.WareHouseItem>> GetAllPaging(string? search, int pageIndex, int pageSize)
        {
            var query = _context.WareHouseItems.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Description.Contains(search)
                || x.Description.Contains(search));
            }
            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagination = new Pagination<Data.Entities.WareHouseItem>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            return pagination;
        }

        public async Task<Data.Entities.WareHouseItem?> GetById(string? id)
        {
            var item = await _context.WareHouseItems
                            .OrderByDescending(p => p.Description)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(WareHouseItemModel model)
        {
            Data.Entities.WareHouseItem item = new Data.Entities.WareHouseItem()
            {
                Code = model.Code,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Country = model.Country,
                Name = model.Name,
                UnitId = model.UnitId,
                VendorId = model.VendorId,
                VendorName = model.VendorName,
                Inactive = model.Inactive
            };
            item.Id = Guid.NewGuid().ToString();

            _context.WareHouseItems.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, WareHouseItemModel model)
        {
            var item = await _context.WareHouseItems.FindAsync(id);
            item.Country = model.Country;
            item.Name = model.Name;
            item.UnitId = model.UnitId;
            item.VendorId = model.VendorId;
            item.VendorName = model.VendorName;
            item.Inactive = model.Inactive;

            _context.WareHouseItems.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.WareHouseItems.FindAsync(id);

            _context.WareHouseItems.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}