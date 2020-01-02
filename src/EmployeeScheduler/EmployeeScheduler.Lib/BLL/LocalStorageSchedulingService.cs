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

        private readonly ILocalStorageService _localStorage;

        public LocalStorageSchedulingService(ILocalStorageService localStorage, ISyncLocalStorageService syncLocalStorage)
        {
            _localStorage = localStorage;

            // TODO: Add keys to localStorage if they don't exist
            if (!syncLocalStorage.ContainKey(KEY_EMPLOYEES))
            {
                syncLocalStorage.SetItem(KEY_EMPLOYEES, new Employee[0]);
            }
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var employees = (await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES)).ToList();

            employee.ID = employees.Count > 0 ? employees.Max(e => e.ID) + 1 : 1;
            employee.Active = true;
            employees.Add(employee);

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employees.ToArray());
            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES);

            return employees.SingleOrDefault(e => e.ID == employeeID);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES);
            var employeesExceptEditedOne = employees.Where(e => e.ID != employee.ID).ToList();
            employeesExceptEditedOne.Add(employee);

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employeesExceptEditedOne.ToArray());
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(int employeeID)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES);
            var employee = employees.Single(e => e.ID == employeeID);
            employee.Active = false;

            await _localStorage.SetItemAsync(KEY_EMPLOYEES, employees);
            return employee;
        }

        public async Task<List<Employee>> GetEmployeesAsync(bool includeDeleted)
        {
            var employees = await _localStorage.GetItemAsync<Employee[]>(KEY_EMPLOYEES);
            return (from e in employees
                    where includeDeleted || e.Active
                    orderby e.Active descending, e.LastName, e.FirstName
                    select e).ToList();
        }
    }
}
