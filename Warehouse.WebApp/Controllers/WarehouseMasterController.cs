using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Common.Common;
using Warehouse.Model.WareHouse;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    public class WarehouseMasterController : Controller
    {
        #region Fields

        private readonly IWareHouseApiClient _wareHouseApiClient;

        public WarehouseMasterController(IWareHouseApiClient wareHouseApiClient)
        {
            _wareHouseApiClient = wareHouseApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, string parentId, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetWareHousePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                ParentId = parentId
            };
            var data = await _wareHouseApiClient.GetPagings(request);
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
            var model = new WareHouseModel();
            await GetDropDownList(model);
            return ViewComponent("CreateWarehouseMaster", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WareHouseModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.Code = ExtensionFull.GetVoucherCode("NCC");

            var result = await _wareHouseApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string warehouseId)
        {
            var result = await _wareHouseApiClient.GetById(warehouseId);
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

            return ViewComponent("EditWarehouseMaster", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WareHouseModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _wareHouseApiClient.Edit(request.Id, request);
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
            var result = await _wareHouseApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string warehouseId)
        {
            var result = await _wareHouseApiClient.GetById(warehouseId);
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

            return ViewComponent("DetailWarehouseMaster", model);
        }

        #endregion

        #region Utilities
        private async Task GetDropDownList(WareHouseModel model)
        {
            var availableCategory = await _wareHouseApiClient.GetAvailableList();

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
