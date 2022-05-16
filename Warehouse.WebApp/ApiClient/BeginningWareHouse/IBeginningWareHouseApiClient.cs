using Warehouse.Common;
using Warehouse.Model.BeginningWareHouse;
using Warehouse.Model.Unit;
using Warehouse.Model.WareHouseItem;
using Warehouse.Model.WareHouseItemUnit;

namespace Warehouse.WebApp.ApiClient
{
    public interface IBeginningWareHouseApiClient
    {
        public Task<bool> Create(BeginningWareHouseModel request);

        public Task<bool> Edit(string id, BeginningWareHouseModel request);

        Task<ApiResult<Pagination<BeginningWareHouseModel>>> GetPagings(GetBeginningWareHousePagingRequest request);

        Task<ApiResult<BeginningWareHouseModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<List<WareHouseItemUnitModel>> GetByWareHouseItemUnitId(string getUnitItem);

        Task<WareHouseItemModel> GetByWareHouseItemId(string id);

    }
}
