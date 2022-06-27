using System.Globalization;
using ElectronicsShop;
using ElectronicsShop.Data;
using ElectronicsShop.IRepos;
using ElectronicsShop.Models;
using ElectronicsShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (_, factory) =>
            factory.Create(typeof(SharedModelsResource));
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new("ar", false),
        new("en",false)
    };
    var defaultCulture = new RequestCulture(culture: "en", uiCulture: "en");
    
    options.DefaultRequestCulture = defaultCulture;
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

#region DI
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDbInitializerRepository, DbInitializerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();



#endregion


var app = builder.Build();

#region Init Database

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;

var logger = service.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Start seeding data.");

var dbInitializerRepository = service.GetRequiredService<IDbInitializerRepository>();
dbInitializerRepository?.Initialize();

logger.LogInformation("Data seeded.");


#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(service.GetService<IOptions<RequestLocalizationOptions>>()?.Value!);

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
