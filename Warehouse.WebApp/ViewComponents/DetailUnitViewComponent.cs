using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Unit;

namespace Warehouse.WebApp.ViewComponents
{
    public class DetailUnitViewComponent : ViewComponent
    {
        public DetailUnitViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(UnitModel unitModel)
        {
            var model = new UnitModel();
            model.Id = unitModel.Id;
            model.UnitName = unitModel.UnitName;
            model.Inactive = unitModel.Inactive;
            return View(model);
        }
    }
}