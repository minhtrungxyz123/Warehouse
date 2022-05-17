using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Data.Entities;
using Warehouse.Model.WareHouseItemUnit;

namespace Warehouse.Service
{
    public class WareHouseItemUnitService : IWareHouseItemUnitService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public WareHouseItemUnitService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Data.Entities.WareHouseItemUnit>> GetByIdAsyn(string id)
        {
            var item = await _context.WareHouseItemUnits
                            .OrderByDescending(p => p.UnitId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var userViewModel = new Data.Entities.WareHouseItemUnit()
            {
                ConvertRate = item.ConvertRate,
                Id = item.Id,
                UnitId = item.UnitId,
                UnitName = item.UnitName,
                ItemId = item.ItemId,
                IsPrimary = item.IsPrimary,
            };
            return new ApiSuccessResult<Data.Entities.WareHouseItemUnit>(userViewModel);
        }

        public async Task<Data.Entities.WareHouseItemUnit?> GetById(string? id)
        {
            var item = await _context.WareHouseItemUnits
                            .OrderByDescending(p => p.UnitId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public virtual IList<WareHouseItemUnit> GetByWareHouseItemUnitId(GetWareHouseItemUnitPagingRequest ctx)
        {
            if (string.IsNullOrWhiteSpace(ctx.ItemId))
                throw new ArgumentNullException(nameof(ctx.ItemId));

            var query =
                from x in _context.WareHouseItemUnits.AsQueryable()
                where x.ItemId == ctx.ItemId
                select x;

            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(WareHouseItemUnitModel model)
        {
            Data.Entities.WareHouseItemUnit item = new Data.Entities.WareHouseItemUnit()
            {
                ConvertRate = model.ConvertRate,
                IsPrimary = model.IsPrimary,
                ItemId = model.ItemId,
                UnitId = model.UnitId,
                UnitName = model.UnitName,
            };
            item.Id = Guid.NewGuid().ToString();

            _context.WareHouseItemUnits.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        #endregion
    }
}