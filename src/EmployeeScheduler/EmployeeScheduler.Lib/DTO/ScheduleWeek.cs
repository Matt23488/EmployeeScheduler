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
        public List<ScheduleDay> Days { get; set; } = new List<ScheduleDay>
        {
            new ScheduleDay(),
            new ScheduleDay(),
            new ScheduleDay(),
            new ScheduleDay(),
            new ScheduleDay(),
            new ScheduleDay(),
            new ScheduleDay()
        };
    }
}
