// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Warehouse.Data.Entities
{
    public partial class AuditDetailSerial
    {
        public string Id { get; set; }
        public string ItemId { get; set; }
        public string Serial { get; set; }
        public string AuditDetailId { get; set; }
    }
}