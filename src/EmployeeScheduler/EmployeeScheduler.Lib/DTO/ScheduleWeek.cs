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
        public ScheduleDay Sunday { get; set; } = new ScheduleDay();
        public ScheduleDay Monday { get; set; } = new ScheduleDay();
        public ScheduleDay Tuesday { get; set; } = new ScheduleDay();
        public ScheduleDay Wednesday { get; set; } = new ScheduleDay();
        public ScheduleDay Thursday { get; set; } = new ScheduleDay();
        public ScheduleDay Friday { get; set; } = new ScheduleDay();
        public ScheduleDay Saturday { get; set; } = new ScheduleDay();
    }
}
