using Warehouse.Common;
using Warehouse.Model.Inward;

namespace Warehouse.Service
{
    public interface IInwardService
    {
        Task<RepositoryResponse> Create(InwardGridModel model);

        Task<RepositoryResponse> Update(string id, InwardGridModel model);

        Task<int> Delete(string id);

        Task<Data.Entities.Inward> GetById(string? id);

        Task<ApiResult<InwardGridModel>> GetByInwardId(string id);
    }
}