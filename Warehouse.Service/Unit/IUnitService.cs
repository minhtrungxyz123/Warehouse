using Warehouse.Common;
using Warehouse.Model.Unit;

namespace Warehouse.Service.Unit
{
    public interface IUnitService
    {
        Task<IEnumerable<Data.Entities.Unit>> GetAll();

        Task<Pagination<Data.Entities.Unit>> GetAllPaging(string? search, int pageIndex, int pageSize);

        Task<Data.Entities.Unit> GetById(string? id);

        Task<RepositoryResponse> Create(UnitModel model);

        Task<RepositoryResponse> Update(string id, UnitModel model);

        Task<int> Delete(string id);
    }
}