using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.WebApp.ViewComponents
{
    public class CreateWareHouseItemCategoryViewComponent : ViewComponent
    {

        public CreateWareHouseItemCategoryViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new WareHouseItemCategoryModel();
            return View(model);
        }
    }
}