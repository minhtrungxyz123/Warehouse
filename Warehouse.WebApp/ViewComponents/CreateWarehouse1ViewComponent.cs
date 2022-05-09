using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouse;

namespace Warehouse.WebApp.ViewComponents
{
    public class CreateWarehouse1ViewComponent : ViewComponent
    {

        public CreateWarehouse1ViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new WareHouseModel();
            return View(model);
        }
    }
}