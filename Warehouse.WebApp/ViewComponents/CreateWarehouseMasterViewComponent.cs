using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouse;

namespace Warehouse.WebApp.ViewComponents
{
    public class CreateWarehouseMasterViewComponent : ViewComponent
    {

        public CreateWarehouseMasterViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(WareHouseModel wareHouse)
        {
            return View(wareHouse);
        }
    }
}