using Warehouse.Common;

namespace Warehouse.Model.WareHouseItem
{
    public class GetWareHouseItemPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public  string? CategoryId { get; set; }
        public  string? VendorId { get; set; }
        public  string? UnitId { get; set; }
    }
}