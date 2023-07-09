using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROJ_Synchronise_Account_Api.Models;
using PROJ_Synchronise_Account_Api.Services;

[assembly: FunctionsStartup(typeof(PROJ_Synchronise_Account_Api.Startup))]
namespace PROJ_Synchronise_Account_Api
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //FunctionsHostBuilderContext context = builder.GetContext();
            //string connectionString = context.Configuration.GetConnectionString("dbconnection");            
            string connectionString = "Data Source=WS34518;Integrated Security=true;Database=wc-local-sitecore-custom";
            builder.Services.AddDbContext<AppDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
            builder.Services.AddTransient<IUserAccountService, UserAccountService>();
            builder.Services.AddTransient<IOrganisationService, OrganisationService>();
        }
    }
}
