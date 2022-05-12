using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Data.Entities;
using Warehouse.Model.CreatedBy;

namespace Master.Service
{
    public interface ICreatedByService
    {
        public CreatedBy User { get; }
        Task<IEnumerable<CreatedBy>> GetAll();

        Task<ApiResult<Pagination<CreatedBy>>> GetAllPaging(GetCreatedByPagingRequest request);

        Task<CreatedBy> GetById(string? id);

        Task<RepositoryResponse> Create(CreatedByModel model);

        Task<RepositoryResponse> Update(string id, CreatedByModel model);

        Task<int> Delete(string unitId);

        Task<ApiResult<CreatedBy>> GetByIdAsyn(string id);
    }
}