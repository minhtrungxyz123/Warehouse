﻿using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.Inward;
using Warehouse.Service.Inward;

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
                return NotFound(new ApiNotFoundResponse($"Inward with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post(InwardModel model)
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(InwardModel model, string id)
        {
            var item = await _inwardService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Inward with id: {id} is not found"));

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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = _inwardService.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Inward with id: {id} is not found"));

            var result = await _inwardService.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete inward failed"));
            }
        }

        #endregion Method
    }
}