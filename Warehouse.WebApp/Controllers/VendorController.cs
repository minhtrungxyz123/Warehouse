using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.Vendor;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VendorController : Controller
    {
        #region Fields

        private readonly IVendorApiClient _vendorApiClient;

        public VendorController(IVendorApiClient vendorApiClient)
        {
            _vendorApiClient = vendorApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetVendorPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _vendorApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        #endregion List

        #region Method

        [HttpGet]
        public ActionResult Create()
        {
            return ViewComponent("CreateVendor");
        }

        [HttpPost]
        public async Task<IActionResult> Create(VendorModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.Code = ExtensionFull.GetVoucherCode("NCC");

            var result = await _vendorApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string vendorId)
        {
            var result = await _vendorApiClient.GetById(vendorId);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new VendorModel()
                {
                    Name = user.Name,
                    Inactive = user.Inactive,
                    Id = vendorId,
                    Address = user.Address,
                    Code = user.Code,
                    ContactPerson = user.ContactPerson,
                    Email = user.Email,
                    Phone=user.Phone,
                };
                return ViewComponent("EditVendor", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VendorModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _vendorApiClient.Edit(request.Id, request);
            if (result)
            {
                TempData["result"] = "Sửa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _vendorApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string vendorId)
        {
            var result = await _vendorApiClient.GetById(vendorId);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new VendorModel()
                {
                    Name = user.Name,
                    Inactive = user.Inactive,
                    Id = vendorId,
                    Address = user.Address,
                    Code = user.Code,
                    ContactPerson = user.ContactPerson,
                    Email = user.Email,
                    Phone = user.Phone,
                };
                return ViewComponent("DetailVendor", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion
    }
}
