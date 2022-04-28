using Microsoft.EntityFrameworkCore;
using Warehouse.Data.EF;
using Warehouse.Service.Audit;
using Warehouse.Service.AuditDetail;
using Warehouse.Service.Inward;
using Warehouse.Service.Unit;
using Warehouse.Service.Vendor;
using Warehouse.Service.WareHouse;
using Warehouse.Service.WareHouseItem;
using Warehouse.Service.WareHouseItemCategory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WarehouseDbContext>(options => options.UseSqlServer(
                            builder.Configuration.GetConnectionString("WarehouseDatabase")));

#region addService
builder.Services.AddScoped(typeof(IWareHouseService), typeof(WareHouseService));
builder.Services.AddScoped(typeof(IUnitService), typeof(UnitService));
builder.Services.AddScoped(typeof(IAuditDetailService), typeof(AuditDetailService));
builder.Services.AddScoped(typeof(IAuditService), typeof(AuditService));
builder.Services.AddScoped<IWareHouseItemCategoryService, WareHouseItemCategoryService>();
builder.Services.AddScoped<IWareHouseItemService, WareHouseItemService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IInwardService, InwardService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();