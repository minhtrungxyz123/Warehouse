using Warehouse.Common;
using Warehouse.Model.WareHouseItem;

namespace Warehouse.Service
{
    public interface IWareHouseItemService
    {
        Task<IEnumerable<Data.Entities.WareHouseItem>> GetAll();

        Task<ApiResult<Pagination<WareHouseItemModel>>> GetAllPaging(GetWareHouseItemPagingRequest request);

        Task<Data.Entities.WareHouseItem> GetById(string? id);

        Task<RepositoryResponse> Create(WareHouseItemModel model);

        Task<RepositoryResponse> Update(string id, WareHouseItemModel model);

        Task<int> Delete(string id);

        Task<ApiResult<Data.Entities.WareHouseItem>> GetByIdAsyn(string id);

        IList<Data.Entities.WareHouseItem> GetMvcListItems(bool showHidden = true);

        Task<Data.Entities.WareHouseItem> GetByIdUnitAsync(string id);

        Task<ApiResult<WareHouseItemModel>> GetByWHItemUnitId(string id);
    }
}