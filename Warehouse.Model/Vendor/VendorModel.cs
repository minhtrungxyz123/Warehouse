namespace Warehouse.Model.Vendor
{
    public class VendorModel
    {
        public  string? Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public bool Inactive { get; set; }
    }
}