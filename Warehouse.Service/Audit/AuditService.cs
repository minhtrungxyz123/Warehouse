using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Audit;

namespace Warehouse.Service
{
    public class AuditService : IAuditService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public AuditService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<IEnumerable<Data.Entities.Audit>> GetAll()
        {
            return await _context.Audits
                            .OrderByDescending(p => p.Description)
                            .ToListAsync();
        }

        public async Task<Data.Entities.Audit?> GetById(string? id)
        {
            var item = await _context.Audits
                            .OrderByDescending(p => p.Description)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(AuditModel model)
        {
            Data.Entities.Audit item = new Data.Entities.Audit()
            {
                VoucherCode = model.VoucherCode,
                Description = model.Description,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                VoucherDate = model.VoucherDate,
                WareHouseId = model.WareHouseId
            };
            item.Id = Guid.NewGuid().ToString();

            _context.Audits.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, AuditModel model)
        {
            var item = await _context.Audits.FindAsync(id);
            item.Description = model.Description;
            item.VoucherCode = model.VoucherCode;
            item.CreatedBy = model.CreatedBy;
            item.CreatedDate = model.CreatedDate;
            item.ModifiedBy = model.ModifiedBy;
            item.ModifiedDate = model.ModifiedDate;
            item.VoucherDate = model.VoucherDate;
            item.WareHouseId = model.WareHouseId;

            _context.Audits.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Audits.FindAsync(id);

            _context.Audits.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}