using Warehouse.Common;
using Warehouse.Model.Inward;
using Warehouse.Model.InwardDetail;

namespace Warehouse.Service
{
    public interface IInwardDetailService
    {
        Task<ApiResult<Pagination<InwardGridModel>>> GetAllPaging(GetInwardPagingRequest request);

        Task<Data.Entities.InwardDetail> GetById(string? id);

        Task<Data.Entities.InwardDetail> GetByInwardId(string? inwardId);

        Task<RepositoryResponse> Create(InwardDetailModel model);

        Task<RepositoryResponse> Update(string id, InwardDetailModel model);

        Task<int> Delete(string id);
    }
}