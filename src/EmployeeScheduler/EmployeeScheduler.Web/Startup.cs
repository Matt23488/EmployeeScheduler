using Blazored.LocalStorage;
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
            services.AddScoped<ISchedulingService, LocalStorageSchedulingService>();
            services.AddScoped<ILogger, JavaScriptConsoleLogger>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
