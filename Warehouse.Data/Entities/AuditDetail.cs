// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using Warehouse.Data.Entities.Base;

namespace Warehouse.Data.Entities
{
    public partial class AuditDetail : BaseEntity
    {
        public string AuditId { get; set; }
        public string ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AuditQuantity { get; set; }
        public string Conclude { get; set; }
    }
}