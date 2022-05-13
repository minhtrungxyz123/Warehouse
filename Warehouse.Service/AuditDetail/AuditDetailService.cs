using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Audit;
using Warehouse.Model.AuditDetail;

namespace Warehouse.Service
{
    public class AuditDetailService : IAuditDetailService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public AuditDetailService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Pagination<AuditGridModel>>> GetAllPaging(GetAuditPagingRequest request)
        {
            var query = from pr in _context.AuditDetails
                        join c in _context.Audits on pr.AuditId equals c.Id into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.WareHouses on tp.WareHouseId equals w.Id into wt
                        from tw in wt.DefaultIfEmpty()
                        join i in _context.WareHouseItems on pr.ItemId equals i.Id into it
                        from ti in it.DefaultIfEmpty()
                        select new { pr, tp, tw, ti };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.tp.Description.Contains(request.Keyword)
                || x.tp.VoucherCode.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.WarehouseId))
            {
                query = query.Where(x => x.tp.WareHouseId == request.WarehouseId);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new AuditGridModel()
                {
                    Id = u.tp.Id,
                    Description = u.tp.Description,
                    VoucherCode = u.tp.VoucherCode,
                    WareHouseId = u.tw.Name,
                    AuditId = u.tp.Id,
                    AuditQuantity = u.pr.AuditQuantity,
                    VoucherDate = u.tp.VoucherDate,
                    CreatedDate = u.tp.CreatedDate,
                    CreatedBy = u.tp.CreatedBy,
                    ItemId = u.ti.Name,
                    Quantity = u.pr.Quantity,
                    Conclude = u.pr.Conclude,
                })
                .ToListAsync();

            var pagination = new Pagination<AuditGridModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return new ApiSuccessResult<Pagination<AuditGridModel>>(pagination);
        }

        public async Task<Data.Entities.AuditDetail?> GetById(string? id)
        {
            var item = await _context.AuditDetails
                            .OrderByDescending(p => p.Conclude)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }
        public async Task<Data.Entities.AuditDetail> GetByAuditId(string? auditId)
        {
            if (string.IsNullOrEmpty(auditId))
            {
                throw new ArgumentNullException(nameof(auditId));
            }

            var result = await _context.AuditDetails
                .FirstOrDefaultAsync(x => x.AuditId == auditId);

            return result;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(AuditDetailModel model)
        {
            Data.Entities.AuditDetail item = new Data.Entities.AuditDetail()
            {
                AuditQuantity = model.AuditQuantity,
                ItemId = model.ItemId,
                Conclude=model.Conclude,
                AuditId = model.AuditId,
                Quantity = model.Quantity,
            };
            item.Id = Guid.NewGuid().ToString();

            _context.AuditDetails.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, AuditDetailModel model)
        {
            var item = await _context.AuditDetails.FindAsync(id);
            item.AuditQuantity = model.AuditQuantity;
            item.ItemId = model.ItemId;
            item.Conclude = model.Conclude;
            item.Quantity = model.Quantity;
            

            _context.AuditDetails.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.AuditDetails.FindAsync(id);

            _context.AuditDetails.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}