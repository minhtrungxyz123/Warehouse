using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Vendor;

namespace Warehouse.WebApp.ViewComponents
{
    public class EditVendorViewComponent : ViewComponent
    {
        public EditVendorViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(VendorModel unitModel)
        {
            var model = new VendorModel();
            model.Id = unitModel.Id;
            model.Name = unitModel.Name;
            model.Inactive = unitModel.Inactive;
            model.Address = unitModel.Address;
            model.Phone = unitModel.Phone;
            model.ContactPerson = unitModel.ContactPerson;
            model.Code = unitModel.Code;
            model.Email = unitModel.Email;
            return View(model);
        }
    }
}