using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warehouse.Model.WareHouseItemUnit
{
    public class WareHouseItemUnitModel
    {
        public  string? Id { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public  string UnitName { get; set; }
        public int ConvertRate { get; set; }
        public bool? IsPrimary { get; set; }

        public IList<SelectListItem>? AvailableUnit { get; set; }
    }
}