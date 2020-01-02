using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeScheduler.Win.Views
{
    public interface IViewBase
    {
        Control Control { get; }
    }
}
