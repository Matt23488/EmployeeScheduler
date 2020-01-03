using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Win.Views
{
    public interface IEmployeeListView : IViewBase
    {
        List<Lib.DTO.Employee> Employees { get; set; }
        Lib.DTO.Employee SelectedEmployee { get; set; }

        event EventHandler AddNewEmployee;
        event EventHandler EditEmployee;
    }
}
