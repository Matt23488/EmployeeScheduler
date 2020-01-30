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
            services.AddScoped<IFetchService, JavaScriptFetchService>(factory =>
            {
                var localStorage = factory.GetService<ILocalStorageService>();
                var js = factory.GetService<Microsoft.JSInterop.IJSRuntime>();
                var logger = factory.GetService<ILogger>();

                var service = new JavaScriptFetchService(js, logger);
                service.AddAdditionalHeader("authentication-token", async () =>
                {
                    return (await localStorage.GetItemAsync<Lib.DTO.ClientToken>("EmployeeScheduler_token"))?.Token;
                });

                return service;
            });
            services.AddScoped<ILogger, JavaScriptConsoleLogger>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
