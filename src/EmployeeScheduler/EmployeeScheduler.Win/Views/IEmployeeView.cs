using EmployeeScheduler.Lib.DTO;
using EmployeeScheduler.Win.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Win.Views
{
    public interface IEmployeeView : IViewBase
    {
        Employee Employee { get; set; }
        EditingMode EditingMode { get; set; }

        event EventHandler Cancel;
        event EventHandler Save;
    }
}
