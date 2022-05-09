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
            model.CategoryId = unitModel.CategoryId;
            model.Code = unitModel.Code;
            model.VendorId = unitModel.VendorId;
            model.UnitId = unitModel.UnitId;
            model.Country = unitModel.Country;
            return View(model);
        }
    }
}