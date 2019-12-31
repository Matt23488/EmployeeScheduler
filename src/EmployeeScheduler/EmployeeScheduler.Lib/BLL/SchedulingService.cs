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
    public class SchedulingService : ISchedulingService
    {
        private const string KEY_EMPLOYEES = "EmployeeScheduler_Employees";

        private readonly ILocalStorageService _localStorage;

        public SchedulingService(ILocalStorageService localStorage, ISyncLocalStorageService syncLocalStorage)
        {
            _localStorage = localStorage;

            // TODO: Add keys to localStorage if they don't exist
            if (!syncLocalStorage.ContainKey(KEY_EMPLOYEES))
            {
                syncLocalStorage.SetItem(KEY_EMPLOYEES, new Employee[0]);
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            var employees = (await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES)).ToList();

            employee.ID = employees.Count > 0 ? employees.Max(e => e.ID) + 1 : 0;
            employee.Active = true;
            employees.Add(employee);

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employees.ToArray());
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES);

            return employees.Single(e => e.ID == employeeID);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var employees = (await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES)).ToList();
            employees.Add(employee);

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employees.ToArray());
        }

        public async Task DeleteEmployeeAsync(int employeeID)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES);
            var employee = employees.Single(e => e.ID == employeeID);
            employee.Active = false;

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employees);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES);
            return employees.Where(e => e.Active).ToList();
        }
    }
}
