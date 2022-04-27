using Warehouse.Common;
using Warehouse.Model.WareHouse;

namespace Warehouse.Service.WareHouse
{
    public interface IWareHouseService
    {
        Task<IEnumerable<Data.Entities.WareHouse>> GetAll();

        Task<Pagination<Data.Entities.WareHouse>> GetAllPaging(string? search, int pageIndex, int pageSize);

        Task<Data.Entities.WareHouse> GetById(string? id);

        Task<RepositoryResponse> Create(WareHouseModel model);

        Task<RepositoryResponse> Update(string id, WareHouseModel model);

        Task<int> Delete(string id);
    }
}