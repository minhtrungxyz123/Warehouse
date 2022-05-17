using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.EF;
using Warehouse.Data.Repositories;
using Warehouse.WebApp.ApiClient;
using Warehouse.WebApp.CustomHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

#region Add DI
builder.Services.AddTransient<IUnitApiClient, UnitApiClient>();
builder.Services.AddTransient<IWareHouseApiClient, WareHouseApiClient>();
builder.Services.AddTransient<IVendorApiClient, VendorApiClient>();
builder.Services.AddTransient<IWareHouseItemCategoryApiClient, WareHouseItemCategoryApiClient>();
builder.Services.AddTransient<IWareHouseItemApiClient, WareHouseItemApiClient>();
builder.Services.AddTransient<IBeginningWareHouseApiClient, BeginningWareHouseApiClient>();
builder.Services.AddTransient<IWareHouseItemUnitApiClient, WareHouseItemUnitApiClient>();
#endregion

//DbContext
builder.Services.AddDbContext<WarehouseDbContext>(options => options.UseSqlServer(
                            builder.Configuration.GetConnectionString("WarehouseDatabase")));

//Repository
builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));

//Authorization
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
builder.Services.AddAuthorization(options =>
{
});

//AddCookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
 {
     config.LoginPath = "/Login/Index";
     config.AccessDeniedPath = "/Login/FalseLogin";
     config.ExpireTimeSpan = TimeSpan.FromHours(1);
     config.Cookie.HttpOnly = true;
     config.Cookie.IsEssential = true;
 });
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<PasswordHasherOptions>(option =>
{
    option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
    option.IterationCount = 12000;
});

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
