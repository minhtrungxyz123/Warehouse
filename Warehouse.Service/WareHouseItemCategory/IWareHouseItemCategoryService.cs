using Warehouse.Common;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.Service
{
    public interface IWareHouseItemCategoryService
    {
        Task<IEnumerable<Data.Entities.WareHouseItemCategory>> GetAll();

        Task<Pagination<Data.Entities.WareHouseItemCategory>> GetAllPaging(string? search, int pageIndex, int pageSize);

        Task<Data.Entities.WareHouseItemCategory> GetById(string? id);

        Task<RepositoryResponse> Create(WareHouseItemCategoryModel model);

        Task<RepositoryResponse> Update(string id, WareHouseItemCategoryModel model);

        Task<int> Delete(string id);
    }
}