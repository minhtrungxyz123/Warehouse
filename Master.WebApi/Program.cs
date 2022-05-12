using Master.Service;
using Master.WebApi.SignalRHubs;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WarehouseDbContext>(options => options.UseSqlServer(
                            builder.Configuration.GetConnectionString("WarehouseDatabase")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region addService

builder.Services.AddScoped<ICreatedByService, CreatedByService>();

#endregion addService

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
    .WithOrigins("https://localhost:1100")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .SetIsOriginAllowed(host => true)
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ConnectRealTimeHub>("/signalr");

app.Run();