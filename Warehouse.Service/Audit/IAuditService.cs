using Warehouse.Common;
using Warehouse.Model.Audit;

namespace Warehouse.Service
{
    public interface IAuditService
    {
        Task<IEnumerable<Data.Entities.Audit>> GetAll();

        Task<Data.Entities.Audit> GetById(string? id);

        Task<RepositoryResponse> Create(AuditModel model);

        Task<RepositoryResponse> Update(string id, AuditModel model);

        Task<int> Delete(string id);
    }
}