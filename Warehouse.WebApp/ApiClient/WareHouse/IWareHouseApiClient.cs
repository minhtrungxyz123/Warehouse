using Warehouse.Common;
using Warehouse.Model.WareHouse;

namespace Warehouse.WebApp.ApiClient
{
    public interface IWareHouseApiClient
    {
        public Task<bool> Create(WareHouseModel request);

        public Task<bool> Edit(string id, WareHouseModel request);

        Task<ApiResult<Pagination<WareHouseModel>>> GetPagings(GetWareHousePagingRequest request);

        Task<ApiResult<WareHouseModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<IList<WareHouseModel>> GetAvailableList(bool showHidden = true);
    }
}
