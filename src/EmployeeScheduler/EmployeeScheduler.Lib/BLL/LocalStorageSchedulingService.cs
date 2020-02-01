using Blazored.LocalStorage;
using EmployeeScheduler.Lib.DAL;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class LocalStorageSchedulingService : ISchedulingService
    {
        private const string KEY_EMPLOYEES = "EmployeeScheduler_Employees";
        private const string KEY_SCHEDULES = "EmployeeScheduler_Schedules";
        private const string KEY_WEEK_START = "EmployeeScheduler_WeekStart";
        private const string KEY_TIMEZONE_OFFSET = "EmployeeScheduler_TimeZoneOffset";

        private readonly ILocalStorageService _localStorage;
        private readonly ISyncLocalStorageService _syncLocalStorage;
        private readonly ILogger _logger;

        public LocalStorageSchedulingService(ILocalStorageService localStorage, ISyncLocalStorageService syncLocalStorage, ILogger logger)
        {
            _localStorage = localStorage;
            _syncLocalStorage = syncLocalStorage;
            _logger = logger;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var employees = (await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES))?.ToList() ?? new List<Employee>();

            employee.EmployeeID = employees.Count > 0 ? employees.Max(e => e.EmployeeID) + 1 : 1;
            employee.Active = true;
            employees.Add(employee);

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employees.ToArray());
            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];

            return employees.SingleOrDefault(e => e.EmployeeID == employeeID);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];
            var employeesExceptEditedOne = employees.Where(e => e.EmployeeID != employee.EmployeeID).ToList();
            employeesExceptEditedOne.Add(employee);

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employeesExceptEditedOne.ToArray());
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(int employeeID)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];
            var employee = employees.Single(e => e.EmployeeID == employeeID);
            employee.Active = false;

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employees);
            return employee;
        }

        public async Task<List<Employee>> GetEmployeesAsync(bool includeDeleted)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];
            return (from e in employees
                    where includeDeleted || e.Active
                    orderby e.Active descending, e.LastName, e.FirstName
                    select e).ToList();
        }

        public List<Employee> GetEmployees(bool includeDeleted)
        {
            throw new NotImplementedException();
        }

        public Employee AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int employeeID)
        {
            throw new NotImplementedException();
        }

        public Employee UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee DeleteEmployee(int employeeID)
        {
            throw new NotImplementedException();
        }

        public async Task<long> GetScheduleIDAsync(DateTime dateWithinWeek)
        {
            var fixedDate = dateWithinWeek.ToUniversalTime().AddHours(await GetTimeZoneOffsetAsync());
            var weekStartDay = await _localStorage.GetItemAsync<int>(KEY_WEEK_START);
            var dayOffset = weekStartDay - (int)fixedDate.Date.DayOfWeek;

            if (dayOffset > 0) dayOffset -= 7;

            return fixedDate.Date.AddDays(dayOffset).Ticks;
        }

        public async Task<ScheduleWeek> GetCurrentScheduleAsync()
            => await GetScheduleAsync(await GetScheduleIDAsync(DateTime.Now));

        public async Task<ScheduleWeek> GetScheduleAsync(long scheduleID)
        {
            var startDay = new DateTime(scheduleID);

            var schedules = await _localStorage.GetItemAsync<ScheduleWeek[]>(KEY_SCHEDULES) ?? new ScheduleWeek[0];

            return schedules.SingleOrDefault(s => s.ScheduleWeekID == scheduleID) ?? new ScheduleWeek
            {
                ScheduleWeekID = scheduleID
            };
        }

        public async Task<ScheduleWeek> SaveScheduleAsync(ScheduleWeek schedule)
        {
            if (IsTooOld(schedule)) return schedule;

            var schedules = await _localStorage.GetItemAsync<ScheduleWeek[]>(KEY_SCHEDULES) ?? new ScheduleWeek[0];
            var schedulesExceptEditedOne = schedules.Where(IsNotTooOld).Where(s => s.ScheduleWeekID != schedule.ScheduleWeekID).ToList();
            schedulesExceptEditedOne.Add(schedule);
            await _localStorage.SetItemAsync(KEY_SCHEDULES, schedulesExceptEditedOne.ToArray());
            return schedule;
        }

        private bool IsTooOld(ScheduleWeek schedule)
            => (DateTime.Now.ToUniversalTime().AddHours(_syncLocalStorage.GetItem<int>(KEY_TIMEZONE_OFFSET)) - new DateTime(schedule.ScheduleWeekID)).TotalDays >= 16 * 7;

        private bool IsNotTooOld(ScheduleWeek schedule)
            => !IsTooOld(schedule);

        private static readonly string[] DAY_NAMES = new [] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        public string GetDayOfWeek(int dayIndex)
            => DAY_NAMES[dayIndex % 7];

        public async Task SetWeekStartAsync(int dayIndex)
            => await _localStorage.SetItemAsync(KEY_WEEK_START, dayIndex);

        public async Task<int> GetWeekStartAsync()
            => await _localStorage.GetItemAsync<int>(KEY_WEEK_START);

        public async Task SetTimeZoneOffsetAsync(int offset)
            => await _localStorage.SetItemAsync(KEY_TIMEZONE_OFFSET, offset);

        public async Task<int> GetTimeZoneOffsetAsync()
            => await _localStorage.GetItemAsync<int>(KEY_TIMEZONE_OFFSET);

        public async Task<bool> HasData()
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];
            var schedules = await _localStorage.GetItemAsync<ScheduleWeek[]>(KEY_SCHEDULES) ?? new ScheduleWeek[0];

            return employees.Length > 0 || schedules.Length > 0;
        }
    }
}
