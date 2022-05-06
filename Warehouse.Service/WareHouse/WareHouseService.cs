using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Data.EF;
using Warehouse.Model.WareHouse;

namespace Warehouse.Service
{
    public class WareHouseService : IWareHouseService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public WareHouseService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<IEnumerable<Data.Entities.WareHouse>> GetAll()
        {
            return await _context.WareHouses
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Data.Entities.WareHouse>>> GetAllPaging(GetWareHousePagingRequest request)
        {
            var query = _context.WareHouses.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword)
                || x.Description.Contains(request.Keyword));
            }
            var totalRecords = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();

            var pagination = new Pagination<Data.Entities.WareHouse>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return new ApiSuccessResult<Pagination<Data.Entities.WareHouse>>(pagination);
        }

        public async Task<Data.Entities.WareHouse?> GetById(string? id)
        {
            var item = await _context.WareHouses
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(WareHouseModel model)
        {
            Data.Entities.WareHouse item = new Data.Entities.WareHouse()
            {
                Name = model.Name,
                Address = model.Address,
                Code = model.Code,
                Description = model.Description,
                ParentId = model.ParentId,
                Path = model.Path,
                Inactive = model.Inactive
            };

            item.Id = Guid.NewGuid().ToString();

            _context.WareHouses.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, WareHouseModel model)
        {
            var item = await _context.WareHouses.FindAsync(id);
            item.Name = model.Name;
            item.Address = model.Address;
            item.Code = model.Code;
            item.Description = model.Description;
            item.ParentId = model.ParentId;
            item.Path = model.Path;
            item.Inactive = model.Inactive;

            _context.WareHouses.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.WareHouses.FindAsync(id);

            _context.WareHouses.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}