using Blazored.LocalStorage;
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
    public class ApiSchedulingService : ISchedulingService
    {
        private const string KEY_EMPLOYEES = "EmployeeScheduler_Employees";
        private const string KEY_SCHEDULES = "EmployeeScheduler_Schedules";
        private const string KEY_WEEK_START = "EmployeeScheduler_WeekStart";
        private const string KEY_TIMEZONE_OFFSET = "EmployeeScheduler_TimeZoneOffset";

        private readonly string _apiUrl;
        private readonly NavigationManager _nav;
        private readonly ILogger _logger;
        private readonly IFetchService _fetch;
        private readonly IToastService _toast;
        private readonly ILocalStorageService _localStorage;

        public ApiSchedulingService(string apiUrl, NavigationManager nav, ILogger logger, IFetchService fetch, IToastService toast, ILocalStorageService localStorage)
        {
            _apiUrl = apiUrl;
            _nav = nav;
            _logger = logger;
            _fetch = fetch;
            _toast = toast;
            _localStorage = localStorage;
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

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            try
            {
                var result = await _fetch.PostAsync<Employee>($"{_apiUrl}employee", employee);

                return await ParseResult(result);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return null;
            }
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            try
            {
                var result = await _fetch.GetAsync<Employee>($"{_apiUrl}employee/{employeeID}");

                return await ParseResult(result);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return null;
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var result = await _fetch.PutAsync<Employee>($"{_apiUrl}employee", employee);

                return await ParseResult(result);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return null;
            }
        }

        public async Task<List<Employee>> GetEmployeesAsync(bool includeDeleted)
        {
            try
            {
                var result = await _fetch.GetAsync<List<Employee>>($"{_apiUrl}employee");

                return await ParseResult(result);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return null;
            }
        }

        public Employee AddEmployee(Employee employee) => throw new NotImplementedException();
        public Employee GetEmployee(int employeeID) => throw new NotImplementedException();
        public Employee UpdateEmployee(Employee employee) => throw new NotImplementedException();
        public List<Employee> GetEmployees(bool includeDeleted) => throw new NotImplementedException();

        public async Task<long> GetScheduleIDAsync(DateTime dateWithinWeek)
        {
            var settingsResult = await _fetch.GetAsync<AdminSettings>($"{_apiUrl}settings");
            //var fixedDate = dateWithinWeek.ToUniversalTime().AddHours(await GetTimeZoneOffsetAsync());
            //var weekStartDay = await _localStorage.GetItemAsync<int>(KEY_WEEK_START);

            // TODO: Create private overload of GetTimeZoneOffsetAsync() that takes in the settings to optimize
            // API calls.
            // I also need to add the TimeZoneOffset column to the AdminSettings entity.
            // And build implementations of ISettingsService for Web and Api.
            // And hide link to Settings.razor if user isn't admin.
            // More stuff of course. Just trying to jot down a quick list of futures.

            var fixedDate = dateWithinWeek.ToUniversalTime().AddHours(settingsResult.Data.TimeZoneOffset);
            var dayOffset = settingsResult.Data.WeekStartOffset - (int)fixedDate.Date.DayOfWeek;

            if (dayOffset > 0) dayOffset -= 7;

            return fixedDate.Date.AddDays(dayOffset).Ticks;
        }

        public async Task<ScheduleWeek> GetCurrentScheduleAsync()
            => await GetScheduleAsync(await GetScheduleIDAsync(DateTime.Now));

        public async Task<ScheduleWeek> GetScheduleAsync(long scheduleID)
        {
            try
            {
                var result = await _fetch.GetAsync<ScheduleWeek>($"{_apiUrl}schedule/{scheduleID}");

                return await ParseResult(result);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return null;
            }
        }

        public async Task<ScheduleWeek> SaveScheduleAsync(ScheduleWeek schedule)
        {
            try
            {
                var result = await _fetch.PutAsync<ScheduleWeek>($"{_apiUrl}schedule", schedule);

                return await ParseResult(result);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return null;
            }
        }

        private static readonly string[] DAY_NAMES = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        public string GetDayOfWeek(int dayIndex)
            => DAY_NAMES[dayIndex % 7];

        public Task SetWeekStartAsync(int dayIndex)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetWeekStartAsync()
        {
            var settingsResult = await _fetch.GetAsync<AdminSettings>($"{_apiUrl}settings");
            return settingsResult.Data.WeekStartOffset;
        }

        public Task SetTimeZoneOffsetAsync(int offset)
        {
            throw new NotImplementedException();
        }

        public async Task<double> GetTimeZoneOffsetAsync()
        { 
            var settingsResult = await _fetch.GetAsync<AdminSettings>($"{_apiUrl}settings");
            return settingsResult.Data.TimeZoneOffset;
        }

        public async Task<bool> HasLocalData()
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];
            var schedules = await _localStorage.GetItemAsync<ScheduleWeek[]>(KEY_SCHEDULES) ?? new ScheduleWeek[0];
            var hasTimeZoneOffset = await _localStorage.ContainKeyAsync(KEY_TIMEZONE_OFFSET);
            var hasWeekStart = await _localStorage.ContainKeyAsync(KEY_WEEK_START);

            return employees.Length > 0 || schedules.Length > 0 || hasTimeZoneOffset || hasWeekStart;
        }
    }
}
