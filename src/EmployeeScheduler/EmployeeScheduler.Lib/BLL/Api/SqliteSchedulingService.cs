using EmployeeScheduler.Lib.DAL;
using EmployeeScheduler.Lib.Extensions;
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

            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeID)
        {
            using var context = new DAL.SchedulerContext();
            var entity = await context.Employees.Include(e => e.TypicalSchedule).ThenInclude(e => e.Days).AsAsyncEnumerable().SingleOrDefaultAsync(e => e.EmployeeID == employeeID);
            if (entity == null) return null;

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

        public async Task<ScheduleWeek> GetScheduleAsync(long scheduleID)
        {
            using var context = new SchedulerContext();
            var entity = await context.ScheduleWeeks
                .Include(e => e.Days)
                .ThenInclude(e => e.EmployeeSchedules)
                .ThenInclude(e => e.Employee)
                .ThenInclude(e => e.TypicalSchedule)
                .ThenInclude(e => e.Days)
                .AsAsyncEnumerable()
                .SingleOrDefaultAsync(e => e.ScheduleWeekID == scheduleID);

            if (entity == null)
            {
                var employees = await context.Employees
                    .Include(e => e.TypicalSchedule)
                    .ThenInclude(e => e.Days)
                    .ToListAsync();

                entity = new ScheduleWeek
                {
                    ScheduleWeekID = scheduleID,
                    Days = new List<ScheduleDay>
                    {
                        new ScheduleDay{ EmployeeSchedules = employees.Select(e => e.TypicalSchedule.Days[0].ToEmployeeSchedule(e)).ToList() },
                        new ScheduleDay{ EmployeeSchedules = employees.Select(e => e.TypicalSchedule.Days[1].ToEmployeeSchedule(e)).ToList() },
                        new ScheduleDay{ EmployeeSchedules = employees.Select(e => e.TypicalSchedule.Days[2].ToEmployeeSchedule(e)).ToList() },
                        new ScheduleDay{ EmployeeSchedules = employees.Select(e => e.TypicalSchedule.Days[3].ToEmployeeSchedule(e)).ToList() },
                        new ScheduleDay{ EmployeeSchedules = employees.Select(e => e.TypicalSchedule.Days[4].ToEmployeeSchedule(e)).ToList() },
                        new ScheduleDay{ EmployeeSchedules = employees.Select(e => e.TypicalSchedule.Days[5].ToEmployeeSchedule(e)).ToList() },
                        new ScheduleDay{ EmployeeSchedules = employees.Select(e => e.TypicalSchedule.Days[6].ToEmployeeSchedule(e)).ToList() },
                    }
                };
            }

            return entity;
        }

        public async Task<ScheduleWeek> SaveScheduleAsync(ScheduleWeek schedule)
        {
            using var context = new SchedulerContext();
            var entity = await context.ScheduleWeeks
                .Include(e => e.Days)
                .ThenInclude(e => e.EmployeeSchedules)
                .ThenInclude(e => e.Employee)
                .ThenInclude(e => e.TypicalSchedule)
                .ThenInclude(e => e.Days)
                .AsAsyncEnumerable()
                .SingleOrDefaultAsync(e => e.ScheduleWeekID == schedule.ScheduleWeekID);

            if (entity == null)
            {
                schedule.Days.ForEach(d => d.EmployeeSchedules.ForEach(s => s.Employee = null));
                context.ScheduleWeeks.Add(schedule);
                entity = schedule;
            }
            else
            {
                for (int i = 0; i < entity.Days.Count; i++)
                {
                    entity.Days[i].IsClosed = schedule.Days[i].IsClosed;
                    for (int j = 0; j < entity.Days[i].EmployeeSchedules.Count; j++)
                    {
                        entity.Days[i].EmployeeSchedules[j].IsOff = schedule.Days[i].EmployeeSchedules[j].IsOff;
                        entity.Days[i].EmployeeSchedules[j].From = schedule.Days[i].EmployeeSchedules[j].From;
                        entity.Days[i].EmployeeSchedules[j].To = schedule.Days[i].EmployeeSchedules[j].To;
                        entity.Days[i].EmployeeSchedules[j].LunchType = schedule.Days[i].EmployeeSchedules[j].LunchType;
                    }
                }
            }

            await context.SaveChangesAsync();

            return entity;
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
