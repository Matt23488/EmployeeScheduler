using EmployeeScheduler.Lib.DAL;
using EmployeeScheduler.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.BLL.Api
{
    public class SqliteSchedulingService : ISchedulingService
    {
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            //var entity = new DAL.Employee
            //{
            //    Active = employee.Active,
            //    FirstName = employee.FirstName,
            //    LastName = employee.LastName,
            //    EmailAddress = employee.EmailAddress
            //};

            //employee.TypicalSchedule.Days.ForEach(d => d.Employee = employee);
            //employee.TypicalSchedule.Days.ForEach(d => d. = employee);

            using var context = new DAL.SchedulerContext();
            //context.Employees.Add(entity);
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            //employee.ID = entity.EmployeeID;

            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            using var context = new DAL.SchedulerContext();
            var entity = await context.Employees.AsAsyncEnumerable().SingleOrDefaultAsync(e => e.EmployeeID == employeeID);
            if (entity == null) return null;

            return entity;
            //return new DTO.Employee
            //{
            //    ID = entity.EmployeeID,
            //    Active = entity.Active,
            //    FirstName = entity.FirstName,
            //    LastName = entity.LastName,
            //    EmailAddress = entity.EmailAddress
            //};
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            using var context = new DAL.SchedulerContext();
            var entity = await context.Employees.AsAsyncEnumerable().SingleOrDefaultAsync(e => e.EmployeeID == employee.EmployeeID);
            if (entity == null) return null;

            entity.Active = employee.Active;
            entity.FirstName = employee.FirstName;
            entity.LastName = employee.LastName;
            entity.EmailAddress = employee.EmailAddress;
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Employee>> GetEmployeesAsync(bool includeDeleted)
        {
            using var context = new DAL.SchedulerContext();
            var entities = await context.Employees.AsAsyncEnumerable().Where(e => includeDeleted || e.Active).ToListAsync();

            //var employees = new List<DTO.Employee>();
            //entities.ForEach(e => employees.Add(new DTO.Employee
            //{
            //    ID = e.EmployeeID,
            //    Active = e.Active,
            //    FirstName = e.FirstName,
            //    LastName = e.LastName,
            //    EmailAddress = e.EmailAddress
            //}));

            return entities;
            //return employees;
        }

        public Employee AddEmployee(Employee employee)
            => throw new NotImplementedException();
        public Employee GetEmployee(int employeeID)
            => throw new NotImplementedException();
        public Employee UpdateEmployee(Employee employee)
            => throw new NotImplementedException();
        public List<Employee> GetEmployees(bool includeDeleted)
            => throw new NotImplementedException();

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

        public Task<double> GetTimeZoneOffsetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasLocalData()
        {
            throw new NotImplementedException();
        }
    }
}
