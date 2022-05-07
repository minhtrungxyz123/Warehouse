using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.Unit;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("unit")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        #region Fields

        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        #endregion Fields

        #region List

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _unitService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _unitService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUnitPagingRequest request)
        {
            var products = await _unitService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _unitService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Unit with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] UnitModel model)
        {
            var result = await _unitService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create unit failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] UnitModel model, string id)
        {
            var item = await _unitService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Unit with id: {id} is not found"));

            var result = await _unitService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update unit failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = _unitService.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Unit with id: {id} is not found"));

            var result = await _unitService.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete unit failed"));
            }
        }

        #endregion Method
    }
}