using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Warehouse.Common;
using Warehouse.Model.Inward;
using Warehouse.Model.InwardDetail;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("inward")]
    [ApiController]
    public class InwardController : ControllerBase
    {
        #region Fields

        private readonly IInwardService _inwardService;

        public InwardController(IInwardService inwardService)
        {
            _inwardService = inwardService;
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
        public async Task<IActionResult> Post([FromBody] InwardModel model)
        {
            if (model.InwardDetails != null && model.InwardDetails.Any())
            {
                var i = 0;
                foreach (var detail in model.InwardDetails)
                {
                    ModelState.Remove($"InwardDetails[{i}].InwardId");
                    i++;
                }
            }

            var entity = model;
            entity.VoucherCode = model.VoucherCode;
            entity.VoucherDate = model.VoucherDate.ToUniversalTime();

            var detailEntities = new List<InwardDetailModel>();
            if (model.InwardDetails != null && model.InwardDetails.Any())
            {
                detailEntities = model.InwardDetails.Select(mDetail =>
                {
                    var eDetail = mDetail;
                    eDetail.InwardId = entity.Id;

                    eDetail.SerialWareHouses = mDetail.SerialWareHouses.Select(mSerial =>
                    {
                        var eSerial = mSerial;
                        eSerial.ItemId = eDetail.ItemId;
                        eSerial.InwardDetailId = eDetail.Id;

                        return eSerial;
                    });

                    return eDetail;
                }).ToList();
            }

            var result = await _inwardService.Create(entity, detailEntities);

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