using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.Audit;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        #region Fields

        private readonly IAuditService _auditService;
        private readonly IAuditDetailService _auditDetailService;

        public AuditController(IAuditService auditService, IAuditDetailService auditDetailService)
        {
            _auditService = auditService;
            _auditDetailService = auditDetailService;
        }

        #endregion Fields

        #region List

        [Route("get")]
        [HttpGet]
        public async Task<ActionResult> GetAllPaging([FromQuery] GetAuditPagingRequest request)
        {
            var audit = await _auditDetailService.GetAllPaging(request);
            return Ok(audit);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _auditService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Audit with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post(AuditModel model)
        {
            var result = await _auditService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create audit failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(AuditModel model, string id)
        {
            var item = await _auditService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Audit with id: {id} is not found"));

            var result = await _auditService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update audit failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = _auditService.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Audit with id: {id} is not found"));

            var result = await _auditService.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete audit failed"));
            }
        }

        #endregion Method
    }
}