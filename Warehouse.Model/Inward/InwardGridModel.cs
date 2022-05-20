using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warehouse.Model.Inward
{
    public class InwardGridModel
    {
        public  string? Id { get; set; }
        public string VoucherCode { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string WareHouseId { get; set; }
        public string Deliver { get; set; }
        public string Receiver { get; set; }
        public string VendorId { get; set; }
        public string Reason { get; set; }
        public string ReasonDescription { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DeliverPhone { get; set; }
        public string DeliverAddress { get; set; }
        public string DeliverDepartment { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverDepartment { get; set; }
        public string Voucher { get; set; }
        public string InwardId { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public decimal? Uiquantity { get; set; }
        public decimal? Uiprice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? StationId { get; set; }
        public string? StationName { get; set; }
        public string? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? AccountMore { get; set; }
        public string? AccountYes { get; set; }
        public string? Status { get; set; }
        public IList<SelectListItem>? AvailableUnit { get; set; }
        public IList<SelectListItem>? AvailableItem { get; set; }
        public IList<SelectListItem>? AvailableWarehouse { get; set; }
        public IList<SelectListItem>? AvailableVendor { get; set; }
        public IList<SelectListItem>? AvailableReason { get; set; }
        public IList<SelectListItem>? AvailableCreatedBy { get; set; }

        public  string AccObjectId { get; set; }
    }
}