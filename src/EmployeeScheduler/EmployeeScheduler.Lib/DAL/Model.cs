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


        //    protected override void OnConfiguring(DbContextOptionsBuilder options)
        //        => options.UseSqlite("Data Source=blogging.db");
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
    //public class BloggingContext : DbContext
    //{
    //    public DbSet<Blog> Blogs { get; set; }
    //    public DbSet<Post> Posts { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder options)
    //        => options.UseSqlite("Data Source=blogging.db");
    //}

    //public class Blog
    //{
    //    public int BlogId { get; set; }
    //    public string Url { get; set; }

    //    public List<Post> Posts { get; } = new List<Post>();
    //}

    //public class Post
    //{
    //    public int PostId { get; set; }
    //    public string Title { get; set; }
    //    public string Content { get; set; }

    //    public int BlogId { get; set; }
    //    public Blog Blog { get; set; }
    //}
}
