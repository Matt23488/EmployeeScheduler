using EmployeeScheduler.Win.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeScheduler.Win.UserControls
{
    public abstract class UserControlBase : UserControl, IViewBase
    {
        public Control AsControl => this;
    }
}
