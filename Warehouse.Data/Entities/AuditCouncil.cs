// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Warehouse.Data.Entities
{
    public partial class AuditCouncil
    {
        public string Id { get; set; }
        public string AuditId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }
    }
}