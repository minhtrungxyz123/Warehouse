using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitModel>>> Get(DataSourceLoadOptions loadOptions)
        {
            HttpClient client = new HttpClient();
            using HttpResponseMessage httpResponseMessage = await client.GetAsync("https://localhost:2000/unit");
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            if (!httpResponseMessage.IsSuccessStatusCode)
                return BadRequest();
            IEnumerable<UnitModel> entities = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<UnitModel>>(httpResponseContent);
            return Ok(DataSourceLoader.Load(entities, loadOptions));
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