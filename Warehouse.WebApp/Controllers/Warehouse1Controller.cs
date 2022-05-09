using Microsoft.AspNetCore.Mvc;
using Warehouse.Model.WareHouse;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    public class Warehouse1Controller : Controller
    {
        #region Fields

        private readonly IWareHouseApiClient _warehouseApiClient;

        public Warehouse1Controller(IWareHouseApiClient wareHouseApiClient)
        {
            _warehouseApiClient = wareHouseApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetWareHousePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _warehouseApiClient.GetPagings(request);
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
            return ViewComponent("CreateWareHouse1");
        }

        [HttpPost]
        public async Task<IActionResult> Create(WareHouseModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _warehouseApiClient.Create(request);

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
            var result = await _warehouseApiClient.GetById(warehouseId);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new WareHouseModel()
                {
                    Name = user.Name,
                    Inactive = user.Inactive,
                    Id = warehouseId,
                    Address = user.Address,
                    Code = user.Code,
                    Description = user.Description,
                    ParentId = user.ParentId,
                    Path = user.Path
                };
                return ViewComponent("EditWareHouse1", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WareHouseModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _warehouseApiClient.Edit(request.Id, request);
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
            var result = await _warehouseApiClient.Delete(id);
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
