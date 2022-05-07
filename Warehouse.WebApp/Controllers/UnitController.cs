using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Unit;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    public class UnitController : Controller
    {
        #region Fields
        private readonly IUnitApiClient _unitApiClient;

        public UnitController(IUnitApiClient unitApiClient)
        {
            _unitApiClient = unitApiClient;
        }

        #endregion

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetUnitPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _unitApiClient.GetPagings(request);
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
        public  ActionResult Create()
        {
            return ViewComponent("CreateUnit");
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnitModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

                var result = await _unitApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới đơn vị tính thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới đơn vị tính thất bại");
            return View(request);

        }

        #endregion
    }
}