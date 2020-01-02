using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Win.Views
{
    public interface ITest1View : IViewBase
    {
        event EventHandler TestEvent;
    }
}
