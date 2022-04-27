namespace Warehouse.Model.Audit
{
    public class AuditGridModel
    {
        public  string Id { get; set; }
        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string WareHouseId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string AuditId { get; set; }
        public string ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AuditQuantity { get; set; }
        public string Conclude { get; set; }
    }
}