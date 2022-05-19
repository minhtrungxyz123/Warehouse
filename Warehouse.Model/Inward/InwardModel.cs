using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Model.InwardDetail;

namespace Warehouse.Model.Inward
{
    public class InwardModel
    {
        public  string? Id  { get; set; }
        public string? VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string WareHouseId { get; set; }

        public  string AccObjectId { get; set; }
        public string Deliver { get; set; }
        public string Receiver { get; set; }
        public string VendorId { get; set; }
        public string Reason { get; set; }
        public string? ReasonDescription { get; set; }
        public string? Description { get; set; }
        public string? Reference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string DeliverPhone { get; set; }
        public string DeliverAddress { get; set; }
        public string DeliverDepartment { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverDepartment { get; set; }
        public string Voucher { get; set; }

        public IList<SelectListItem>? AvailableVendor { get; set; }
        public IList<SelectListItem>? AvailableWarehouse { get; set; }
        public IList<InwardDetailModel>? InwardDetails { get; set; }
    }
}