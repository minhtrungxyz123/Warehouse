﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Common.Common;
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
                Path=item.Path,
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

        public async Task<ApiResult<Pagination<Data.Entities.WareHouseItemCategory>>> GetAllPaging(GetWareHouseItemCategoryPagingRequest request)
        {
            var query = _context.WareHouseItemCategories.AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new Data.Entities.WareHouseItemCategory()
                {
                    Name = x.Name,
                    Path = x.Path,
                    ParentId = x.ParentId,
                    Description = x.Description,
                    Code=x.Code,
                    Inactive = x.Inactive,
                    Id = x.Id
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new Pagination<Data.Entities.WareHouseItemCategory>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Data.Entities.WareHouseItemCategory>>(pagedResult);
        }

        public async Task<Data.Entities.WareHouseItemCategory?> GetById(string? id)
        {
            var item = await _context.WareHouseItemCategories
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
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