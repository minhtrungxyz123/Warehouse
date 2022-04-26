using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.WebApi.Data;
using Warehouse.WebApi.Models;
using Warehouse.WebApi.Service.Unit;


namespace Warehouse.WebApi.Controllers
{
    [Route("unit")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        #region Fields

        private readonly IUnitService _unitService;
        private readonly WarehouseContext _context;

        public UnitsController(IUnitService unitService, WarehouseContext context)
        {
            _unitService = unitService;
            _context = context;
        }

        #endregion

        #region List

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            return await _context.Units.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(string id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        #endregion

        #region Method

        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.Id }, unit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(string id, Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(string id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Utilities
        private bool UnitExists(string id)
        {
            return _context.Units.Any(e => e.Id == id);
        }
        #endregion
    }
}
