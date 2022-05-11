using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouse;

namespace Warehouse.WebApp.ViewComponents
{
    public class DetailWarehouseMasterViewComponent : ViewComponent
    {
        public DetailWarehouseMasterViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(WareHouseModel unitModel)
        {
            var model = new WareHouseModel();
            model.Id = unitModel.Id;
            model.Name = unitModel.Name;
            model.Inactive = unitModel.Inactive;
            model.Description = unitModel.Description;
            model.AvailableCategory = unitModel.AvailableCategory;
            model.Code = unitModel.Code;
            model.Path = unitModel.Path;
            model.Address = unitModel.Address;
            return View(model);
        }
    }
}