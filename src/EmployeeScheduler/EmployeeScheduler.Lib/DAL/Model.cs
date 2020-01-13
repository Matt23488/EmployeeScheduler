using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DAL
{
    //https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=visual-studio
    public class SchedulerContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }
        public DbSet<ScheduleDay> ScheduleDays { get; set; }
        public DbSet<ScheduleWeek> ScheduleWeeks { get; set; }
        public DbSet<Token> Tokens { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=test.db");
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool Active { get; set; }
    }

    public class EmployeeSchedule
    {
        public int EmployeeScheduleID { get; set; }
        public bool IsOff { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int LunchType { get; set; }// = 2;

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int ScheduleDayID { get; set; }
        public ScheduleDay ScheduleDay { get; set; }
    }

    public class ScheduleDay
    {
        public int ScheduleDayID { get; set; }
        public bool IsClosed { get; set; }
        public List<EmployeeSchedule> EmployeeSchedules { get; set; } = new List<EmployeeSchedule>();

        public long ScheduleWeekID { get; set; }
        public ScheduleWeek ScheduleWeek { get; set; }
    }

    public class ScheduleWeek
    {
        public long ScheduleWeekID { get; set; }
        public List<ScheduleDay> Days { get; set; } = new List<ScheduleDay>();
    }

    public class Token
    {
        public const int ROLE_ADMIN = 0;
        public const int ROLE_USER = 1;
        public int TokenID { get; set; }
        //public string Role { get; set; }
        public int Role { get; set; }
        public string TokenValue { get; set; }
        public string IpAddress { get; set; }
        public DateTime Expires { get; set; }
    }

    // TODO: A table to track IPs that send incorrect passwords
}
