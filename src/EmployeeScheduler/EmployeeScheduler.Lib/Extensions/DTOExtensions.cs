using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Lib.Extensions
{
    public static class DTOExtensions
    {
        public static string FullName(this DTO.Employee employee)
            => $"{employee.FirstName} {employee.LastName}";

        public static string FormattedName(this DTO.Employee employee)
            => $"{employee.LastName}, {employee.FirstName}";

        public static string CssClass(this DTO.Alert alert)
        {
            switch (alert.Type)
            {
                case DTO.AlertType.Info: return "list-group-item-info";
                case DTO.AlertType.Success: return "list-group-item-success";
                case DTO.AlertType.Warning: return "list-group-item-warning";
                case DTO.AlertType.Error: return "list-group-item-danger";
                default: return string.Empty;
            }
        }

        public static DateTime Date(this DTO.ScheduleWeek schedule)
            => new DateTime(schedule.ID);
    }
}
