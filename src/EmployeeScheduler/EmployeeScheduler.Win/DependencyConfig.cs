using EmployeeScheduler.Win.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Win
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies(DependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterDependency<Lib.Services.ISchedulingService, Lib.BLL.Win.JsonFileSchedulingService>(service =>
            {
                service.JsonFilePath = @"data\employees.json";
            });

            dependencyResolver.RegisterDependency<Lib.Services.IJsonFileService, Lib.BLL.Win.JsonFileService>();
            dependencyResolver.RegisterDependency<Views.IEmployeeView, UserControls.Employee>();
            dependencyResolver.RegisterDependency<Views.IEmployeeListView, UserControls.EmployeeList>();
        }
    }
}
