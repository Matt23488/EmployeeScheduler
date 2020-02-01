using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<AdminSettings> AdminSettings { get; set; }
        public DbSet<Token> Tokens { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=test.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleWeek>()
                  .Property(e => e.ScheduleWeekID)
                  .ValueGeneratedNever()
                  .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
        }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool Active { get; set; }

        public int TypicalWeekID { get; set; }
        public TypicalWeek TypicalSchedule { get; set; }
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
    }

    public class TypicalDay
    {
        public int TypicalDayID { get; set; }
        public bool IsOff { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int LunchType { get; set; }

        public int TypicalWeekID { get; set; }
    }

    public class TypicalWeek
    {
        public int TypicalWeekID { get; set; }
        public int EmployeeID { get; set; }

        public List<TypicalDay> Days { get; set; } = new List<TypicalDay>();
    }

    public class ScheduleDay
    {
        public int ScheduleDayID { get; set; }
        public bool IsClosed { get; set; }
        public List<EmployeeSchedule> EmployeeSchedules { get; set; } = new List<EmployeeSchedule>();

        public long ScheduleWeekID { get; set; }
    }

    public class ScheduleWeek
    {
        public long ScheduleWeekID { get; set; }
        public List<ScheduleDay> Days { get; set; } = new List<ScheduleDay>();
    }

    public class AdminSettings
    {
        public int AdminSettingsID { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0, 6)]
        public int WeekStartOffset { get; set; }

        [System.ComponentModel.DataAnnotations.Range(-12.0, 14.0)]
        public double TimeZoneOffset { get; set; }
    }

    public class Token
    {
        //public const int ROLE_ADMIN = 0;
        //public const int ROLE_USER = 1;
        public int TokenID { get; set; }
        //public string Role { get; set; }
        public int Role { get; set; } // Maybe use the enum
        public string TokenValue { get; set; }
        public string IpAddress { get; set; }
        public DateTime Expires { get; set; }
    }

    public enum Roles
    {
        None = -1,
        Admin = 0,
        User = 1
    }

    // TODO: A table to track IPs that send incorrect passwords
}
