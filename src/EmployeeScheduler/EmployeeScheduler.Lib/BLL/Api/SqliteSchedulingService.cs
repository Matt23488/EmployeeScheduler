using EmployeeScheduler.Lib.DAL;
using EmployeeScheduler.Lib.Services;
using Microsoft.EntityFrameworkCore;
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
            using var context = new DAL.SchedulerContext();
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            employee.TypicalSchedule.Employee = null;

            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            using var context = new DAL.SchedulerContext();
            var entity = await context.Employees.Include(e => e.TypicalSchedule).ThenInclude(e => e.Days).AsAsyncEnumerable().SingleOrDefaultAsync(e => e.EmployeeID == employeeID);
            if (entity == null) return null;

            entity.TypicalSchedule.Employee = null;

            return entity;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            using var context = new DAL.SchedulerContext();
            var entity = await context.Employees.Include(e => e.TypicalSchedule).ThenInclude(e => e.Days).AsAsyncEnumerable().SingleOrDefaultAsync(e => e.EmployeeID == employee.EmployeeID);
            if (entity == null) return null;

            entity.Active = employee.Active;
            entity.FirstName = employee.FirstName;
            entity.LastName = employee.LastName;
            entity.EmailAddress = employee.EmailAddress;

            for (int i = 0; i < entity.TypicalSchedule.Days.Count; i++)
            {
                entity.TypicalSchedule.Days[i].From = employee.TypicalSchedule.Days[i].From;
                entity.TypicalSchedule.Days[i].To = employee.TypicalSchedule.Days[i].To;
                entity.TypicalSchedule.Days[i].IsOff = employee.TypicalSchedule.Days[i].IsOff;
                entity.TypicalSchedule.Days[i].LunchType = employee.TypicalSchedule.Days[i].LunchType;
            }

            await context.SaveChangesAsync();

            entity.TypicalSchedule.Employee = null;

            return entity;
        }

        public async Task<List<Employee>> GetEmployeesAsync(bool includeDeleted)
        {
            using var context = new DAL.SchedulerContext();
            var entities = await context.Employees.AsAsyncEnumerable().Where(e => includeDeleted || e.Active).ToListAsync();

            return entities;
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
