using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.WareHouse;

namespace Warehouse.Service
{
    public interface IWareHouseService
    {
        Task<IEnumerable<Data.Entities.WareHouse>> GetAll();

        Task<ApiResult<Pagination<Data.Entities.WareHouse>>> GetAllPaging(GetWareHousePagingRequest request);

        Task<Data.Entities.WareHouse> GetById(string? id);

        Task<RepositoryResponse> Create(WareHouseModel model);

        Task<RepositoryResponse> Update(string id, WareHouseModel model);

        Task<int> Delete(string id);
    }
}