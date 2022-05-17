using Warehouse.Common;
using Warehouse.Model.WareHouseItemUnit;

namespace Warehouse.WebApp.ApiClient
{
    public interface IWareHouseItemUnitApiClient
    {
        Task<ApiResult<WareHouseItemUnitModel>> GetById(string id);
    }
}
