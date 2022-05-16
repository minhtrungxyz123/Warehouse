using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public WareHouseItemUnitController(IWareHouseItemUnitService  wareHouseItemUnitService,
            IUnitService unitService)
        {
            _wareHouseItemUnitService = wareHouseItemUnitService;
            _unitService = unitService;
        }

        #endregion Fields

        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? ItemId)
        {
            var searchContext = new GetWareHouseItemUnitPagingRequest
            {
                ItemId = ItemId
            };

            var models = new List<WareHouseItemUnitModel>();
            var entities = _wareHouseItemUnitService.GetByWareHouseItemUnitId(searchContext);

            var units = _unitService.GetMvcListItems(true);

            foreach (var e in entities)
            {

                if (!string.IsNullOrWhiteSpace(e.UnitId))

                    e.UnitName = units.FirstOrDefault(w => w.Id == e.UnitId)?.UnitName;
            }

            return Ok(models);
        }
    }
}
