using Microsoft.EntityFrameworkCore;
using Warehouse.WebApi.Configuration;
using Warehouse.WebApi.Models;

namespace Warehouse.WebApi.Data
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }

        public DbSet<Warehouse.WebApi.Models.Unit> Units { get; set; }
    }
}
