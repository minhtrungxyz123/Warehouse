using Warehouse.Common;

namespace Warehouse.Model.BeginningWareHouse
{
    public class GetBeginningWareHousePagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public  string? ItemId { get; set; }
        public  string? WarehouseId  { get; set; }
    }
}