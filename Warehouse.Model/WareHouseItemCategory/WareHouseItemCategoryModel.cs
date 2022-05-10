using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warehouse.Model.WareHouseItemCategory
{
    public class WareHouseItemCategoryModel
    {
        public string? Id  { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string? Path { get; set; }
        public string Description { get; set; }
        public bool Inactive { get; set; }

        public IList<SelectListItem>? AvailableCategory { get; set; }
    }
}