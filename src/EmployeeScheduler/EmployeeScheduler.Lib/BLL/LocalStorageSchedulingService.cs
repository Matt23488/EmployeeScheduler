using Blazored.LocalStorage;
using EmployeeScheduler.Lib.DTO;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    // Might should do some error checking in these methods...
    public class LocalStorageSchedulingService : ISchedulingService
    {
        private const string KEY_EMPLOYEES = "EmployeeScheduler_Employees";
        private const string KEY_SCHEDULES = "EmployeeScheduler_Schedules";

        private readonly ILocalStorageService _localStorage;

        public LocalStorageSchedulingService(ILocalStorageService localStorage, ISyncLocalStorageService syncLocalStorage)
        {
            _localStorage = localStorage;

            // TODO: Add keys to localStorage if they don't exist
            //if (!syncLocalStorage.ContainKey(KEY_EMPLOYEES))
            //{
            //    syncLocalStorage.SetItem(KEY_EMPLOYEES, new Employee[0]);
            //}
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var employees = (await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES))?.ToList() ?? new List<Employee>();

            employee.ID = employees.Count > 0 ? employees.Max(e => e.ID) + 1 : 1;
            employee.Active = true;
            employees.Add(employee);

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employees.ToArray());
            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];

            return employees.SingleOrDefault(e => e.ID == employeeID);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];
            var employeesExceptEditedOne = employees.Where(e => e.ID != employee.ID).ToList();
            employeesExceptEditedOne.Add(employee);

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employeesExceptEditedOne.ToArray());
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(int employeeID)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES) ?? new Employee[0];
            var employee = employees.Single(e => e.ID == employeeID);
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

        public async Task<ScheduleWeek> GetCurrentScheduleAsync()
            => await GetScheduleAsync(DateTime.Now);

        public async Task<ScheduleWeek> GetScheduleAsync(DateTime dateWithinWeek)
        {
            var sunday = dateWithinWeek.Date.AddDays(-(int)dateWithinWeek.DayOfWeek);
            var id = sunday.Ticks;

            var schedules = await _localStorage.GetItemAsync<ScheduleWeek[]>(KEY_SCHEDULES) ?? new ScheduleWeek[0];

            return schedules.SingleOrDefault(s => s.ID == id) ?? new ScheduleWeek
            {
                ID = id
            };
        }
    }
}
