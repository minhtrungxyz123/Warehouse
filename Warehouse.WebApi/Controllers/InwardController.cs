using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.Inward;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("inward")]
    [ApiController]
    public class InwardController : ControllerBase
    {
        #region Fields

        private readonly IInwardService _inwardService;
        private readonly IInwardDetailService _inwardDetailService;

        public InwardController(IInwardService inwardService, IInwardDetailService inwardDetailService)
        {
            _inwardService = inwardService;
            _inwardDetailService = inwardDetailService;
        }

        #endregion Fields

        #region List

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _inwardService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"inward with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] InwardGridModel model)
        {
            var result = await _inwardService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create inward failed"));
            }
        }

        [Route("edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var entity = await _inwardService.GetById(id);
            if (entity == null)
            {
                return NotFound(new ApiNotFoundResponse($"inward with id: {id} is not found"));
            }

            return Ok(entity);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] InwardGridModel model, string id)
        {
            var item = await _inwardService.GetByInwardId(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"inward with id: {id} is not found"));

            var result = await _inwardService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update inward failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _inwardService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}