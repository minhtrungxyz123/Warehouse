// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using Warehouse.Data.Entities.Base;

namespace Warehouse.Data.Entities
{
    public partial class WareHouseItem : BaseEntity
    {
        public string? Code { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string VendorId { get; set; }
        public string? VendorName { get; set; }
        public string Country { get; set; }
        public string UnitId { get; set; }
        public bool Inactive { get; set; }
    }
}