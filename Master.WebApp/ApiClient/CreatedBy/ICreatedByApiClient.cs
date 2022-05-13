using Warehouse.Common;
using Warehouse.Model.CreatedBy;

namespace Master.WebApp.ApiClient
{
    public interface ICreatedByApiClient
    {
        public Task<bool> Create(CreatedByModel request);

        public Task<bool> Edit(string id, CreatedByModel request);

        Task<ApiResult<Pagination<CreatedByModel>>> GetPagings(GetCreatedByPagingRequest request);

        Task<ApiResult<CreatedByModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}
