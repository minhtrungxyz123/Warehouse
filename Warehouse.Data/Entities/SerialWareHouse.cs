using Warehouse.Data.Entities.Base;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Warehouse.Data.Entities
{
    public partial class SerialWareHouse : BaseEntity
    {
        public string ItemId { get; set; }
        public string Serial { get; set; }
        public string InwardDetailId { get; set; }
        public string OutwardDetailId { get; set; }
        public bool IsOver { get; set; }
    }
}