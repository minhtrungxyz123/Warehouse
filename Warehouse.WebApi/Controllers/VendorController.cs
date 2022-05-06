using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.Vendor;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("vendor")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        #region Fields

        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        #endregion Fields

        #region List

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _vendorService.GetAll());
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetAllPaging([FromQuery] GetVendorPagingRequest request)
        {
            return Ok(await _vendorService.GetAllPaging(request));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _vendorService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Vendor with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post(VendorModel model)
        {
            var result = await _vendorService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create vendor failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(VendorModel model, string id)
        {
            var item = await _vendorService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Vendor with id: {id} is not found"));

            var result = await _vendorService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update vendor failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = _vendorService.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Vendor with id: {id} is not found"));

            var result = await _vendorService.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete vendor failed"));
            }
        }

        #endregion Method
    }
}