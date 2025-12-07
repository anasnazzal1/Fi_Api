using F_BLL.Service;
using F_DAL.DATA;
using F_DAL.Models;
using F_DAL.Respsotry;
using F_DAL.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLocalization(options => options.ResourcesPath = "");

const string defaultCulture = "en";

var supportedCultures = new[]
{
    new CultureInfo(defaultCulture),
    new CultureInfo("ar")
};



builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider
    {
        QueryStringKey = "lang"
    });
});

builder.Services.AddDbContext<ApplecationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//to identity
builder.Services
    .AddIdentity<ApplecationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplecationDbContext>()
    .AddDefaultTokenProviders();




builder.Services.AddScoped<ICatgryResostry, CatgryResostry>();
builder.Services.AddScoped<ICatgryService, CatgryService>();
builder.Services.AddScoped<IAuthraizationService, AutharizationService>();


// Register seeders (MULTIPLE)
builder.Services.AddScoped<ISeedData, RoleSeedData>();
builder.Services.AddScoped<ISeedData, UserSeadData>();
var app = builder.Build();

// Localization
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Run ALL seeders
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var seeders = services.GetServices<ISeedData>();  // <== important!

    foreach (var seeder in seeders)
    {
        await seeder.SeedData();  // <== correct method name
    }
}

app.MapControllers();
app.Run();
