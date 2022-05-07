using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Unit;

namespace Warehouse.WebApp.ApiClient
{
    public interface IUnitApiClient
    {
        public Task<bool> Create(UnitModel request);

        public Task<bool> Edit(UnitModel request, string id);

        Task<ApiResult<Pagination<UnitModel>>> GetPagings(GetUnitPagingRequest request);
    }
}
