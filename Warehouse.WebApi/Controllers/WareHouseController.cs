using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.WareHouse;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("warehouse")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {

        #region Fields

        private readonly IWareHouseService _warehouseService;

        public WareHouseController(IWareHouseService wareHouseService)
        {
            _warehouseService = wareHouseService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _warehouseService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _warehouseService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetWareHousePagingRequest request)
        {
            var products = await _warehouseService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _warehouseService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"WareHouse with id: {id} is not found"));
            }

            return Ok(item);
        }

        [Route("get-available")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableList(bool showHidden = true)
        {
            var user = _warehouseService.GetMvcListItems(showHidden);
            return Ok(user);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] WareHouseModel model)
        {
            var result = await _warehouseService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create warehouse failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] WareHouseModel model, string id)
        {
            var item = await _warehouseService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"WareHouse with id: {id} is not found"));

            var result = await _warehouseService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update warehouse failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string warehouseId)
        {
            var result = await _warehouseService.Delete(warehouseId);
            return Ok(result);
        }

        #endregion Method
    }
}