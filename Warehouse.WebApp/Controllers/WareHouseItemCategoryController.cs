using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouseItemCategory;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    public class WareHouseItemCategoryController : Controller
    {
        #region Fields

        private readonly IWareHouseItemCategoryApiClient _wareHouseItemCategoryApiClient;

        public WareHouseItemCategoryController(IWareHouseItemCategoryApiClient wareHouseItemCategoryApiClient)
        {
            _wareHouseItemCategoryApiClient = wareHouseItemCategoryApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetWareHouseItemCategoryPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _wareHouseItemCategoryApiClient.GetPagings(request);
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
            return ViewComponent("CreateWareHouseItemCategory");
        }

        [HttpPost]
        public async Task<IActionResult> Create(WareHouseItemCategoryModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _wareHouseItemCategoryApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string wareHouseItemCategoryId)
        {
            var result = await _wareHouseItemCategoryApiClient.GetById(wareHouseItemCategoryId);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new WareHouseItemCategoryModel()
                {
                    Code = user.Code,
                    Description = user.Description,
                    ParentId = user.ParentId,
                    Path = user.Path,
                    Name = user.Name,
                    Inactive = user.Inactive,
                    Id = wareHouseItemCategoryId
                };
                return ViewComponent("EditWareHouseItemCategory", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WareHouseItemCategoryModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _wareHouseItemCategoryApiClient.Edit(request.Id, request);
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
            var result = await _wareHouseItemCategoryApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        #endregion
    }
}
