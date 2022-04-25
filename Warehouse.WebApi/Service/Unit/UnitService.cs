using Warehouse.WebApi.Data;

namespace Warehouse.WebApi.Service.Unit
{
    public class UnitService : IUnitService
    {
        private readonly WarehouseContext _context;
        public UnitService(WarehouseContext context)
        {
            _context = context;
        }

        public bool AddEntity(WebApi.Models.Unit entity)
        {
            _context.Units.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteEntity(string id)
        {
            WebApi.Models.Unit unit = _context.Units.Find(id);
            _context.Units.Remove(unit);
            _context.SaveChanges();
            return true;
        }

        public List<WebApi.Models.Unit> Get()
        {
            return _context.Units.OrderByDescending(x => x.Id).ToList();
        }

        public bool UpdateEntity(WebApi.Models.Unit entity)
        {
            throw new NotImplementedException();
        }
    }
}