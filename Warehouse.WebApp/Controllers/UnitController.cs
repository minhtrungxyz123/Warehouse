using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Warehouse.Model.Unit;
using Warehouse.WebApp.ApiClient;

namespace Warehouse.WebApp.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitApiClient _unitApiClient;

        public UnitController(IUnitApiClient unitApiClient)
        {
            _unitApiClient = unitApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _unitApiClient.GetAll();

            var model = JsonConvert.DeserializeObject<List<UnitModel>>(response);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnitModel request)
        {
            try
            {
                var response = await _unitApiClient.Create(request);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}