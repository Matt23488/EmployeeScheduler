using EmployeeScheduler.Lib.DTO;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class JsonFileSchedulingService : ISchedulingService
    {
        public Task<Employee> AddEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> DeleteEmployeeAsync(int employeeID)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeAsync(int employeeID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetEmployeesAsync(bool includeDeleted)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
