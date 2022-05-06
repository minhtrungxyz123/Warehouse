using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.WareHouse;

namespace Warehouse.WebApp.ApiClient.WareHouse
{
    public interface IWareHouseApiClient
    {
        public Task<string> Create(WareHouseModel request);

        Task<ApiResult<Pagination<WareHouseModel>>> GetPagings(GetWareHousePagingRequest request);
    }
}
