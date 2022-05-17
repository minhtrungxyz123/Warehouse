﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Model.Inward;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    public class InwardController : Controller
    {
        #region Fields

        private readonly IWareHouseApiClient _wareHouseApiClient;
        private readonly IWareHouseItemApiClient _wareHouseItemApiClient;
        private readonly IInwardApiClient _inwardApiClient;

        public InwardController(IInwardApiClient inwardApiClient,
            IWareHouseItemApiClient wareHouseItemApiClient,
            IWareHouseApiClient wareHouseApiClient)
        {
            _inwardApiClient = inwardApiClient;
            _wareHouseItemApiClient = wareHouseItemApiClient;
            _wareHouseApiClient = wareHouseApiClient;
        }

        #endregion Fields

        #region Method

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new InwardGridModel();
            await GetDropDownList(model);
            return ViewComponent("CreateInward", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InwardGridModel request)
        {
            request.CreatedDate = DateTime.Now;
            request.ModifiedDate = DateTime.Now;
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;
            request.CreatedBy = userId;
            if (!ModelState.IsValid)
                return View(request);

            var result = await _inwardApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            TempData["result"] = "Thêm mới thất bại";
            return RedirectToAction("Index");
        }

        #endregion

        #region Utilities

        private async Task GetDropDownList(InwardGridModel model)
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

        #endregion Utilities
    }
}
