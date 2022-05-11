using Master.WebApp.ApiClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using Warehouse.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

#region Add DI

builder.Services.AddTransient<ICreatedByApiClient, CreatedByApiClient>();

#endregion

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//AddCookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
 {
     config.LoginPath = "/Login/Index";
     config.AccessDeniedPath = "/Login/Index";
     config.ExpireTimeSpan = TimeSpan.FromHours(1);
     config.Cookie.HttpOnly = true;
     config.Cookie.IsEssential = true;
 });
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<PasswordHasherOptions>(option =>
{
    option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
    option.IterationCount = 12000;
});

//Repository
builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));

//Authorization
builder.Services.AddAuthorization(options =>
{
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
