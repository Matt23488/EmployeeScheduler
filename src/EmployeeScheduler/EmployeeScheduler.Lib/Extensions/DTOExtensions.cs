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
    }
}
