using Warehouse.Common;
using Warehouse.Model.WareHouseItem;

namespace Warehouse.Service.WareHouseItem
{
    public interface IWareHouseItemService
    {
        Task<IEnumerable<Data.Entities.WareHouseItem>> GetAll();

        Task<Pagination<Data.Entities.WareHouseItem>> GetAllPaging(string? search, int pageIndex, int pageSize);

        Task<Data.Entities.WareHouseItem> GetById(string? id);

        Task<RepositoryResponse> Create(WareHouseItemModel model);

        Task<RepositoryResponse> Update(string id, WareHouseItemModel model);

        Task<int> Delete(string id);
    }
}