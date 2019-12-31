using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DTO
{
    public class EmployeeSchedule
    {
        public int EmployeeID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
