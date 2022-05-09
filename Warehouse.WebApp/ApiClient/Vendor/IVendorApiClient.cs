using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Vendor;

namespace Warehouse.WebApp.ApiClient
{
    public interface IVendorApiClient
    {
        public Task<bool> Create(VendorModel request);

        public Task<bool> Edit(string id, VendorModel request);

        Task<ApiResult<Pagination<VendorModel>>> GetPagings(GetVendorPagingRequest request);

        Task<ApiResult<VendorModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}
