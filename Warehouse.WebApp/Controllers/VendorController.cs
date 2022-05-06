using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Vendor;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    public class VendorController : Controller
    {
        #region Fields
        private readonly IVendorApiClient _vendorApiClient;

        public VendorController(IVendorApiClient vendorApiClient)
        {
            _vendorApiClient = vendorApiClient;
        }

        #endregion

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 2)
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

        #endregion

        #region Method

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VendorModel request)
        {
            try
            {
                var response = await _vendorApiClient.Create(request);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}
