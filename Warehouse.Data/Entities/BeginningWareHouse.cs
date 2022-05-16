// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using Warehouse.Data.Entities.Base;

namespace Warehouse.Data.Entities
{
    public partial class BeginningWareHouse : BaseEntity
    {
        public string WareHouseId { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public string? UnitName { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}