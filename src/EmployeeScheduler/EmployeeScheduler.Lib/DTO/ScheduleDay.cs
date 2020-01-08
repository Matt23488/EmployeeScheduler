using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DTO
{
    public class ScheduleDay
    {
        public bool IsClosed { get; set; }
        public List<EmployeeSchedule> EmployeeSchedules { get; set; }
    }
}
