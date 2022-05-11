using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.CreatedBy;

namespace Master.WebApp.ViewComponents
{
    public class CreateCreatedByViewComponent : ViewComponent
    {
        public CreateCreatedByViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new CreatedByModel();
            return View(model);
        }
    }
}