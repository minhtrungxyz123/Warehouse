using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Inward;

namespace Warehouse.WebApp.ViewComponents
{
    public class AddItemCPViewComponent : ViewComponent
    {
        public AddItemCPViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(InwardGridModel beginning)
        {
            return View(beginning);
        }
    }
}