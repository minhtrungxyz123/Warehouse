using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
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

        public async Task<ApiResult<Pagination<WareHouseModel>>> GetAllPaging(GetWareHousePagingRequest request)
        {
            var query = from pr in _context.WareHouses
                        join c in _context.WareHouses on pr.ParentId equals c.Id into pt
                        from tp in pt.DefaultIfEmpty()
                        select new { pr, tp };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pr.Name.Contains(request.Keyword)
                || x.pr.Description.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.ParentId))
            {
                query = query.Where(x => x.tp.ParentId == request.ParentId);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new WareHouseModel()
                {
                    Id = u.pr.Id,
                    Description = u.pr.Description,
                    Name = u.pr.Name,
                    Code = u.pr.Code,
                    ParentId = u.tp.Name,
                    Inactive = u.pr.Inactive,
                    Path = u.pr.Path,
                    Address = u.pr.Address,
                })
                .ToListAsync();

            var pagination = new Pagination<WareHouseModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return new ApiSuccessResult<Pagination<WareHouseModel>>(pagination);
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

        public IList<Data.Entities.WareHouse> GetMvcListItems(bool showHidden = true)
        {
            var query = from p in _context.WareHouses.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Inactive select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        #endregion Method
    }
}