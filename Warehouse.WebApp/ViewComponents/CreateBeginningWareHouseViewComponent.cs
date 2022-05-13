using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.BeginningWareHouse;

namespace Warehouse.WebApp.ViewComponents
{
    public class CreateBeginningWareHouseViewComponent : ViewComponent
    {

        public CreateBeginningWareHouseViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(BeginningWareHouseModel beginning)
        {
            return View(beginning);
        }
    }
}