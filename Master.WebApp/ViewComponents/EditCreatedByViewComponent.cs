using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.CreatedBy;

namespace Master.WebApp.ViewComponents
{
    public class EditCreatedByViewComponent : ViewComponent
    {
        public EditCreatedByViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(CreatedByModel unitModel)
        {
            var model = new CreatedByModel();
            model.Id = unitModel.Id;
            model.AccountName = unitModel.AccountName;
            model.FullName = unitModel.FullName;
            model.Avarta=unitModel.Avarta;
            model.DateCreate=unitModel.DateCreate;
            model.DateRegister=unitModel.DateRegister;
            model.Role=unitModel.Role;
            model.Email=unitModel.Email;
            model.Password=unitModel.Password;
            return View(model);
        }
    }
}