using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Unit;

namespace Warehouse.Service
{
    public class UnitService : IUnitService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public UnitService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Data.Entities.Unit>> GetByIdAsyn(string id)
        {
            var item = await _context.Units
                            .OrderByDescending(p => p.UnitName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var userViewModel = new Data.Entities.Unit()
            {
                UnitName = item.UnitName,
                Inactive = item.Inactive,
                Id = item.Id
            };
            return new ApiSuccessResult<Data.Entities.Unit>(userViewModel);
        }

        public async Task<IEnumerable<Data.Entities.Unit>> GetAll()
        {
            return await _context.Units
                            .OrderByDescending(p => p.UnitName)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Data.Entities.Unit>>> GetAllPaging(GetUnitPagingRequest request)
        {
            var query = _context.Units.AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UnitName.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new Data.Entities.Unit()
                {
                    UnitName = x.UnitName,
                    Inactive = x.Inactive,
                    Id = x.Id
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new Pagination<Data.Entities.Unit>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Data.Entities.Unit>>(pagedResult);
        }

        public async Task<Data.Entities.Unit?> GetById(string? id)
        {
            var item = await _context.Units
                            .OrderByDescending(p => p.UnitName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public IList<Data.Entities.Unit> GetMvcListItems(bool showHidden = true)
        {
            var query = from p in _context.Units.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Inactive select p;
            }
            query = from p in query orderby p.UnitName select p;
            return query.ToList();
        }

        #endregion List

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

        #endregion Method
    }
}