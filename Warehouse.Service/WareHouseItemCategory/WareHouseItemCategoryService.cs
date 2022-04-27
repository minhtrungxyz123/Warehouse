﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.Service.WareHouseItemCategory
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

        public async Task<IEnumerable<Data.Entities.WareHouseItemCategory>> GetAll()
        {
            return await _context.WareHouseItemCategories
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<Pagination<Data.Entities.WareHouseItemCategory>> GetAllPaging(string? search, int pageIndex, int pageSize)
        {
            var query = _context.WareHouseItemCategories.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search)
                || x.Name.Contains(search));
            }
            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagination = new Pagination<Data.Entities.WareHouseItemCategory>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            return pagination;
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
                Inactive = model.Inactive, 
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