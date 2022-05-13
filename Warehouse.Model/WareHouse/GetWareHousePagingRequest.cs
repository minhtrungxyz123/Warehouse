using Warehouse.Common;

namespace Warehouse.Model.WareHouse
{
    public class GetWareHousePagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public  string? ParentId { get; set; }
    }
}