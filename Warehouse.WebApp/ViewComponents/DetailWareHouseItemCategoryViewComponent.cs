using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.WebApp.ViewComponents
{
    public class DetailWareHouseItemCategoryViewComponent : ViewComponent
    {
        public DetailWareHouseItemCategoryViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(WareHouseItemCategoryModel unitModel)
        {
            var model = new WareHouseItemCategoryModel();
            model.Id = unitModel.Id;
            model.Name = unitModel.Name;
            model.Inactive = unitModel.Inactive;
            model.Description = unitModel.Description;
            model.AvailableCategory = unitModel.AvailableCategory;
            model.Code = unitModel.Code;
            model.Path = unitModel.Path;
            return View(model);
        }
    }
}