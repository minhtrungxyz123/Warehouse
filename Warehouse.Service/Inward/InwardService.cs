using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Inward;

namespace Warehouse.Service.Inward
{
    public class InwardService : IInwardService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public InwardService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion

        #region List
        public async Task<Data.Entities.Inward?> GetById(string? id)
        {
            var item = await _context.Inwards
                            .OrderByDescending(p => p.Description)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion

        #region Method

        public async Task<RepositoryResponse> Create(InwardModel model)
        {
            Data.Entities.Inward item = new Data.Entities.Inward()
            {
                VoucherCode = model.VoucherCode,
                Voucher = model.Voucher,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Deliver = model.Deliver,
                DeliverAddress = model.DeliverAddress,
                DeliverDepartment = model.DeliverDepartment,
                DeliverPhone = model.DeliverPhone,
                Description = model.Description,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                Reason = model.Reason,
                ReasonDescription = model.ReasonDescription,
                Receiver = model.Receiver,
                ReceiverAddress = model.ReceiverAddress,
                ReceiverDepartment = model.ReceiverDepartment,
                ReceiverPhone = model.ReceiverPhone,
                Reference = model.Reference,
                VendorId = model.VendorId,
                VoucherDate = model.VoucherDate,
                WareHouseId = model.WareHouseId
            };
            item.Id = Guid.NewGuid().ToString();

            _context.Inwards.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, InwardModel model)
        {
            var item = await _context.Inwards.FindAsync(id);
            item.VoucherCode = model.VoucherCode;
            item.Voucher = model.Voucher;
            item.VoucherDate = model.VoucherDate;
            item.WareHouseId = model.WareHouseId;
            item.VendorId = model.VendorId;
            item.WareHouseId = model.WareHouseId;
            item.VoucherDate = model.VoucherDate;
            item.Reference = model.Reference;
            item.CreatedBy = model.CreatedBy;
            item.CreatedDate = model.CreatedDate;
            item.CreatedDate = model.CreatedDate;
            item.Deliver = model.Deliver;
            item.DeliverAddress = model.DeliverAddress;
            item.DeliverPhone = model.DeliverPhone;
            item.ReceiverPhone = model.ReceiverPhone;
            item.ReceiverAddress = model.ReceiverAddress;
            item.ReceiverAddress = model.ReceiverAddress;
            item.ReceiverDepartment = model.ReceiverDepartment;
            item.ReceiverPhone= model.ReceiverPhone;
            item.Description = model.Description; 

            _context.Inwards.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Inwards.FindAsync(id);

            _context.Inwards.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion
    }
}
