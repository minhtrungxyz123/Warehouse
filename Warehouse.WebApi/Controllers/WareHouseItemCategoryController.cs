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

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _wareHouseItemCategoryService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _wareHouseItemCategoryService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetWareHouseItemCategoryPagingRequest request)
        {
            var products = await _wareHouseItemCategoryService.GetAllPaging(request);
            return Ok(products);
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
        public async Task<IActionResult> Post([FromBody] WareHouseItemCategoryModel model)
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
        public async Task<IActionResult> Put([FromBody] WareHouseItemCategoryModel model, string id)
        {
            var item = await _wareHouseItemCategoryService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"WareHouseItemCategory with id: {id} is not found"));

            var result = await _wareHouseItemCategoryService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update WareHouseItemCategory failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _wareHouseItemCategoryService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}