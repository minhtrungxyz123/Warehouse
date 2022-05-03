using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Unit;

namespace Warehouse.WebApp.ApiClient
{
    public interface IUnitApiClient
    {
        public Task<string> Create(UnitModel request);

        Task<ApiResult<Pagination<UnitModel>>> GetPagings(GetUnitPagingRequest request);
    }
}
