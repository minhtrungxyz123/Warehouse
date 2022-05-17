using Microsoft.AspNetCore.Mvc;
using Warehouse.Common;
using Warehouse.Model.WareHouseItemUnit;
using Warehouse.Service;

namespace Warehouse.WebApi.Controllers
{
    [Route("warehouse-item-unit")]
    [ApiController]
    public class WareHouseItemUnitController : ControllerBase
    {
        #region Fields

        private readonly IWareHouseItemUnitService _wareHouseItemUnitService;
        private readonly IUnitService _unitService;

        public WareHouseItemUnitController(IWareHouseItemUnitService wareHouseItemUnitService,
            IUnitService unitService)
        {
            _wareHouseItemUnitService = wareHouseItemUnitService;
            _unitService = unitService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsyn(string id)
        {
            var user = await _wareHouseItemUnitService.GetByIdAsyn(id);
            return Ok(user);
        }

        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? ItemId)
        {
            var searchContext = new GetWareHouseItemUnitPagingRequest
            {
                ItemId = ItemId
            };
            var entities = _wareHouseItemUnitService.GetByWareHouseItemUnitId(searchContext);

            var units = _unitService.GetMvcListItems(true);

            foreach (var e in entities)
            {
                if (!string.IsNullOrWhiteSpace(e.UnitId))

                    e.UnitName = units.FirstOrDefault(w => w.Id == e.UnitId)?.UnitName;
            }

            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var item = await _wareHouseItemUnitService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"WareHouseItemUnit with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] WareHouseItemUnitModel model)
        {
            var result = await _wareHouseItemUnitService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create WareHouseItemUnit failed"));
            }
        }

        #endregion
    }
}