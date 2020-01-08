using EmployeeScheduler.Lib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Services
{
    public interface ISchedulingService
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeAsync(int employeeID);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        //Task<Employee> DeleteEmployeeAsync(int employeeID);
        Task<List<Employee>> GetEmployeesAsync(bool includeDeleted);

        Employee AddEmployee(Employee employee);
        Employee GetEmployee(int employeeID);
        Employee UpdateEmployee(Employee employee);
        //Employee DeleteEmployee(int employeeID);
        List<Employee> GetEmployees(bool includeDeleted);


        Task<long> GetScheduleIDAsync(DateTime dateWithinWeek);
        Task<ScheduleWeek> GetCurrentScheduleAsync();
        Task<ScheduleWeek> GetScheduleAsync(long scheduleID);
        //Task<ScheduleWeek> GetScheduleAsync(DateTime dateWithinWeek);
        Task<ScheduleWeek> SaveScheduleAsync(ScheduleWeek schedule);

        string GetDayOfWeek(int dayIndex);

        Task SetWeekStartAsync(int dayIndex);
        Task<int> GetWeekStartAsync();

        Task SetTimeZoneOffsetAsync(int offset);
        Task<int> GetTimeZoneOffsetAsync();
    }
}
