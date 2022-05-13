using Microsoft.EntityFrameworkCore;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.Inward;
using Warehouse.Model.InwardDetail;

namespace Warehouse.Service
{
    public class InwardDetailService : IInwardDetailService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public InwardDetailService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Pagination<InwardGridModel>>> GetAllPaging(GetInwardPagingRequest request)
        {
            var query = from pr in _context.InwardDetails
                        join c in _context.Inwards on pr.InwardId equals c.Id into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.WareHouses on tp.WareHouseId equals w.Id into wt
                        from tw in wt.DefaultIfEmpty()
                        join i in _context.WareHouseItems on pr.ItemId equals i.Id into it
                        from ti in it.DefaultIfEmpty()
                        join v in _context.Vendors on tp.VendorId equals v.Id into vt
                        from tv in vt.DefaultIfEmpty()
                        select new { pr, tp, tw, ti, tv };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.tp.Description.Contains(request.Keyword)
                || x.tp.VoucherCode.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.WarehouseId))
            {
                query = query.Where(x => x.tp.WareHouseId == request.WarehouseId);
            }

            if (!string.IsNullOrEmpty(request.VendorId))
            {
                query = query.Where(x => x.tp.VendorId == request.VendorId);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new InwardGridModel()
                {
                    Id = u.tp.Id,
                    Description = u.tp.Description,
                    VoucherCode = u.tp.VoucherCode,
                    WareHouseId = u.tw.Name,
                    InwardId = u.tp.Id,
                    VoucherDate = u.tp.VoucherDate,
                    CreatedDate = u.tp.CreatedDate,
                    CreatedBy = u.tp.CreatedBy,
                    ItemId = u.ti.Name,
                    Quantity = u.pr.Quantity,
                    VendorId = u.tv.Name,
                    AccountMore = u.pr.AccountMore,
                    AccountYes = u.pr.AccountYes,
                    Amount = u.pr.Amount,
                    CustomerId = u.pr.CustomerId,
                    CustomerName = u.pr.CustomerName,
                    Deliver = u.tp.Deliver,
                    DeliverAddress = u.tp.DeliverAddress,
                    DeliverDepartment = u.tp.DeliverDepartment,
                    DeliverPhone = u.tp.DeliverPhone,
                    DepartmentId = u.pr.DepartmentId,
                    DepartmentName = u.pr.DepartmentName,
                    EmployeeId = u.pr.EmployeeId,
                    EmployeeName = u.pr.EmployeeName,
                    ModifiedBy = u.tp.ModifiedBy,
                    ModifiedDate = u.tp.ModifiedDate,
                    Price = u.pr.Price,
                    ProjectId = u.pr.ProjectId,
                    ProjectName = u.pr.ProjectName,
                    Reason = u.tp.Reason,
                    ReasonDescription = u.tp.ReasonDescription,
                    Receiver = u.tp.Receiver,
                    ReceiverAddress = u.tp.ReceiverAddress,
                    ReceiverDepartment = u.tp.ReceiverDepartment,
                    ReceiverPhone = u.tp.ReceiverPhone,
                    Reference = u.tp.Reference,
                    StationId = u.pr.StationId,
                    StationName = u.pr.StationName,
                    Status = u.pr.Status,
                    Uiprice = u.pr.Uiprice,
                    Uiquantity = u.pr.Uiquantity,
                    UnitId = u.pr.UnitId,
                    Voucher = u.tp.Voucher
                })
                .ToListAsync();

            var pagination = new Pagination<InwardGridModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return new ApiSuccessResult<Pagination<InwardGridModel>>(pagination);
        }

        public async Task<Data.Entities.InwardDetail?> GetById(string? id)
        {
            var item = await _context.InwardDetails
                            .OrderByDescending(p => p.Amount)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public async Task<Data.Entities.InwardDetail> GetByInwardId(string? inwardId)
        {
            if (string.IsNullOrEmpty(inwardId))
            {
                throw new ArgumentNullException(nameof(inwardId));
            }

            var result = await _context.InwardDetails
                .FirstOrDefaultAsync(x => x.InwardId == inwardId);

            return result;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(InwardDetailModel model)
        {
            Data.Entities.InwardDetail item = new Data.Entities.InwardDetail()
            {
                AccountMore = model.AccountMore,
                ItemId = model.ItemId,
                AccountYes = model.AccountYes,
                InwardId = model.InwardId,
                Quantity = model.Quantity,
                Amount = model.Amount,
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                DepartmentId = model.DepartmentId,
                DepartmentName = model.DepartmentName,
                EmployeeId = model.EmployeeId,
                EmployeeName = model.EmployeeName,
                Price = model.Price,
                ProjectId = model.ProjectId,
                ProjectName = model.ProjectName,
                StationId = model.StationId,
                StationName = model.StationName,
                Status = model.Status,
                Uiprice = model.Uiprice,
                Uiquantity = model.Uiquantity,
                UnitId = model.UnitId
            };
            item.Id = Guid.NewGuid().ToString();

            _context.InwardDetails.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, InwardDetailModel model)
        {
            var item = await _context.InwardDetails.FindAsync(id);
            item.ProjectName = model.ProjectName;
            item.ItemId = model.ItemId;
            item.InwardId = model.InwardId;
            item.Quantity = model.Quantity;
            item.Status = model.Status;
            item.Uiprice = model.Uiprice;
            item.Uiquantity = model.Uiquantity;
            item.UnitId = model.UnitId;
            item.AccountMore = model.AccountMore;
            item.Amount = model.Amount;
            item.CustomerId = model.CustomerId;
            item.CustomerName = model.CustomerName;
            item.DepartmentId = model.DepartmentId;
            item.DepartmentName = model.DepartmentName;
            item.ItemId=model.ItemId;
            item.Price = model.Price;
            item.ProjectId = model.ProjectId;
            item.ProjectName=model.ProjectName;
            item.StationId=model.StationId;
            item.StationName=model.StationName;

            _context.InwardDetails.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.InwardDetails.FindAsync(id);

            _context.InwardDetails.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}