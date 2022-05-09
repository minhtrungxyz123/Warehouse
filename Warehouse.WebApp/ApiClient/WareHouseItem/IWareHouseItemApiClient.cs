using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Unit;
using Warehouse.Model.Vendor;
using Warehouse.Model.WareHouseItem;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.WebApp.ApiClient
{
    public interface IWareHouseItemApiClient
    {
        public Task<bool> Create(WareHouseItemModel request);

        public Task<bool> Edit(string id, WareHouseItemModel request);

        Task<ApiResult<Pagination<WareHouseItemModel>>> GetPagings(GetWareHouseItemPagingRequest request);

        Task<ApiResult<WareHouseItemModel>> GetById(string id);

        Task<IList<UnitModel>> GetAvailableList(bool showHidden = true);

        Task<IList<VendorModel>> GetVendor(bool showHidden = true);

        Task<IList<WareHouseItemCategoryModel>> GetCategory(bool showHidden = true);

        Task<bool> Delete(string id);

        Task<WareHouseItemModel> GetByIdAync(string id);
    }
}
