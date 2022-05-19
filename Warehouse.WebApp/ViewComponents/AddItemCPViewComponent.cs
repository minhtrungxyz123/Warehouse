using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Inward;
using Warehouse.Model.InwardDetail;

namespace Warehouse.WebApp.ViewComponents
{
    public class AddItemCPViewComponent : ViewComponent
    {
        public AddItemCPViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(InwardDetailModel beginning)
        {
            return View(beginning);
        }
    }
}