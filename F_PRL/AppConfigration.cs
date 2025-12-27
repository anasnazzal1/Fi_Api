using F_BLL.Service;
using F_DAL.Respsotry;
using F_DAL.Utilites;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace F_PRL
{
    public static class AppConfigration
    {
        public static void config(IServiceCollection Services)
        {

                Services.AddScoped<ICatgryResostry, CatgryResostry>();
                Services.AddScoped<ICatgryService, CatgryService>();
            Services.AddScoped<IAuthraizationService, AutharizationService>();
            Services.AddTransient<IEmailSender, EmailSender>();


            // Register seeders (MULTIPLE)
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeadData>();
        }
    }
}
