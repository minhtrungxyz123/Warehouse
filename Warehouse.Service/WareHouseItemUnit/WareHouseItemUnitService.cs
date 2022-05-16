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
    }
}