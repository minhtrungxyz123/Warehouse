using Warehouse.Common;
using Warehouse.Model.Audit;
using Warehouse.Model.AuditDetail;

namespace Warehouse.Service
{
    public interface IAuditDetailService
    {
        Task<ApiResult<Pagination<AuditGridModel>>> GetAllPaging(GetAuditPagingRequest request);

        Task<Data.Entities.AuditDetail> GetById(string? id);

        Task<Data.Entities.AuditDetail> GetByAuditId(string? auditId);

        Task<RepositoryResponse> Create(AuditDetailModel model);

        Task<RepositoryResponse> Update(string id, AuditDetailModel model);

        Task<int> Delete(string id);
    }
}