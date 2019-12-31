using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DTO
{
    public class ScheduleWeek
    {
        public ScheduleDay Sunday { get; set; }
        public ScheduleDay Monday { get; set; }
        public ScheduleDay Tuesday { get; set; }
        public ScheduleDay Wednesday { get; set; }
        public ScheduleDay Thursday { get; set; }
        public ScheduleDay Friday { get; set; }
        public ScheduleDay Saturday { get; set; }
    }
}
