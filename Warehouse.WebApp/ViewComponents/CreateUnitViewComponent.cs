using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Unit;

namespace Warehouse.WebApp.ViewComponents
{
    public class CreateUnitViewComponent : ViewComponent
    {

        public CreateUnitViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new UnitModel();
            return View(model);
        }
    }
}