using Blazored.LocalStorage;
using Blazored.SessionStorage;
using EmployeeScheduler.Lib.BLL;
using EmployeeScheduler.Lib.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeScheduler.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
            services.AddScoped<ISchedulingService, LocalStorageSchedulingService>();
            services.AddScoped<IMigrationService, LocalToApiMigrationService>();
            services.AddScoped<IAlertService, SessionStorageAlertService>();
            services.AddScoped<IToastService, ToastService>();
            services.AddScoped<IFetchService, JavaScriptFetchService>();
            services.AddScoped<ILogger, JavaScriptConsoleLogger>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
