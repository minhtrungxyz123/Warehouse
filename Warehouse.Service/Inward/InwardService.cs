using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Data.Entities;
using Warehouse.Model.Inward;
using Warehouse.Model.InwardDetail;

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
                Description = inward.Description,
                AccountYes = inwardDetail.AccountYes,
                Amount = inwardDetail.Amount,
                CreatedBy = inward.CreatedBy,
                CreatedDate = inward.CreatedDate,
                CustomerName = inwardDetail.CustomerName,
                Deliver = inward.Deliver,
                DeliverAddress = inward.DeliverAddress,
                DeliverPhone = inward.DeliverPhone,
                DeliverDepartment = inward.DeliverDepartment,
                DepartmentId = inwardDetail.DepartmentId,
                DepartmentName = inwardDetail.DepartmentName,
                EmployeeId = inwardDetail.EmployeeId,
                EmployeeName = inwardDetail.EmployeeName,
                InwardId = inwardDetail.InwardId,
                ItemId = inwardDetail.ItemId,
                ModifiedBy = inward.ModifiedBy,
                ModifiedDate = inward.ModifiedDate,
                Price = inwardDetail.Price,
                ProjectId = inwardDetail.ProjectId,
                ProjectName = inwardDetail.ProjectName,
                Quantity = inwardDetail.Quantity,
                Reason = inward.Reason,
                ReasonDescription = inward.ReasonDescription,
                Receiver = inward.Receiver,
                ReceiverAddress = inward.ReceiverAddress,
                ReceiverDepartment = inward.ReceiverDepartment,
                ReceiverPhone = inward.ReceiverPhone,
                Reference = inward.Reference,
                StationId = inwardDetail.StationId,
                StationName = inwardDetail.StationName,
                Status = inwardDetail.Status,
                Uiprice = inwardDetail.Uiprice,
                Uiquantity = inwardDetail.Uiquantity,
                UnitId = inwardDetail.UnitId,
                VendorId = inward.VendorId,
                VoucherCode = inward.VoucherCode,
                VoucherDate = inward.VoucherDate,
                WareHouseId = inward.WareHouseId
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

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(InwardModel inwardModel, IList<InwardDetailModel> detailModel)
        {
            Data.Entities.Inward inward = new Data.Entities.Inward()
            {
                VoucherCode = inwardModel.VoucherCode,
                CreatedBy = inwardModel.CreatedBy,
                CreatedDate = inwardModel.CreatedDate,
                Deliver = inwardModel.Deliver,
                DeliverAddress = inwardModel.DeliverAddress,
                DeliverDepartment = inwardModel.DeliverDepartment,
                DeliverPhone = inwardModel.DeliverPhone,
                Description = inwardModel.Description,
                ModifiedBy = inwardModel.ModifiedBy,
                ModifiedDate = inwardModel.ModifiedDate,
                Reason = inwardModel.Reason,
                ReasonDescription = inwardModel.ReasonDescription,
                ReceiverAddress = inwardModel.ReceiverAddress,
                ReceiverDepartment = inwardModel.ReceiverDepartment,
                ReceiverPhone = inwardModel.ReceiverPhone,
                Receiver = inwardModel.Receiver,
                Reference = inwardModel.Reference,
                VendorId = inwardModel.VendorId,
                Voucher = inwardModel.Voucher,
                VoucherDate = inwardModel.VoucherDate,
                WareHouseId = inwardModel.WareHouseId,
            };
            inward.Id = Guid.NewGuid().ToString();

            if (detailModel != null)
            {
                var list = new List<InwardDetail>();
                foreach (var item in detailModel)
                {
                  var inwardDetail = new Data.Entities.InwardDetail()
                    {
                        EmployeeName = item.EmployeeName,
                        DepartmentId = item.DepartmentId,
                        CustomerName = item.CustomerName,
                        CustomerId = item.CustomerId,
                        Amount = item.Amount,
                        EmployeeId = item.EmployeeId,
                        AccountMore = item.AccountMore,
                        AccountYes = item.AccountYes,
                        DepartmentName = item.DepartmentName,
                        InwardId = item.InwardId,
                        ItemId = item.ItemId,
                        Price = item.Price,
                        ProjectId = item.ProjectId,
                        ProjectName = item.ProjectName,
                        Quantity = item.Quantity,
                        StationId = item.StationId,
                        StationName = item.StationName,
                        Status = item.Status,
                        Uiprice = item.Uiprice,
                        Uiquantity = item.Uiquantity,
                        UnitId = item.UnitId,
                    };

                    inwardDetail.Id = Guid.NewGuid().ToString();
                    inwardDetail.InwardId = inward.Id;
                    list.Add(inwardDetail);
                }

                await _context.InwardDetails.AddRangeAsync(list);
            }

            
            _context.Inwards.Add(inward);

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