using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.Services.DataSeed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<HeadHunterContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true; 
    })
    .AddEntityFrameworkStores<HeadHunterContext>();
builder.Services.ConfigureApplicationCookie(o =>
{
    o.Cookie.HttpOnly = true;
    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    o.SlidingExpiration = true;
});

builder.Services.AddScoped<IdentityErrorDescriber, RussianErrorDescriber>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AccountExtensions>();
builder.Services.AddScoped<MapTo>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IResponseApplicationService, ResponseApplicationService>();



builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});
var app = builder.Build();
var serviceProvider = app.Services;
using var scope = serviceProvider.CreateScope();
try
{
    UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    // добавление ролей в бд при старте проекта
    await RoleInitial.SeedRoleAsync(userManager, roleManager);
    // добавление категорий при старте проекта
    try
    {
        CategorySeed.CategoryDataSeed(scope.ServiceProvider);
    }
    catch (Exception e)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogCritical(e, "Не удалось добавить категории в БД");;
    }
}
catch (Exception e)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogCritical(e, "Не удалось добавить админа в БД");
}


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}");
app.MapControllers(); // Используем верхнеуровневую регистрацию маршрутов

app.MapControllerRoute(
    name: "download",
    pattern: "download",
    defaults: new { controller = "Employee", action = "DownloadPdf" });

app.Run();