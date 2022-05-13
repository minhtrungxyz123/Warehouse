using Warehouse.Common;

namespace Warehouse.Model.Vendor
{
    public class GetVendorPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}