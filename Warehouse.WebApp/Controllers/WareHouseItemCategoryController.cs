using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Common;
using Warehouse.Model.WareHouseItemCategory;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> Index(string keyword, string categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetWareHouseItemCategoryPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = categoryId
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
        public async Task<IActionResult> Create()
        {
            var model = new WareHouseItemCategoryModel();
            await GetDropDownList(model);
            return ViewComponent("CreateWareHouseItemCategory", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WareHouseItemCategoryModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.Code = ExtensionFull.GetVoucherCode("NCC");

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
            var model = result.ResultObj;

            await GetDropDownList(model);

            if (model.AvailableCategory.Count > 0 &&
                !string.IsNullOrEmpty(model.ParentId))
            {
                var item = model.AvailableCategory
                    .FirstOrDefault(x => x.Value.Equals(model.ParentId));

                if (item != null)
                {
                    item.Selected = true;
                }
            }

            return ViewComponent("EditWareHouseItemCategory", model);
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

        [HttpGet]
        public async Task<IActionResult> Detail(string wareHouseItemCategoryId)
        {
            var result = await _wareHouseItemCategoryApiClient.GetById(wareHouseItemCategoryId);
            var model = result.ResultObj;

            await GetDropDownList(model);

            if (model.AvailableCategory.Count > 0 &&
                !string.IsNullOrEmpty(model.ParentId))
            {
                var item = model.AvailableCategory
                    .FirstOrDefault(x => x.Value.Equals(model.ParentId));

                if (item != null)
                {
                    item.Selected = true;
                }
            }

            return ViewComponent("DetailWareHouseItemCategory", model);
        }

        #endregion

        #region Utilities
        private async Task GetDropDownList(WareHouseItemCategoryModel model)
        {
            var availableCategory = await _wareHouseItemCategoryApiClient.GetAvailableList();

            var categories = new List<SelectListItem>();
            var data = availableCategory;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.Id,
                    };
                    categories.Add(item);
                }
            }
            categories.OrderBy(e => e.Text);
            if (categories == null || categories.Count == 0)
            {
                categories = new List<SelectListItem>();
            }

            model.AvailableCategory = new List<SelectListItem>(categories);
        }

        #endregion
    }
}
