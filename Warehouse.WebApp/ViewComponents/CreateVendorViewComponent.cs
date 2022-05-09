using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Vendor;

namespace Warehouse.WebApp.ViewComponents
{
    public class CreateVendorViewComponent : ViewComponent
    {
        public CreateVendorViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new VendorModel();
            return View(model);
        }
    }
}