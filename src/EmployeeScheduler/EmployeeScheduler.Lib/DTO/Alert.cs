using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.DTO
{
    public class Alert
    {
        public string Text { get; set; }
        public AlertType Type { get; set; }
    }

    public enum AlertType
    {
        Info = 0,
        Success,
        Warning,
        Error
    }
}
