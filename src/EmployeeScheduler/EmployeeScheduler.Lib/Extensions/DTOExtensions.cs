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
            => $"{employee.FirstName} {employee.LastName}".TrimEnd();

        public static string FormattedName(this DTO.Employee employee)
            => $"{employee.LastName}, {employee.FirstName}".TrimStart().TrimStart(", ".ToArray());

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

        // TODO: Separate this
        // DAL
        public static string FullName(this DAL.Employee employee)
            => $"{employee.FirstName} {employee.LastName}".TrimEnd();

        public static string FormattedName(this DAL.Employee employee)
            => $"{employee.LastName}, {employee.FirstName}".TrimStart().TrimStart(", ".ToArray());

        //public static string CssClass(this DTO.Alert alert)
        //{
        //    switch (alert.Type)
        //    {
        //        case DTO.AlertType.Info: return "list-group-item-info";
        //        case DTO.AlertType.Success: return "list-group-item-success";
        //        case DTO.AlertType.Warning: return "list-group-item-warning";
        //        case DTO.AlertType.Error: return "list-group-item-danger";
        //        default: return string.Empty;
        //    }
        //}

        public static DateTime Date(this DAL.ScheduleWeek schedule)
            => new DateTime(schedule.ScheduleWeekID);

        public static DAL.EmployeeSchedule ToEmployeeSchedule(this DAL.TypicalDay day, DAL.Employee employee)
            => new DAL.EmployeeSchedule
            {
                Employee = employee,
                EmployeeID = employee.EmployeeID,
                IsOff = day.IsOff,
                From = day.From,
                To = day.To,
                LunchType = day.LunchType
            };
    }
}
