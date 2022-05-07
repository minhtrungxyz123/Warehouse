using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Unit;

namespace Warehouse.Service
{
    public interface IUnitService
    {
        Task<IEnumerable<Data.Entities.Unit>> GetAll();

        Task<ApiResult<Pagination<Data.Entities.Unit>>> GetAllPaging(GetUnitPagingRequest request);

        Task<Data.Entities.Unit> GetById(string? id);

        Task<RepositoryResponse> Create(UnitModel model);

        Task<RepositoryResponse> Update(string id, UnitModel model);

        Task<int> Delete(string unitId);

        Task<ApiResult<Data.Entities.Unit>> GetByIdAsyn(string id);
    }
}