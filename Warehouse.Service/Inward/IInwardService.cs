using Warehouse.Common;
using Warehouse.Model.Inward;
using Warehouse.Model.InwardDetail;

namespace Warehouse.Service
{
    public interface IInwardService
    {
        Task<RepositoryResponse> Create(InwardModel inwardModel, IList<InwardDetailModel> detailModel);

        Task<RepositoryResponse> Update(string id, InwardGridModel model);

        Task<int> Delete(string id);

        Task<Data.Entities.Inward> GetById(string? id);

        Task<ApiResult<InwardGridModel>> GetByInwardId(string id);
    }
}