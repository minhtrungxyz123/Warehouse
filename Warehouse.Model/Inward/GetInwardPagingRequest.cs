using Warehouse.Common;

namespace Warehouse.Model.Inward
{
    public class GetInwardPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }

        public string? WarehouseId { get; set; }

        public string? VendorId { get; set; }
    }
}