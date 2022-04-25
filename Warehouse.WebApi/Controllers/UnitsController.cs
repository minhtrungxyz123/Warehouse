using Microsoft.AspNetCore.Mvc;
using Warehouse.WebApi.Service.Unit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warehouse.WebApi.Controllers
{
    [Route("unit")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        // GET: api/<UnitsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_unitService.Get());
        }

        // GET api/<UnitsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UnitsController>
        [HttpPost]
        public IActionResult Post(Warehouse.WebApi.Models.Unit unit)
        {
            return Ok(_unitService.AddEntity(unit));
        }

        // PUT api/<UnitsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UnitsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(_unitService.DeleteEntity(id));
        }
    }
}
