using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DTO
{
    public class ScheduleWeek
    {
        public long ID { get; set; }
        //public long ID => Date.Ticks;
        //public DateTime Date { get; set; }
        //public ScheduleDay Sunday { get; set; } = new ScheduleDay();
        //public ScheduleDay Monday { get; set; } = new ScheduleDay();
        //public ScheduleDay Tuesday { get; set; } = new ScheduleDay();
        //public ScheduleDay Wednesday { get; set; } = new ScheduleDay();
        //public ScheduleDay Thursday { get; set; } = new ScheduleDay();
        //public ScheduleDay Friday { get; set; } = new ScheduleDay();
        //public ScheduleDay Saturday { get; set; } = new ScheduleDay();
        public List<EmployeeSchedule> Sunday { get; set; }
        public List<EmployeeSchedule> Monday { get; set; }
        public List<EmployeeSchedule> Tuesday { get; set; }
        public List<EmployeeSchedule> Wednesday { get; set; }
        public List<EmployeeSchedule> Thursday { get; set; }
        public List<EmployeeSchedule> Friday { get; set; }
        public List<EmployeeSchedule> Saturday { get; set; }
    }
}
