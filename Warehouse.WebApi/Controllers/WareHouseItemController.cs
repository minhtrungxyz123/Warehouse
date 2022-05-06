using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.WareHouseItem;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("wareHouse-item")]
    [ApiController]
    public class WareHouseItemController : ControllerBase
    {
        #region Fields

        private readonly IWareHouseItemService _wareHouseItemService;

        public WareHouseItemController(IWareHouseItemService wareHouseItemService)
        {
            _wareHouseItemService = wareHouseItemService;
        }

        #endregion Fields

        #region List

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _wareHouseItemService.GetAll());
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetAllPaging(string? search, int pageIndex, int pageSize)
        {
            return Ok(await _wareHouseItemService.GetAllPaging(search, pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _wareHouseItemService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"WareHouseItem with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post(WareHouseItemModel model)
        {
            var result = await _wareHouseItemService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create wareHouseItem failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(WareHouseItemModel model, string id)
        {
            var item = await _wareHouseItemService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"WareHouseItem with id: {id} is not found"));

            var result = await _wareHouseItemService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update wareHouseItem failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = _wareHouseItemService.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"WareHouseItem with id: {id} is not found"));

            var result = await _wareHouseItemService.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete wareHouseItem failed"));
            }
        }

        #endregion Method
    }
}