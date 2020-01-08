using EmployeeScheduler.Lib.DTO;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class JsonFileSchedulingService : ISchedulingService
    {
        private string _jsonFilePath;
        public string JsonFilePath
        {
            //get => _jsonFilePath;
            set
            {
                if (!File.Exists(value))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(value));
                    File.WriteAllText(value, "[]");
                }
                _jsonFilePath = value;
            }
        }

        private readonly IJsonFileService _jsonService;

        public JsonFileSchedulingService(IJsonFileService jsonService)
        {
            _jsonService = jsonService;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var employees = (await _jsonService.ReadJsonAsync<Employee[]>(_jsonFilePath)).ToList();

            employee.ID = employees.Count > 0 ? employees.Max(e => e.ID) + 1 : 1;
            employee.Active = true;
            employees.Add(employee);

            await _jsonService.WriteJsonAsync(_jsonFilePath, employees.ToArray());
            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            var employees = await _jsonService.ReadJsonAsync<Employee[]>(_jsonFilePath);

            return employees.SingleOrDefault(e => e.ID == employeeID);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var employees = await _jsonService.ReadJsonAsync<Employee[]>(_jsonFilePath);
            var employeesExceptEditedOne = employees.Where(e => e.ID != employee.ID).ToList();
            employeesExceptEditedOne.Add(employee);

            await _jsonService.WriteJsonAsync(_jsonFilePath, employeesExceptEditedOne.ToArray());
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(int employeeID)
        {
            var employees = await _jsonService.ReadJsonAsync<Employee[]>(_jsonFilePath);
            var employee = employees.Single(e => e.ID == employeeID);
            employee.Active = false;

            await _jsonService.WriteJsonAsync(_jsonFilePath, employees);
            return employee;
        }

        public async Task<List<Employee>> GetEmployeesAsync(bool includeDeleted)
        {
            var employees = await _jsonService.ReadJsonAsync<Employee[]>(_jsonFilePath);
            return (from e in employees
                    where includeDeleted || e.Active
                    orderby e.Active descending, e.LastName, e.FirstName
                    select e).ToList();
        }

        public Employee AddEmployee(Employee employee)
        {
            var employees = _jsonService.ReadJson<Employee[]>(_jsonFilePath).ToList();

            employee.ID = employees.Count > 0 ? employees.Max(e => e.ID) + 1 : 1;
            employee.Active = true;
            employees.Add(employee);

            _jsonService.WriteJson(_jsonFilePath, employees.ToArray());
            return employee;
        }

        public Employee GetEmployee(int employeeID)
        {
            var employees = _jsonService.ReadJson<Employee[]>(_jsonFilePath);

            return employees.SingleOrDefault(e => e.ID == employeeID);
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var employees = _jsonService.ReadJson<Employee[]>(_jsonFilePath);
            var employeesExceptEditedOne = employees.Where(e => e.ID != employee.ID).ToList();
            employeesExceptEditedOne.Add(employee);

            _jsonService.WriteJson(_jsonFilePath, employeesExceptEditedOne.ToArray());
            return employee;
        }

        public Employee DeleteEmployee(int employeeID)
        {
            var employees = _jsonService.ReadJson<Employee[]>(_jsonFilePath);
            var employee = employees.Single(e => e.ID == employeeID);
            employee.Active = false;

            _jsonService.WriteJson(_jsonFilePath, employees);
            return employee;
        }

        public List<Employee> GetEmployees(bool includeDeleted)
        {
            var employees = _jsonService.ReadJson<Employee[]>(_jsonFilePath);
            return (from e in employees
                    where includeDeleted || e.Active
                    orderby e.Active descending, e.LastName, e.FirstName
                    select e).ToList();
        }

        public Task<ScheduleWeek> GetCurrentScheduleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleWeek> GetScheduleAsync(DateTime dateWithinWeek)
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleWeek> SaveScheduleAsync(ScheduleWeek schedule)
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleWeek> GetScheduleAsync(long scheduleID)
        {
            throw new NotImplementedException();
        }

        public long GetScheduleID(DateTime dateWithinWeek)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetScheduleIDAsync(DateTime dateWithinWeek)
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
    }
}
