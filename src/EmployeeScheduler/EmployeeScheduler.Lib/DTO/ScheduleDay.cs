using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DTO
{
    // Maybe a way to denote a closure of some kind
    public class ScheduleDay
    {
        public bool IsClosed { get; set; }
        public List<EmployeeSchedule> EmployeeSchedules { get; set; }
    }
}
