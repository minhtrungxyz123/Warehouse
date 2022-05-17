using Microsoft.EntityFrameworkCore;
using System.Linq;
using Warehouse.Common;
using Warehouse.Data.EF;
using Warehouse.Model.BeginningWareHouse;

namespace Warehouse.Service
{
    public class BeginningWareHouseService : IBeginningWareHouseService
    {
        #region Fields

        private readonly WarehouseDbContext _context;

        public BeginningWareHouseService(WarehouseDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Data.Entities.BeginningWareHouse>> GetByIdAsyn(string id)
        {
            var item = await _context.BeginningWareHouses
                            .OrderByDescending(p => p.UnitId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            var userViewModel = new Data.Entities.BeginningWareHouse()
            {
                CreatedBy = item.CreatedBy,
                UnitId = item.UnitId,
                ItemId = item.ItemId,
                CreatedDate = item.CreatedDate,
                ModifiedBy = item.ModifiedBy,
                ModifiedDate = item.ModifiedDate,
                Id = item.Id,
                Quantity = item.Quantity,
                UnitName = item.UnitName,
                WareHouseId=item.WareHouseId,
            };
            return new ApiSuccessResult<Data.Entities.BeginningWareHouse>(userViewModel);
        }

        public async Task<IEnumerable<Data.Entities.BeginningWareHouse>> GetAll()
        {
            return await _context.BeginningWareHouses
                            .OrderByDescending(p => p.UnitId)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<BeginningWareHouseModel>>> GetAllPaging(GetBeginningWareHousePagingRequest request)
        {
            var query = from pr in _context.BeginningWareHouses
                        join c in _context.WareHouseItems on pr.ItemId equals c.Id into pt
                        from tp in pt.DefaultIfEmpty()
                        join u in _context.Units on pr.UnitId equals u.Id into ut
                        from up in ut.DefaultIfEmpty()
                        join o in _context.CreatedBies on pr.CreatedBy equals o.Id into ot
                        from op in ot.DefaultIfEmpty()
                        join w in _context.WareHouses on pr.WareHouseId equals w.Id into wt
                        from wp in wt.DefaultIfEmpty()
                        select new { pr, tp, up, op, wp };


            if (!string.IsNullOrEmpty(request.ItemId))
            {
                query = query.Where(x => x.pr.ItemId == request.ItemId);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new BeginningWareHouseModel()
                {
                    Id = u.pr.Id,
                    ItemId = u.tp.Name,
                    CreatedBy = u.op.FullName,
                    CreatedDate = u.pr.CreatedDate,
                    ModifiedBy = u.pr.ModifiedBy,
                    ModifiedDate = u.pr.ModifiedDate,
                    Quantity = u.pr.Quantity,
                    UnitId = u.up.UnitName,
                    WareHouseId = u.wp.Name,
                })
                .ToListAsync();

            var pagination = new Pagination<BeginningWareHouseModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return new ApiSuccessResult<Pagination<BeginningWareHouseModel>>(pagination);
        }

        public async Task<Data.Entities.BeginningWareHouse?> GetById(string? id)
        {
            var item = await _context.BeginningWareHouses
                            .OrderByDescending(p => p.UnitId)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<bool> ExistAsync(string idWareHouse, string idItem)
        {
            var check = await _context.BeginningWareHouses.AsQueryable().AnyAsync(
                a => a.WareHouseId.Equals(idWareHouse)
                     && a.ItemId.Equals(idItem));
            return check;
        }

        public async Task<RepositoryResponse> Create(BeginningWareHouseModel model)
        {
            Data.Entities.BeginningWareHouse item = new Data.Entities.BeginningWareHouse()
            {
                CreatedBy = model.CreatedBy,
                UnitId = model.UnitId,
                ItemId = model.ItemId,
                UnitName = model.UnitName,
                WareHouseId=model.WareHouseId,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                Quantity = model.Quantity,
            };
            item.Id = Guid.NewGuid().ToString();

            _context.BeginningWareHouses.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(string id, BeginningWareHouseModel model)
        {
            var item = await _context.BeginningWareHouses.FindAsync(id);
            item.UnitName = model.UnitName;
            item.WareHouseId = model.WareHouseId;
            item.CreatedDate = model.CreatedDate;
            item.ModifiedBy = model.ModifiedBy;
            item.ModifiedDate = model.ModifiedDate;
            item.Quantity = model.Quantity;
            item.UnitId = model.UnitId;
            item.CreatedBy = model.CreatedBy;
            

            _context.BeginningWareHouses.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.BeginningWareHouses.FindAsync(id);

            _context.BeginningWareHouses.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}