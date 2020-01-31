using EmployeeScheduler.Lib.DTO;
using EmployeeScheduler.Lib.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class ApiSchedulingService : ISchedulingService
    {
        private readonly string _apiUrl;
        private readonly NavigationManager _nav;
        private readonly ILogger _logger;
        private readonly IFetchService _fetch;
        private readonly IToastService _toast;

        public ApiSchedulingService(string apiUrl, NavigationManager nav, ILogger logger, IFetchService fetch, IToastService toast)
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

        private async Task<T> ParseResult<T>(FetchResult<T> result) where T : class
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

        public Task<long> GetScheduleIDAsync(DateTime dateWithinWeek)
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleWeek> GetCurrentScheduleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleWeek> GetScheduleAsync(long scheduleID)
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleWeek> SaveScheduleAsync(ScheduleWeek schedule)
        {
            throw new NotImplementedException();
        }

        public string GetDayOfWeek(int dayIndex)
        {
            throw new NotImplementedException();
        }

        public Task SetWeekStartAsync(int dayIndex)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetWeekStartAsync()
        {
            throw new NotImplementedException();
        }

        public Task SetTimeZoneOffsetAsync(int offset)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTimeZoneOffsetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasData()
        {
            throw new NotImplementedException();
        }
    }
}
