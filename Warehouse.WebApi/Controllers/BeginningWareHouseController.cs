using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.BeginningWareHouse;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("beginning-wareHouse")]
    [ApiController]
    public class BeginningWareHouseController : ControllerBase
    {
        #region Fields

        private readonly IBeginningWareHouseService _beginningWareHouseService;

        public BeginningWareHouseController(IBeginningWareHouseService beginningWareHouseService)
        {
            _beginningWareHouseService = beginningWareHouseService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _beginningWareHouseService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _beginningWareHouseService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetBeginningWareHousePagingRequest request)
        {
            var products = await _beginningWareHouseService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _beginningWareHouseService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"BeginningWareHouse with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] BeginningWareHouseModel models)
        {
            if (await _beginningWareHouseService.ExistAsync(models.WareHouseId, models.ItemId) == false)
            {
                var result = await _beginningWareHouseService.Create(models);

                if (result.Result > 0)
                {
                    return RedirectToAction(nameof(Get), new { id = result.Id });
                }
            }
            return BadRequest(new ApiBadRequestResponse("Create BeginningWareHouse failed"));

        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] BeginningWareHouseModel model, string id)
        {
            var item = await _beginningWareHouseService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"BeginningWareHouse with id: {id} is not found"));

            if (await _beginningWareHouseService.ExistAsync(model.WareHouseId, model.ItemId) == false)
            {
                var result = await _beginningWareHouseService.Update(id, model);
                if (result.Result > 0)
                {
                    return Ok();
                }
            }
                return BadRequest(new ApiBadRequestResponse("Update BeginningWareHouse failed"));

        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _beginningWareHouseService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}