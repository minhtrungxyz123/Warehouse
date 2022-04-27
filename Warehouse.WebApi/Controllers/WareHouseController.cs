using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.WareHouse;
using Warehouse.Service.WareHouse;

namespace Warehouse.WebApi.Controllers
{
    [Route("ware-house")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        #region Fields

        private readonly IWareHouseService _wareHouseService;

        public WareHouseController(IWareHouseService wareHouseService)
        {
            _wareHouseService = wareHouseService;
        }

        #endregion Fields

        #region List

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _wareHouseService.GetAll());
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetAllPaging(string? search, int pageIndex, int pageSize)
        {
            return Ok(await _wareHouseService.GetAllPaging(search, pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _wareHouseService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"WareHouse with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post(WareHouseModel model)
        {
            var result = await _wareHouseService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create wareHouse failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(WareHouseModel model, string id)
        {
            var item = await _wareHouseService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"WareHouse with id: {id} is not found"));

            if (id == model.ParentId)
            {
                return BadRequest(new ApiBadRequestResponse("WareHouse cannot be a child itself."));
            }

            var result = await _wareHouseService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update wareHouse failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = _wareHouseService.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"WareHouse with id: {id} is not found"));

            var result = await _wareHouseService.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete wareHouse failed"));
            }
        }

        #endregion Method
    }
}