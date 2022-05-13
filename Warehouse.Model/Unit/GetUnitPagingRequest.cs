using Warehouse.Common;

namespace Warehouse.Model.Unit
{
    public class GetUnitPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}