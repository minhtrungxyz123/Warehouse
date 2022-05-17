using Warehouse.Common;
using Warehouse.Model.BeginningWareHouse;

namespace Warehouse.Service
{
    public interface IBeginningWareHouseService
    {
        Task<IEnumerable<Data.Entities.BeginningWareHouse>> GetAll();

        Task<ApiResult<Pagination<BeginningWareHouseModel>>> GetAllPaging(GetBeginningWareHousePagingRequest request);

        Task<Data.Entities.BeginningWareHouse> GetById(string? id);

        Task<RepositoryResponse> Create(BeginningWareHouseModel model);

        Task<RepositoryResponse> Update(string id, BeginningWareHouseModel model);

        Task<int> Delete(string id);

        Task<ApiResult<Data.Entities.BeginningWareHouse>> GetByIdAsyn(string id);

        Task<bool> ExistAsync(string idWareHouse, string idItem);

    }
}