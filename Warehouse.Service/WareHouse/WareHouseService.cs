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

        public async Task<ApiResult<Data.Entities.WareHouse>> GetByIdAsyn(string id)
        {
            var item = await _context.WareHouses
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var userViewModel = new Data.Entities.WareHouse()
            {
                Name = item.Name,
                Inactive = item.Inactive,
                Id = item.Id,
                Code= item.Code,
                Path = item.Path,
                Address = item.Address,
                ParentId = item.ParentId,
                Description = item.Description
            };
            return new ApiSuccessResult<Data.Entities.WareHouse>(userViewModel);
        }

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
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new Data.Entities.WareHouse()
                {
                    Name = x.Name,
                    Inactive = x.Inactive,
                    Id = x.Id,
                    Description = x.Description,
                    ParentId = x.ParentId,
                    Address = x.Address,
                    Path = x.Path,
                    Code=x.Code,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new Pagination<Data.Entities.WareHouse>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Data.Entities.WareHouse>>(pagedResult);
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
                Inactive = model.Inactive,
                Address = model.Address,
                Code = model.Code,
                ParentId = model.ParentId,
                Description = model.Description
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
            item.Inactive = model.Inactive;
            item.Address = model.Address;
            item.Code = model.Code;
            item.ParentId = model.ParentId;
            item.Description = model.Description;

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