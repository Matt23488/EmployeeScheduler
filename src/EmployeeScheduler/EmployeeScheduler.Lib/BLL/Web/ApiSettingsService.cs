using EmployeeScheduler.Lib.DAL;
using EmployeeScheduler.Lib.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL.Web
{
    public class ApiSettingsService : ISettingsService
    {
        private readonly string _apiUrl;
        private readonly NavigationManager _nav;
        private readonly ILogger _logger;
        private readonly IFetchService _fetch;
        private readonly IToastService _toast;

        public ApiSettingsService(string apiUrl, NavigationManager nav, ILogger logger, IFetchService fetch, IToastService toast)
        {
            _apiUrl = apiUrl;
            _nav = nav;
            _logger = logger;
            _fetch = fetch;
            _toast = toast;
        }

        private async Task OnUnauthorizedAsync()
        {
            await _toast.ShowAsync("You are not authorized. Please input your password.", ToastType.Warning);
            _nav.NavigateTo($"{_nav.BaseUri}auth"); // TODO: return URL or something.
        }

        private async Task OnErrorAsync(object data)
        {
            await _toast.ShowAsync("Something went wrong. Check the console with F12 and alert Matt.", ToastType.Error);
            await _logger.LogAsync("Data:", data);
        }

        private async Task OnErrorAsync(Exception ex)
        {
            await _toast.ShowAsync("Something went wrong. Check the console with F12 and alert Matt.", ToastType.Error);
            await _logger.LogExceptionAsync(ex);
        }

        private async Task<T> ParseResult<T>(DTO.FetchResult<T> result) where T : class
        {
            if (result.Status == 401)
            {
                await OnUnauthorizedAsync();
                return null;
            }

            if (result.Status != 200)
            {
                await OnErrorAsync(result.UnParsedData);
                return null;
            }

            return result.Data;
        }

        public async Task<AdminSettings> GetSettingsAsync()
        {
            try
            {
                var result = await _fetch.GetAsync<AdminSettings>($"{_apiUrl}settings");

                return await ParseResult(result);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return null;
            }
        }

        public async Task<AdminSettings> SaveSettingsAsync(AdminSettings settings)
        {
            try
            {
                var result = await _fetch.PostAsync<AdminSettings>($"{_apiUrl}settings", settings);

                return await ParseResult(result);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return null;
            }
        }
    }
}
