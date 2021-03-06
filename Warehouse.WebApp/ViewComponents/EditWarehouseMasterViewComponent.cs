using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouse;

namespace Warehouse.WebApp.ViewComponents
{
    public class EditWarehouseMasterViewComponent : ViewComponent
    {
        public EditWarehouseMasterViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(WareHouseModel warehouseModel)
        {
            var model = new WareHouseModel();
            model.Id = warehouseModel.Id;
            model.Name = warehouseModel.Name;
            model.Inactive = warehouseModel.Inactive;
            model.Address = warehouseModel.Address;
            model.Path = warehouseModel.Path;
            model.Description = warehouseModel.Description;
            model.Code = warehouseModel.Code;
            model.AvailableCategory = warehouseModel.AvailableCategory;
            return View(model);
        }
    }
}