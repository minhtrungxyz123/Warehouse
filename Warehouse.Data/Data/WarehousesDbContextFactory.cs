using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Warehouse.Data.Data
{
    public class WarehousesDbContextFactory : IDesignTimeDbContextFactory<WarehousesContext>
    {
        public WarehousesContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configurationRoot.GetConnectionString("WarehouseDatabase");

            var optionBuilder = new DbContextOptionsBuilder<WarehousesContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new WarehousesContext(optionBuilder.Options);
        }
    }
}