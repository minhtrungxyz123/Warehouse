using Warehouse.Common;

namespace Warehouse.Model.WareHouseItemUnit
{
    public class GetWareHouseItemUnitPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public string? ItemId { get; set; }
    }
}