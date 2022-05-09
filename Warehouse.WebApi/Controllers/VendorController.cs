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

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _vendorService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _vendorService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetVendorPagingRequest request)
        {
            var products = await _vendorService.GetAllPaging(request);
            return Ok(products);
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

        [Route("get-available")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableList(bool showHidden = true)
        {
            var user = _vendorService.GetMvcListItems(showHidden);
            return Ok(user);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] VendorModel model)
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
        public async Task<IActionResult> Put([FromBody] VendorModel model, string id)
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

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string vendorId)
        {
            var result = await _vendorService.Delete(vendorId);
            return Ok(result);
        }

        #endregion Method
    }
}