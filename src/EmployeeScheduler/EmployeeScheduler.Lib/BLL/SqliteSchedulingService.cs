using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL
{
    public class SqliteSchedulingService : ISchedulingService
    {
        public async Task<DTO.Employee> AddEmployeeAsync(DTO.Employee employee)
        {
            var entity = new DAL.Employee
            {
                Active = employee.Active,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmailAddress = employee.EmailAddress
            };

            using var context = new DAL.SchedulerContext();
            context.Employees.Add(entity);
            await context.SaveChangesAsync();

            employee.ID = entity.EmployeeID;

            return employee;
        }

        public async Task<DTO.Employee> GetEmployeeAsync(int employeeID)
        {
            using var context = new DAL.SchedulerContext();
            var entity = await context.Employees.AsAsyncEnumerable().SingleOrDefaultAsync(e => e.EmployeeID == employeeID);
            if (entity == null) return null;

            return new DTO.Employee
            {
                ID = entity.EmployeeID,
                Active = entity.Active,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EmailAddress = entity.EmailAddress
            };
        }

        public async Task<DTO.Employee> UpdateEmployeeAsync(DTO.Employee employee)
        {
            using var context = new DAL.SchedulerContext();
            var entity = await context.Employees.AsAsyncEnumerable().SingleOrDefaultAsync(e => e.EmployeeID == employee.ID);
            if (entity == null) return null;

            entity.Active = employee.Active;
            entity.FirstName = employee.FirstName;
            entity.LastName = employee.LastName;
            entity.EmailAddress = employee.EmailAddress;
            await context.SaveChangesAsync();

            return employee;
        }

        public async Task<List<DTO.Employee>> GetEmployeesAsync(bool includeDeleted)
        {
            using var context = new DAL.SchedulerContext();
            var entities = await context.Employees.AsAsyncEnumerable().Where(e => includeDeleted || e.Active).ToListAsync();

            var employees = new List<DTO.Employee>();
            entities.ForEach(e => employees.Add(new DTO.Employee
            {
                ID = e.EmployeeID,
                Active = e.Active,
                FirstName = e.FirstName,
                LastName = e.LastName,
                EmailAddress = e.EmailAddress
            }));

            return employees;
        }

        public DTO.Employee AddEmployee(DTO.Employee employee)
            => throw new NotImplementedException();
        public DTO.Employee GetEmployee(int employeeID)
            => throw new NotImplementedException();
        public DTO.Employee UpdateEmployee(DTO.Employee employee)
            => throw new NotImplementedException();
        public List<DTO.Employee> GetEmployees(bool includeDeleted)
            => throw new NotImplementedException();

        public Task<long> GetScheduleIDAsync(DateTime dateWithinWeek)
        {
            throw new NotImplementedException();
        }

        public Task<DTO.ScheduleWeek> GetCurrentScheduleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DTO.ScheduleWeek> GetScheduleAsync(long scheduleID)
        {
            throw new NotImplementedException();
        }

        public Task<DTO.ScheduleWeek> SaveScheduleAsync(DTO.ScheduleWeek schedule)
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
