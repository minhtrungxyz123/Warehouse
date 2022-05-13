using Warehouse.Common;

namespace Warehouse.Model.WareHouseItemCategory
{
    public class GetWareHouseItemCategoryPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public  string? CategoryId { get; set; }
    }
}