// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Warehouse.Data.Entities
{
    public partial class WarehouseBalance
    {
        public string Id { get; set; }
        public string ItemId { get; set; }
        public string WarehouseId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}