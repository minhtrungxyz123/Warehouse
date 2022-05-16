using Warehouse.Data.Entities;
using Warehouse.Model.WareHouseItemUnit;

namespace Warehouse.Service
{
    public interface IWareHouseItemUnitService
    {
        IList<WareHouseItemUnit> GetByWareHouseItemUnitId(GetWareHouseItemUnitPagingRequest ctx);
    }
}