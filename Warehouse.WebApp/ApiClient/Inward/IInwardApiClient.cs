using Warehouse.Common;
using Warehouse.Model.Inward;

namespace Warehouse.WebApp.ApiClient
{
    public interface IInwardApiClient
    {
        Task<ApiResult<Pagination<InwardGridModel>>> GetPagings(GetInwardPagingRequest request);

        public Task<bool> Create(InwardModel model);

        public Task<bool> Edit(string id, InwardGridModel request);
    }
}