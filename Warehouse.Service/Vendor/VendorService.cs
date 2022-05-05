﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Vendor;

namespace Warehouse.Service.Vendor
{
    public class VendorService : IVendorService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public VendorService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<IEnumerable<Data.Entities.Vendor>> GetAll()
        {
            return await _context.Vendors
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Data.Entities.Vendor>>> GetAllPaging(GetVendorPagingRequest request)
        {
            var query = _context.Vendors.AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            // Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new Data.Entities.Vendor()
                {
                    Name = x.Name,
                    Address = x.Address,
                    Code = x.Code,
                    ContactPerson = x.ContactPerson,
                    Email = x.Email,
                    Phone = x.Phone,
                    Inactive = x.Inactive,
                    Id = x.Id
                }).ToListAsync();

            //Select and projection
            var pagedResult = new Pagination<Data.Entities.Vendor>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Data.Entities.Vendor>>(pagedResult);
        }

        public async Task<Data.Entities.Vendor?> GetById(string? id)
        {
            var item = await _context.Vendors
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(VendorModel model)
        {
            Data.Entities.Vendor item = new Data.Entities.Vendor()
            {
                Name = model.Name,
                Address = model.Address,
                Code = model.Code,
                ContactPerson = model.ContactPerson,
                Email = model.Email,
                Phone = model.Phone,
                Inactive = model.Inactive
            };
            item.Id = Guid.NewGuid().ToString();

            _context.Vendors.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, VendorModel model)
        {
            var item = await _context.Vendors.FindAsync(id);
            item.Name = model.Name;
            item.Address = model.Address;
            item.Code = model.Code;
            item.ContactPerson = model.ContactPerson;
            item.Email = model.Email;
            item.Phone = model.Phone;
            item.Inactive = model.Inactive;

            _context.Vendors.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Vendors.FindAsync(id);

            _context.Vendors.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}