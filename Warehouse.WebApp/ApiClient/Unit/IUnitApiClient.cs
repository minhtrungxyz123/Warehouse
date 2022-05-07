using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Unit;

namespace Warehouse.WebApp.ApiClient
{
    public interface IUnitApiClient
    {
        public Task<bool> Create(UnitModel request);

        public Task<bool> Edit(string id, UnitModel request);

        Task<ApiResult<Pagination<UnitModel>>> GetPagings(GetUnitPagingRequest request);

        Task<ApiResult<UnitModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}
