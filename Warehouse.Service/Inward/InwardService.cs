using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Inward;

namespace Warehouse.Service
{
    public class InwardService : IInwardService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public InwardService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<InwardGridModel>> GetByInwardId(string id)
        {
            var inward = await _context.Inwards.FindAsync(id);
            var inwardDetail = await _context.InwardDetails.FirstOrDefaultAsync(x => x.ItemId == id);

            var inwardViewModel = new InwardGridModel()
            {
                Voucher = inward.Voucher,
                CustomerId = inwardDetail.CustomerId,
                AccountMore = inwardDetail.AccountMore,
                Description=inward.Description,
                AccountYes = inwardDetail.AccountYes,
                Amount=inwardDetail.Amount,
                CreatedBy=inward.CreatedBy,
                CreatedDate=inward.CreatedDate,
                CustomerName=inwardDetail.CustomerName,
                Deliver=inward.Deliver,
                DeliverAddress=inward.DeliverAddress,
                DeliverPhone=inward.DeliverPhone,
                DeliverDepartment = inward.DeliverDepartment,
                DepartmentId=inwardDetail.DepartmentId,
                DepartmentName=inwardDetail.DepartmentName,
                EmployeeId=inwardDetail.EmployeeId,
                EmployeeName=inwardDetail.EmployeeName,
                InwardId=inwardDetail.InwardId,
                ItemId=inwardDetail.ItemId,
                ModifiedBy=inward.ModifiedBy,
                ModifiedDate=inward.ModifiedDate,
                Price=inwardDetail.Price,
                ProjectId=inwardDetail.ProjectId,
                ProjectName=inwardDetail.ProjectName,
                Quantity=inwardDetail.Quantity,
                Reason=inward.Reason,
                ReasonDescription=inward.ReasonDescription,
                Receiver=inward.Receiver,
                ReceiverAddress=inward.ReceiverAddress,
                ReceiverDepartment=inward.ReceiverDepartment,
                ReceiverPhone=inward.ReceiverPhone,
                Reference=inward.Reference,
                StationId=inwardDetail.StationId,
                StationName=inwardDetail.StationName,
                Status=inwardDetail.Status,
                Uiprice=inwardDetail.Uiprice,
                Uiquantity=inwardDetail.Uiquantity,
                UnitId=inwardDetail.UnitId,
                VendorId=inward.VendorId,
                VoucherCode=inward.VoucherCode,
                VoucherDate=inward.VoucherDate,
                WareHouseId=inward.WareHouseId
            };
            return new ApiSuccessResult<InwardGridModel>(inwardViewModel);
        }

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

        public async Task<RepositoryResponse> Create(InwardGridModel model)
        {
            Data.Entities.Inward inward = new Data.Entities.Inward()
            {
                VoucherCode = model.VoucherCode,
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
                ReceiverAddress = model.ReceiverAddress,
                ReceiverDepartment = model.ReceiverDepartment,
                ReceiverPhone = model.ReceiverPhone,
                Receiver = model.Receiver,
                Reference = model.Reference,
                VendorId = model.VendorId,
                Voucher = model.Voucher,
                VoucherDate = model.VoucherDate,
                WareHouseId = model.WareHouseId
            };

            Data.Entities.InwardDetail inwardDetail = new Data.Entities.InwardDetail()
            {
                EmployeeName = model.EmployeeName,
                DepartmentId = model.DepartmentId,
                CustomerName = model.CustomerName,
                CustomerId = model.CustomerId,
                Amount = model.Amount,
                EmployeeId = model.EmployeeId,
                AccountMore = model.AccountMore,
                AccountYes = model.AccountYes,
                DepartmentName = model.DepartmentName,
                InwardId = model.InwardId,
                ItemId = model.ItemId,
                Price = model.Price,
                ProjectId = model.ProjectId,
                ProjectName = model.ProjectName,
                Quantity = model.Quantity,
                StationId = model.StationId,
                StationName = model.StationName,
                Status = model.Status,
                Uiprice = model.Uiprice,
                Uiquantity = model.Uiquantity,
                UnitId = model.UnitId
            };
            inward.Id = Guid.NewGuid().ToString();
            inwardDetail.Id = Guid.NewGuid().ToString();

            inwardDetail.InwardId = inward.Id;

            _context.Inwards.Add(inward);
            _context.InwardDetails.Add(inwardDetail);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = inward.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, InwardGridModel model)
        {
            var inward = await _context.Inwards.FindAsync(id);
            var inwardDetail = await _context.InwardDetails.FirstOrDefaultAsync(x => x.InwardId == model.Id);

            inward.VoucherCode = model.VoucherCode;
            inward.VoucherDate = model.VoucherDate;
            inward.WareHouseId = model.WareHouseId;
            inward.Deliver = model.Deliver;
            inward.Receiver = model.Receiver;
            inward.VendorId = model.VendorId;
            inward.Reason = model.Reason;
            inward.ReasonDescription = model.ReasonDescription;
            inward.Description = model.Description;
            inward.Reference = model.Reference;
            inward.CreatedDate = model.CreatedDate;
            inward.CreatedBy = model.CreatedBy;
            inward.ModifiedDate = model.ModifiedDate;
            inward.ModifiedBy = model.ModifiedBy;
            inward.DeliverPhone = model.DeliverPhone;
            inward.DeliverAddress = model.DeliverAddress;
            inward.DeliverDepartment = model.DeliverDepartment;
            inward.ReceiverPhone = model.ReceiverPhone;
            inward.ReceiverAddress = model.ReceiverAddress;
            inward.ReceiverDepartment = model.ReceiverDepartment;
            inward.Voucher = model.Voucher;
            inwardDetail.InwardId = model.InwardId;
            inwardDetail.ItemId = model.ItemId;
            inwardDetail.UnitId = model.UnitId;
            inwardDetail.Uiquantity = model.Uiquantity;
            inwardDetail.Uiprice = model.Uiprice;
            inwardDetail.Amount = model.Amount;
            inwardDetail.Quantity = model.Quantity;
            inwardDetail.Price = model.Price;
            inwardDetail.DepartmentId = model.DepartmentId;
            inwardDetail.DepartmentName = model.DepartmentName;
            inwardDetail.EmployeeId = model.EmployeeId;
            inwardDetail.EmployeeName = model.EmployeeName;
            inwardDetail.StationId = model.StationId;
            inwardDetail.StationName = model.StationName;
            inwardDetail.ProjectId = model.ProjectId;
            inwardDetail.ProjectName = model.ProjectName;
            inwardDetail.CustomerId = model.CustomerId;
            inwardDetail.CustomerName = model.CustomerName;
            inwardDetail.AccountMore = model.AccountMore;
            inwardDetail.AccountYes = model.AccountYes;
            inwardDetail.Status = model.Status;

            _context.Inwards.Update(inward);
            _context.InwardDetails.Update(inwardDetail);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var inward = await _context.Inwards.FindAsync(id);
            if (inward == null) throw new WarehouseException($"Cannot find a inward: {id}");

            var images = _context.InwardDetails.Where(i => i.InwardId == id);

            foreach (var image in images)
            {
                _context.InwardDetails.Remove(image);
            }

            _context.Inwards.Remove(inward);

            return await _context.SaveChangesAsync();
        }

        #endregion Method
    }
}