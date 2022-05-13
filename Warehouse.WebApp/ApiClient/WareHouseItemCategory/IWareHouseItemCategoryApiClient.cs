using Warehouse.Common;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.WebApp.ApiClient
{
    public interface IWareHouseItemCategoryApiClient
    {
        public Task<bool> Create(WareHouseItemCategoryModel request);

        public Task<bool> Edit(string id, WareHouseItemCategoryModel request);

        Task<ApiResult<Pagination<WareHouseItemCategoryModel>>> GetPagings(GetWareHouseItemCategoryPagingRequest request);

        Task<ApiResult<WareHouseItemCategoryModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<IList<WareHouseItemCategoryModel>> GetAvailableList(bool showHidden = true);
    }
}
