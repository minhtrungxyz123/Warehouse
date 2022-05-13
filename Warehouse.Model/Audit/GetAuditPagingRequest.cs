using Warehouse.Common;

namespace Warehouse.Model.Audit
{
    public class GetAuditPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }

        public string? WarehouseId { get; set; }
    }
}