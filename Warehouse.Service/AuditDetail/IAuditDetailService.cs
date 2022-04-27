using Warehouse.Common;
using Warehouse.Model.Audit;
using Warehouse.Model.AuditDetail;

namespace Warehouse.Service.AuditDetail
{
    public interface IAuditDetailService
    {
        Task<Pagination<AuditGridModel>> GetAllPaging(string? search, Guid? wahouseId, int pageIndex, int pageSize);

        Task<Data.Entities.AuditDetail> GetById(string? id);

        Task<Data.Entities.AuditDetail> GetByAuditId(string? auditId);

        Task<RepositoryResponse> Create(AuditDetailModel model);

        Task<RepositoryResponse> Update(string id, AuditDetailModel model);

        Task<int> Delete(string id);
    }
}