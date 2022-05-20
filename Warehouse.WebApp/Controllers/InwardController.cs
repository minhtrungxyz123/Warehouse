using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Model.Inward;
using Warehouse.Model.InwardDetail;
using Warehouse.Model.SerialWareHouse;
using Warehouse.Model.WareHouseItemUnit;
using Warehouse.WebApp.ApiClient;
using Warehouse.WebApp.Models;

namespace Warehouse.WebApp.Controllers
{
    public class InwardController : Controller
    {
        #region Fields
        private readonly IBeginningWareHouseApiClient _beginningWareHouseApiClient;
        private readonly IWareHouseApiClient _wareHouseApiClient;
        private readonly IWareHouseItemApiClient _wareHouseItemApiClient;
        private readonly IInwardApiClient _inwardApiClient;

        public InwardController(IInwardApiClient inwardApiClient,
            IWareHouseItemApiClient wareHouseItemApiClient,
            IWareHouseApiClient wareHouseApiClient,
            IBeginningWareHouseApiClient beginningWareHouseApiClient)
        {
            _inwardApiClient = inwardApiClient;
            _wareHouseItemApiClient = wareHouseItemApiClient;
            _wareHouseApiClient = wareHouseApiClient;
            _beginningWareHouseApiClient = beginningWareHouseApiClient;
        }

        #endregion Fields

        #region Method

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new InwardModel();
            await GetDropDownList(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSave(InwardModel model, IEnumerable<InwardDetailModel> listDetalis)
        {

            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;
            model.CreatedBy = userId;

            if (!ModelState.IsValid)
                return View(model);

            model.InwardDetails = listDetalis.ToList();

            var result = await _inwardApiClient.Create(model);
            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Create");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(model);
        }

        #endregion

        #region Utilities

        private async Task GetDropDownListDetail(InwardDetailModel model)
        {
            var availableUnit = await _wareHouseItemApiClient.GetAvailableList();
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
        }

        private async Task GetDropDownList(InwardModel model)
        {
            var availableWH = await _wareHouseApiClient.GetAvailableList();
            var availableVendor = await _wareHouseItemApiClient.GetVendor();

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

            //vendor
            var categories3 = new List<SelectListItem>();
            var data3 = availableVendor;

            if (data3?.Count > 0)
            {
                foreach (var m3 in data3)
                {
                    var item3 = new SelectListItem
                    {
                        Text = m3.Name,
                        Value = m3.Id,
                    };
                    categories3.Add(item3);
                }
            }
            categories3.OrderBy(e => e.Text);
            if (categories3 == null || categories3.Count == 0)
            {
                categories3 = new List<SelectListItem>();
            }

            model.AvailableVendor = new List<SelectListItem>(categories3);
        }

        [HttpGet]
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

        #region AddItem

        [HttpGet]
        public async Task<IActionResult> AddItem()
        {
            var model = new InwardDetailModel();
            await GetDropDownListDetail(model);
            return ViewComponent("AddItemCP", model);
        }

        #endregion
    }
}
