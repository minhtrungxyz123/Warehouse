using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.WebApp.ViewComponents
{
    public class EditWareHouseItemCategoryViewComponent : ViewComponent
    {
        public EditWareHouseItemCategoryViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(WareHouseItemCategoryModel unitModel)
        {
            var model = new WareHouseItemCategoryModel();
            model.Id = unitModel.Id;
            model.Name = unitModel.Name;
            model.Inactive = unitModel.Inactive;
            model.Description = unitModel.Description;
            model.ParentId = unitModel.ParentId;
            model.Code = unitModel.Code;
            model.Path = unitModel.Path;
            return View(model);
        }
    }
}