using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Data.EF;
using Warehouse.Model.WareHouseItem;

namespace Warehouse.Service
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

        public async Task<ApiResult<Data.Entities.WareHouseItem>> GetByIdAsyn(string id)
        {
            var item = await _context.WareHouseItems
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var userViewModel = new Data.Entities.WareHouseItem()
            {
                Name = item.Name,
                Code = item.Code,
                Description = item.Description,
                CategoryId = item.CategoryId,
                Country = item.CategoryId,
                Inactive = item.Inactive,
                UnitId = item.UnitId,
                VendorId = item.VendorId,
                VendorName=item.VendorName,
                Id = item.Id
            };
            return new ApiSuccessResult<Data.Entities.WareHouseItem>(userViewModel);
        }

        public async Task<IEnumerable<Data.Entities.WareHouseItem>> GetAll()
        {
            return await _context.WareHouseItems
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<WareHouseItemModel>>> GetAllPaging(GetWareHouseItemPagingRequest request)
        {
            var query = from pr in _context.WareHouseItems
                        join c in _context.Vendors on pr.VendorId equals c.Id into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.WareHouseItemCategories on pr.CategoryId equals w.Id into wt
                        from tw in wt.DefaultIfEmpty()
                        join i in _context.Units on pr.UnitId equals i.Id into it
                        from ti in it.DefaultIfEmpty()
                        select new { pr, tp, tw, ti };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.tp.Name.Contains(request.Keyword)
                || x.tp.Email.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.CategoryId))
            {
                query = query.Where(x => x.pr.CategoryId == request.CategoryId);
            }

            if (!string.IsNullOrEmpty(request.VendorId))
            {
                query = query.Where(x => x.pr.VendorId == request.VendorId);
            }

            if (!string.IsNullOrEmpty(request.UnitId))
            {
                query = query.Where(x => x.pr.UnitId == request.UnitId);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new WareHouseItemModel()
                {
                    Id = u.pr.Id,
                    Description = u.pr.Description,
                    Name = u.tp.Name,
                    CategoryId = u.tw.Name,
                    VendorId = u.tp.Name,
                    UnitId = u.ti.UnitName,
                    Code = u.pr.Code,
                    Country=u.pr.Country,
                    Inactive=u.pr.Inactive,
                    VendorName=u.pr.VendorName,
                })
                .ToListAsync();

            var pagination = new Pagination<WareHouseItemModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return new ApiSuccessResult<Pagination<WareHouseItemModel>>(pagination);
        }

        public async Task<Data.Entities.WareHouseItem?> GetById(string? id)
        {
            var item = await _context.WareHouseItems
                            .OrderByDescending(p => p.Name)
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
                Name = model.Name,
                Code = model.Code,
                Description = model.Description,
                Country = model.Country,
                CategoryId = model.CategoryId,
                Inactive = model.Inactive,
                UnitId = model.UnitId,
                VendorId = model.VendorId,
                VendorName = model.VendorName,
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
            item.Name = model.Name;
            item.Code = model.Code;
            item.Description = model.Description;
            item.CategoryId = model.CategoryId;
            item.Country = model.Country;
            item.Inactive = model.Inactive;
            item.UnitId = model.UnitId;
            item.VendorId = model.VendorId;
            item.VendorName = model.VendorName;

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