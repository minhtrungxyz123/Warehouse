using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.Unit;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UnitController : Controller
    {
        #region Fields

        private readonly IUnitApiClient _unitApiClient;

        public UnitController(IUnitApiClient unitApiClient)
        {
            _unitApiClient = unitApiClient;
        }

        #endregion Fields

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

        #endregion List

        #region Method

        [HttpGet]
        public ActionResult Create()
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
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string unitId)
        {
            var result = await _unitApiClient.GetById(unitId);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UnitModel()
                {
                    UnitName = user.UnitName,
                    Inactive = user.Inactive,
                    Id = unitId
                };
                return ViewComponent("EditUnit", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UnitModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _unitApiClient.Edit(request.Id, request);
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
            var result = await _unitApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string unitId)
        {
            var result = await _unitApiClient.GetById(unitId);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UnitModel()
                {
                    UnitName = user.UnitName,
                    Inactive = user.Inactive,
                    Id = unitId
                };
                return ViewComponent("DetailUnit", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion
    }
}