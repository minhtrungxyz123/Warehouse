namespace Warehouse.WebApi.Service.Unit
{
    public interface IUnitService
    {
        List<Warehouse.WebApi.Models.Unit> Get();

        Boolean AddEntity(Warehouse.WebApi.Models.Unit entity);

        Boolean UpdateEntity(Warehouse.WebApi.Models.Unit entity);

        Boolean DeleteEntity(string id);
    }
}
