using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Service.Audit;
using Warehouse.Service.AuditDetail;

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
        #endregion

        #region List

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string auditId)
        {
            var item = await _auditDetailService.GetByAuditId(auditId);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Audit with id: {auditId} is not found"));
            }

            return Ok(item);
        }

        #endregion
    }
}
