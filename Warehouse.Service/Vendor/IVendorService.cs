using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Vendor;

namespace Warehouse.Service.Vendor
{
    public interface IVendorService
    {
        Task<IEnumerable<Data.Entities.Vendor>> GetAll();

        Task<ApiResult<Pagination<Data.Entities.Vendor>>> GetAllPaging(GetVendorPagingRequest request);

        Task<Data.Entities.Vendor> GetById(string? id);

        Task<RepositoryResponse> Create(VendorModel model);

        Task<RepositoryResponse> Update(string id, VendorModel model);

        Task<int> Delete(string id);
    }
}