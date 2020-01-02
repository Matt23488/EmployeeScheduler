using EmployeeScheduler.Win.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeScheduler.Win
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DependencyResolver.RegisterDependency<Lib.Services.ISchedulingService, Lib.BLL.JsonFileSchedulingService>();
            DependencyResolver.RegisterDependency<Views.ITest1View, UserControls.Test1>();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Container());
        }
    }
}
