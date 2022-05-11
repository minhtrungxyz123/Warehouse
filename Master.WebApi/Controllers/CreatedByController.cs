using Master.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.CreatedBy;

namespace Master.WebApi.Controllers
{
    [Route("created-by")]
    [ApiController]
    public class CreatedByController : ControllerBase
    {
        #region Fields

        private readonly ICreatedByService _createdByService;

        public CreatedByController(ICreatedByService createdByService)
        {
            _createdByService = createdByService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _createdByService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _createdByService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetCreatedByPagingRequest request)
        {
            var products = await _createdByService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _createdByService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"CreatedBy with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreatedByModel model)
        {
            var result = await _createdByService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create CreatedBy failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] CreatedByModel model, string id)
        {
            var item = await _createdByService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"CreatedBy with id: {id} is not found"));

            var result = await _createdByService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update CreatedBy failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _createdByService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}
