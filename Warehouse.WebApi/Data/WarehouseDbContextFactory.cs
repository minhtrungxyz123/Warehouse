using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Warehouse.WebApi.Data
{
    public class WarehouseDbContextFactory : IDesignTimeDbContextFactory<WarehouseContext>
    {
        public WarehouseContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configurationRoot.GetConnectionString("WarehouseDatabase");

            var optionBuilder = new DbContextOptionsBuilder<WarehouseContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new WarehouseContext(optionBuilder.Options);
        }
    }
}
