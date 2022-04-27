﻿using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.Unit;
using Warehouse.Service.Unit;

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
        #endregion

        #region List

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _unitService.GetAll());
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetAllPaging(string? search, int pageIndex, int pageSize)
        {
            return Ok(await _unitService.GetAllPaging(search, pageIndex, pageSize));
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

        #endregion

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post(UnitModel model)
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
        public async Task<IActionResult> Put(UnitModel model, string id)
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

        #endregion
    }
}