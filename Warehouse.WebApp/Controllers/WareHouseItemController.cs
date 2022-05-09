﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Model.Unit;
using Warehouse.Model.WareHouseItem;
using Warehouse.WebApp.ApiClient;
using System.Linq;

namespace Warehouse.WebApp.Controllers
{
    public class WareHouseItemController : Controller
    {
        #region Fields

        private readonly IWareHouseItemApiClient _wareHouseItemApiClient;

        public WareHouseItemController(IWareHouseItemApiClient wareHouseItemApiClient)
        {
            _wareHouseItemApiClient = wareHouseItemApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, string CategoryId, string VendorId, string UnitId, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetWareHouseItemPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = CategoryId,
                VendorId = VendorId,
                UnitId = UnitId,
            };
            var data = await _wareHouseItemApiClient.GetPagings(request);
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
            var model = new WareHouseItemModel();
            await GetDropDownList(model);
            return ViewComponent("CreateWareHouseItem", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WareHouseItemModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _wareHouseItemApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string itemId)
        {
            var result = await _wareHouseItemApiClient.GetById(itemId);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new WareHouseItemModel()
                {
                    Name = user.Name,
                    Inactive = user.Inactive,
                    Id = itemId,
                    CategoryId = user.CategoryId,
                    Code = user.Code,
                    Country = user.Country,
                    Description = user.Description,
                    UnitId = user.UnitId,
                    VendorId = user.VendorId,
                    VendorName = user.VendorName,
                };

                await GetDropDownList(updateRequest);
                return ViewComponent("EditWareHouseItem", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WareHouseItemModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _wareHouseItemApiClient.Edit(request.Id, request);
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
            var result = await _wareHouseItemApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        #endregion

        #region Utilities
        private async Task GetDropDownList(WareHouseItemModel model)
        {
            var availableUnits = await  _wareHouseItemApiClient.GetAvailableList();

            var availableVendor = await _wareHouseItemApiClient.GetVendor();

            var availableCategory = await _wareHouseItemApiClient.GetCategory();

            var categories = new List<SelectListItem>();
            var data = availableUnits;

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

            // vendor
            var categories1 = new List<SelectListItem>();
            var data1 = availableVendor;

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

            model.AvailableVendor = new List<SelectListItem>(categories1);

            // WHcategory
            var categories2 = new List<SelectListItem>();
            var data2 = availableCategory;

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

            model.AvailableCategory = new List<SelectListItem>(categories2);
        } 
        #endregion
    }
}
