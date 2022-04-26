// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using Warehouse.Data.Entities.Base;

namespace Warehouse.Data.Entities
{
    public partial class OutwardDetail : BaseEntity
    {
        public string OutwardId { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public decimal Uiquantity { get; set; }
        public decimal Uiprice { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string StationId { get; set; }
        public string StationName { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AccountMore { get; set; }
        public string AccountYes { get; set; }
        public string Status { get; set; }
    }
}