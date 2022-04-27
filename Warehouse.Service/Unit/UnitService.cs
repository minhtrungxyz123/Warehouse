using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Unit;

namespace Warehouse.Service.Unit
{
    public class UnitService : IUnitService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public UnitService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion

        #region List

        public async Task<IEnumerable<Data.Entities.Unit>> GetAll()
        {
            return await _context.Units
                            .OrderByDescending(p => p.UnitName)
                            .ToListAsync();
        }

        public async Task<Pagination<Data.Entities.Unit>> GetAllPaging(string? search, int pageIndex, int pageSize)
        {
            var query = _context.Units.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.UnitName.Contains(search)
                || x.UnitName.Contains(search));
            }
            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagination = new Pagination<Data.Entities.Unit>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            return pagination;
        }

        public async Task<Data.Entities.Unit?> GetById(string? id)
        {
            var item = await _context.Units
                            .OrderByDescending(p => p.UnitName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion

        #region Method

        public async Task<RepositoryResponse> Create(UnitModel model)
        {
            Data.Entities.Unit item = new Data.Entities.Unit()
            {
                UnitName = model.UnitName,
                Inactive = model.Inactive
            };
            item.Id = Guid.NewGuid().ToString();

            _context.Units.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, UnitModel model)
        {
            var item = await _context.Units.FindAsync(id);
            item.UnitName = model.UnitName;
            item.Inactive = model.Inactive;

            _context.Units.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Units.FindAsync(id);

            _context.Units.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion
    }
}