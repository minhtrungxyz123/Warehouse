using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.CreatedBy;

namespace Master.WebApp.ViewComponents
{
    [Authorize]
    public class InfoAccountViewComponent : ViewComponent
    {
        private readonly ILogger<InfoAccountViewComponent> _logger;

        public InfoAccountViewComponent(ILogger<InfoAccountViewComponent> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _logger.LogInformation("Get SetCookie ");
            var model = new CreatedByModel();

            var claims = HttpContext.User.Claims;
            var userName = claims.FirstOrDefault(c => c.Type == "AccountName").Value;
            var userId = claims.FirstOrDefault(c => c.Type == "Email").Value;
            model.AccountName = userName;
            model.Email = userId;
            _logger.LogInformation("End get SetCookie");
            return View(model);
        }
    }
}