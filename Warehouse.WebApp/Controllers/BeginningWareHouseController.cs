using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Model.BeginningWareHouse;
using Warehouse.Model.WareHouseItemUnit;
using Warehouse.WebApp.ApiClient;
using Warehouse.WebApp.Models;

namespace Warehouse.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BeginningWareHouseController : Controller
    {
        #region Fields

        private readonly IBeginningWareHouseApiClient _beginningWareHouseApiClient;
        private readonly IWareHouseItemApiClient _wareHouseItemApiClient;
        private readonly IWareHouseApiClient _wareHouseApiClient;

        public BeginningWareHouseController(IBeginningWareHouseApiClient beginningWareHouseApiClient,
            IWareHouseItemApiClient wareHouseItemApiClient,
            IWareHouseApiClient wareHouseApiClient)
        {
            _beginningWareHouseApiClient = beginningWareHouseApiClient;
            _wareHouseItemApiClient = wareHouseItemApiClient;
            _wareHouseApiClient = wareHouseApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, string itemId, string warehouseId, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetBeginningWareHousePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                WarehouseId = warehouseId,
                ItemId = itemId
            };
            var data = await _beginningWareHouseApiClient.GetPagings(request);
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
            var model = new BeginningWareHouseModel();
            await GetDropDownList(model);
            return ViewComponent("CreateBeginningWareHouse", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BeginningWareHouseModel request)
        {
            request.CreatedDate = DateTime.Now;
            request.ModifiedDate = DateTime.Now;
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;
            request.CreatedBy = userId;
            if (!ModelState.IsValid)
                return View(request);

            var result = await _beginningWareHouseApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            TempData["result"] = "Thêm mới thất bại";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string beginningWareHouseId)
        {
            var result = await _beginningWareHouseApiClient.GetById(beginningWareHouseId);
            var model = result.ResultObj;

            await GetDropDownList(model);

            if (model.AvailableUnit.Count > 0 &&
                !string.IsNullOrEmpty(model.UnitId))
            {
                var item = model.AvailableUnit
                    .FirstOrDefault(x => x.Value.Equals(model.UnitId));

                if (item != null)
                {
                    item.Selected = true;
                }
            }

            if (model.AvailableWarehouse.Count > 0 &&
                !string.IsNullOrEmpty(model.WareHouseId))
            {
                var item1 = model.AvailableWarehouse
                    .FirstOrDefault(x => x.Value.Equals(model.WareHouseId));

                if (item1 != null)
                {
                    item1.Selected = true;
                }
            }

            if (model.AvailableItem.Count > 0 &&
               !string.IsNullOrEmpty(model.ItemId))
            {
                var item2 = model.AvailableItem
                    .FirstOrDefault(x => x.Value.Equals(model.ItemId));

                if (item2 != null)
                {
                    item2.Selected = true;
                }
            }

            return ViewComponent("EditBeginningWareHouse", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BeginningWareHouseModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _beginningWareHouseApiClient.Edit(request.Id, request);
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
            var result = await _beginningWareHouseApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string beginningWareHouseId)
        {
            var result = await _beginningWareHouseApiClient.GetById(beginningWareHouseId);
            var model = result.ResultObj;

            await GetDropDownList(model);

            if (model.AvailableUnit.Count > 0 &&
                !string.IsNullOrEmpty(model.UnitId))
            {
                var item = model.AvailableUnit
                    .FirstOrDefault(x => x.Value.Equals(model.UnitId));

                if (item != null)
                {
                    item.Selected = true;
                }
            }

            if (model.AvailableWarehouse.Count > 0 &&
                !string.IsNullOrEmpty(model.WareHouseId))
            {
                var item1 = model.AvailableWarehouse
                    .FirstOrDefault(x => x.Value.Equals(model.WareHouseId));

                if (item1 != null)
                {
                    item1.Selected = true;
                }
            }

            if (model.AvailableItem.Count > 0 &&
               !string.IsNullOrEmpty(model.ItemId))
            {
                var item2 = model.AvailableItem
                    .FirstOrDefault(x => x.Value.Equals(model.ItemId));

                if (item2 != null)
                {
                    item2.Selected = true;
                }
            }

            return ViewComponent("DetailBeginningWareHouse", model);
        }

        #endregion Method

        #region Utilities

        private async Task GetDropDownList(BeginningWareHouseModel model)
        {
            var availableUnit = await _wareHouseItemApiClient.GetAvailableList();
            var availableWH = await _wareHouseApiClient.GetAvailableList();
            var availableItem = await _wareHouseItemApiClient.GetAvailableItem();

            //unit
            var categories = new List<SelectListItem>();
            var data = availableUnit;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new SelectListItem
                    {
                        Text = m.UnitName,
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

            model.AvailableUnit = new List<SelectListItem>(categories);

            //whitem
            var categories1 = new List<SelectListItem>();
            var data1 = availableItem;

            if (data1?.Count > 0)
            {
                foreach (var m1 in data1)
                {
                    var item1 = new SelectListItem
                    {
                        Text = m1.Name,
                        Value = m1.Id,
                    };
                    categories1.Add(item1);
                }
            }
            categories1.OrderBy(e => e.Text);
            if (categories1 == null || categories1.Count == 0)
            {
                categories1 = new List<SelectListItem>();
            }

            model.AvailableItem = new List<SelectListItem>(categories1);

            //wh
            var categories2 = new List<SelectListItem>();
            var data2 = availableWH;

            if (data2?.Count > 0)
            {
                foreach (var m2 in data2)
                {
                    var item2 = new SelectListItem
                    {
                        Text = m2.Name,
                        Value = m2.Id,
                    };
                    categories2.Add(item2);
                }
            }
            categories2.OrderBy(e => e.Text);
            if (categories2 == null || categories2.Count == 0)
            {
                categories2 = new List<SelectListItem>();
            }

            model.AvailableWarehouse = new List<SelectListItem>(categories2);
        }

        public async Task<IActionResult> GetWareHouseItemUnitByItemId(string id)
        {
            var getUnitItem = new GetWareHouseItemUnitPagingRequest();
            getUnitItem.ItemId = id;
            var listItem = await _beginningWareHouseApiClient.GetByWareHouseItemUnitId(id);
            var getItem = await _beginningWareHouseApiClient.GetByWareHouseItemId(id);
            var model = new List<SelectItem>();
            foreach (var item in listItem)
            {
                var tem = new SelectItem
                {
                    text = item.UnitName,
                    id = item.UnitId
                };
                if (getItem != null && getItem.UnitId.Equals(item.UnitId))
                    tem.selected = true;
                model.Add(tem);
            }

            return Ok(model);
        }

        #endregion Utilities
    }
}