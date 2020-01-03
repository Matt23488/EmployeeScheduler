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
            var dependencyResolver = new DependencyResolver();
            DependencyConfig.RegisterDependencies(dependencyResolver);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Container(dependencyResolver));
        }
    }
}
