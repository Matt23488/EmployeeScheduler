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
        Task AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeAsync(int employeeID);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeID);
        Task<List<Employee>> GetEmployeesAsync();
    }
}
