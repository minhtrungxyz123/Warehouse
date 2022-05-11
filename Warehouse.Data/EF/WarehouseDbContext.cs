using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Configuration;
using Warehouse.Data.Entities;
using Warehouse.Data.UnitOfWork;

namespace Warehouse.Data.EF
{
    public class WarehouseDbContext : DbContext, IUnitOfWork
    {
        public WarehouseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new AuditConfiguration());
            modelBuilder.ApplyConfiguration(new AuditCouncilConfiguration());
            modelBuilder.ApplyConfiguration(new AuditDetailConfiguration());
            modelBuilder.ApplyConfiguration(new AuditDetailSerialConfiguration());
            modelBuilder.ApplyConfiguration(new BeginningWareHouseConfiguration());
            modelBuilder.ApplyConfiguration(new InwardConfiguration());
            modelBuilder.ApplyConfiguration(new InwardDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OutwardConfiguration());
            modelBuilder.ApplyConfiguration(new OutwardDetailConfiguration());
            modelBuilder.ApplyConfiguration(new SerialWareHouseConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            modelBuilder.ApplyConfiguration(new VendorConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseBalanceConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseItemCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseItemConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseItemUnitConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseLimitConfiguration());
            modelBuilder.ApplyConfiguration(new CreatedByConfiguration());
        }

        private readonly IMediator _mediator;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return true;
            else
                return false;
        }

        public DbSet<AuditDetailSerial> AuditDetailSerials { get; set; }
        public DbSet<BeginningWareHouse> BeginningWareHouses { get; set; }
        public DbSet<Inward> Inwards { get; set; }
        public DbSet<InwardDetail> InwardDetails { get; set; }
        public DbSet<Outward> Outwards { get; set; }
        public DbSet<OutwardDetail> OutwardDetails { get; set; }
        public DbSet<SerialWareHouse> SerialWareHouses { get; set; }
        public DbSet<Data.Entities.Unit> Units { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<WarehouseBalance> WarehouseBalances { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<WareHouseItemCategory> WareHouseItemCategories { get; set; }
        public DbSet<WareHouseItem> WareHouseItems { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<AuditCouncil> AuditCouncils { get; set; }
        public DbSet<AuditDetail> AuditDetails { get; set; }
        public DbSet<WareHouseLimit> WareHouseLimits { get; set; }
        public DbSet<WareHouseItemUnit> WareHouseItemUnits { get; set; }
        public DbSet<CreatedBy> CreatedBies { get; set; }
    }
}