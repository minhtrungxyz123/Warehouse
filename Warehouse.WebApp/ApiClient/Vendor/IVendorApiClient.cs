using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Vendor;

namespace Warehouse.WebApp.ApiClient
{
    public interface IVendorApiClient
    {
        public Task<string> Create(VendorModel request);

        Task<ApiResult<Pagination<VendorModel>>> GetPagings(GetVendorPagingRequest request);
    }
}
