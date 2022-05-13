using Warehouse.Common;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.Service
{
    public interface IWareHouseItemCategoryService
    {
        Task<IEnumerable<Data.Entities.WareHouseItemCategory>> GetAll();

        Task<ApiResult<Pagination<WareHouseItemCategoryModel>>> GetAllPaging(GetWareHouseItemCategoryPagingRequest request);

        Task<Data.Entities.WareHouseItemCategory> GetById(string? id);

        Task<RepositoryResponse> Create(WareHouseItemCategoryModel model);

        Task<RepositoryResponse> Update(string id, WareHouseItemCategoryModel model);

        Task<int> Delete(string id);

        Task<ApiResult<Data.Entities.WareHouseItemCategory>> GetByIdAsyn(string id);

        IList<Data.Entities.WareHouseItemCategory> GetMvcListItems(bool showHidden = true);
    }
}