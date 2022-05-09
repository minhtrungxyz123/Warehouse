using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouseItem;

namespace Warehouse.WebApp.ViewComponents
{
    public class EditWareHouseItemViewComponent : ViewComponent
    {
        public EditWareHouseItemViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(WareHouseItemModel unitModel)
        {
            var model = new WareHouseItemModel();
            model.Id = unitModel.Id;
            model.Name = unitModel.Name;
            model.Inactive = unitModel.Inactive;
            model.Description = unitModel.Description;
            model.Country = unitModel.Country;
            model.Code = unitModel.Code;
            model.CategoryId = unitModel.CategoryId;
            model.VendorId = unitModel.VendorId;
            model.VendorName = unitModel.VendorName;
            model.UnitId = unitModel.UnitId;
            return View(model);
        }
    }
}