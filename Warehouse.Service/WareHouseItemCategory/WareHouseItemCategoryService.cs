using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.Service
{
    public class WareHouseItemCategoryService : IWareHouseItemCategoryService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public WareHouseItemCategoryService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Data.Entities.WareHouseItemCategory>> GetByIdAsyn(string id)
        {
            var item = await _context.WareHouseItemCategories
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var userViewModel = new Data.Entities.WareHouseItemCategory()
            {
                Name = item.Name,
                Code = item.Code,
                Description = item.Description,
                ParentId = item.ParentId,
                Path = item.Path,
                Inactive = item.Inactive,
                Id = item.Id
            };
            return new ApiSuccessResult<Data.Entities.WareHouseItemCategory>(userViewModel);
        }

        public async Task<IEnumerable<Data.Entities.WareHouseItemCategory>> GetAll()
        {
            return await _context.WareHouseItemCategories
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<WareHouseItemCategoryModel>>> GetAllPaging(GetWareHouseItemCategoryPagingRequest request)
        {
            var query = from pr in _context.WareHouseItemCategories
                        join c in _context.WareHouseItemCategories on pr.ParentId equals c.Id into pt
                        from tp in pt.DefaultIfEmpty()
                        select new { pr, tp };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pr.Name.Contains(request.Keyword)
                || x.pr.Description.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.CategoryId))
            {
                query = query.Where(x => x.tp.ParentId == request.CategoryId);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new WareHouseItemCategoryModel()
                {
                    Id = u.pr.Id,
                    Description = u.pr.Description,
                    Name = u.pr.Name,
                    Code = u.pr.Code,
                    ParentId = u.tp.Name,
                    Inactive = u.pr.Inactive,
                    Path = u.pr.Path,
                })
                .ToListAsync();

            var pagination = new Pagination<WareHouseItemCategoryModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return new ApiSuccessResult<Pagination<WareHouseItemCategoryModel>>(pagination);
        }

        public async Task<Data.Entities.WareHouseItemCategory?> GetById(string? id)
        {
            var item = await _context.WareHouseItemCategories
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public IList<Data.Entities.WareHouseItemCategory> GetMvcListItems(bool showHidden = true)
        {
            var query = from p in _context.WareHouseItemCategories.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Inactive select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(WareHouseItemCategoryModel model)
        {
            Data.Entities.WareHouseItemCategory item = new Data.Entities.WareHouseItemCategory()
            {
                Name = model.Name,
                Code = model.Code,
                Description = model.Description,
                ParentId = model.ParentId,
                Path = model.Path,
                Inactive = model.Inactive
            };
            item.Id = Guid.NewGuid().ToString();

            _context.WareHouseItemCategories.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, WareHouseItemCategoryModel model)
        {
            var item = await _context.WareHouseItemCategories.FindAsync(id);
            item.Name = model.Name;
            item.Code = model.Code;
            item.Description = model.Description;
            item.ParentId = model.ParentId;
            item.Path = model.Path;
            item.Inactive = model.Inactive;

            _context.WareHouseItemCategories.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.WareHouseItemCategories.FindAsync(id);

            _context.WareHouseItemCategories.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}