using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warehouse.Model.BeginningWareHouse
{
    public class BeginningWareHouseModel
    {
        public string? Id { get; set; }
        public string WareHouseId { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public string? UnitName { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public IList<SelectListItem>? AvailableUnit { get; set; }

        public IList<SelectListItem>? AvailableItem { get; set; }

        public IList<SelectListItem>? AvailableWarehouse { get; set; }
    }
}