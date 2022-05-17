using Warehouse.Common;
using Warehouse.Data.Entities;
using Warehouse.Model.WareHouseItemUnit;

namespace Warehouse.Service
{
    public interface IWareHouseItemUnitService
    {
        IList<WareHouseItemUnit> GetByWareHouseItemUnitId(GetWareHouseItemUnitPagingRequest ctx);

        Task<RepositoryResponse> Create(WareHouseItemUnitModel model);

        Task<Data.Entities.WareHouseItemUnit> GetById(string? id);

        Task<ApiResult<Data.Entities.WareHouseItemUnit>> GetByIdAsyn(string id);
    }
}