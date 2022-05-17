using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouseItem;

namespace Warehouse.WebApp.ViewComponents
{
    public class DetailWareHouseItemViewComponent : ViewComponent
    {
        public DetailWareHouseItemViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(WareHouseItemModel unitModel)
        {
            var model = new WareHouseItemModel();
            model.Id = unitModel.Id;
            model.Name = unitModel.Name;
            model.Inactive = unitModel.Inactive;
            model.Description = unitModel.Description;
            model.AvailableCategory = unitModel.AvailableCategory;
            model.Code = unitModel.Code;
            model.AvailableVendor = unitModel.AvailableVendor;
            model.AvailableUnit = unitModel.AvailableUnit;
            model.Country = unitModel.Country;
            model.ConvertRate = unitModel.ConvertRate;
            model.IsPrimary = unitModel.IsPrimary;
            return View(model);
        }
    }
}