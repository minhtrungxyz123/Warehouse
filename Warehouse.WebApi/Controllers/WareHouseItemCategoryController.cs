using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.WareHouseItemCategory;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("wareHouse-itemCategory")]
    [ApiController]
    public class WareHouseItemCategoryController : ControllerBase
    {
        #region Fields

        private readonly IWareHouseItemCategoryService _wareHouseItemCategoryService;

        public WareHouseItemCategoryController(IWareHouseItemCategoryService wareHouseItemCategoryService)
        {
            _wareHouseItemCategoryService = wareHouseItemCategoryService;
        }

        #endregion Fields

        #region List

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _wareHouseItemCategoryService.GetAll());
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetAllPaging(string? search, int pageIndex, int pageSize)
        {
            return Ok(await _wareHouseItemCategoryService.GetAllPaging(search, pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _wareHouseItemCategoryService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"WareHouseItemCategory with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post(WareHouseItemCategoryModel model)
        {
            var result = await _wareHouseItemCategoryService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create WareHouseItemCategory failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(WareHouseItemCategoryModel model, string id)
        {
            var item = await _wareHouseItemCategoryService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"WareHouseItemCategory with id: {id} is not found"));

            if (id == model.ParentId)
            {
                return BadRequest(new ApiBadRequestResponse("WareHouseItemCategory cannot be a child itself."));
            }

            var result = await _wareHouseItemCategoryService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update wareHouseItemCategory failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = _wareHouseItemCategoryService.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"WareHouseItemCategory with id: {id} is not found"));

            var result = await _wareHouseItemCategoryService.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete wareHouseItemCategory failed"));
            }
        }

        #endregion Method
    }
}