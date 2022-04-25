using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Configuration;

namespace Warehouse.Data
{
    public class WarehousesContext : DbContext
    {
        public WarehousesContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuditConfiguration());
        }
    }
}