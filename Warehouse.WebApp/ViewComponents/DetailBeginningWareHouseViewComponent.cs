using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.BeginningWareHouse;

namespace Warehouse.WebApp.ViewComponents
{
    public class DetailBeginningWareHouseViewComponent : ViewComponent
    {
        public DetailBeginningWareHouseViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(BeginningWareHouseModel unitModel)
        {
            var model = new BeginningWareHouseModel();
            model.Id = unitModel.Id;
            model.AvailableItem = unitModel.AvailableItem;
            model.AvailableUnit = unitModel.AvailableUnit;
            model.AvailableWarehouse = unitModel.AvailableWarehouse;
            model.Quantity = unitModel.Quantity;
            return View(model);
        }
    }
}