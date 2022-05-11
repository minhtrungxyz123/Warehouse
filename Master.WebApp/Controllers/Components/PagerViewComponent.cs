using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;

namespace Master.WebApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PaginationBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}