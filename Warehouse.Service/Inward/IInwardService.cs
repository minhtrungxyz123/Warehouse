using Warehouse.Common;
using Warehouse.Model.Inward;

namespace Warehouse.Service
{
    public interface IInwardService
    {
        Task<Data.Entities.Inward> GetById(string? id);

        Task<RepositoryResponse> Create(InwardModel model);

        Task<RepositoryResponse> Update(string id, InwardModel model);

        Task<int> Delete(string id);
    }
}