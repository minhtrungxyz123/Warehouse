using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warehouse.Model.WareHouseItem
{
    public class WareHouseItemModel
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string VendorId { get; set; }
        public string? VendorName { get; set; }
        public string Country { get; set; }
        public string UnitId { get; set; }
        public bool Inactive { get; set; }
        public IList<SelectListItem>? AvailableUnit { get; set; }

        public IList<SelectListItem>? AvailableVendor { get; set; }

        public IList<SelectListItem>? AvailableCategory { get; set; }

        public string? ItemId { get; set; }
        public string? UnitName { get; set; }
        public int? ConvertRate { get; set; }
        public bool? IsPrimary { get; set; }

        public  string? Note { get; set; }
    }
}