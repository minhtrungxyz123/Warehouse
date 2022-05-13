using Warehouse.Common;

namespace Warehouse.Model.CreatedBy
{
    public class GetCreatedByPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}