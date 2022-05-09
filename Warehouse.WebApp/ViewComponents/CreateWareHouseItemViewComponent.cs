using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouseItem;

namespace Warehouse.WebApp.ViewComponents
{
    public class CreateWareHouseItemViewComponent : ViewComponent
    {
        public CreateWareHouseItemViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(WareHouseItemModel wareHouseItemModel)
        {
            return View(wareHouseItemModel);
        }
    }
}