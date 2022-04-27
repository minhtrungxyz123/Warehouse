using Warehouse.Model.Unit;

namespace Warehouse.WebApp.ApiClient
{
    public interface IUnitApiClient
    {
        public Task<string> GetAll();

        public Task<string> Create(UnitModel request);
    }
}
