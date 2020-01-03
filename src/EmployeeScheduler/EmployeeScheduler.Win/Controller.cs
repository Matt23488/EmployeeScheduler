using EmployeeScheduler.Lib.Services;
using EmployeeScheduler.Win.Utilities;
using EmployeeScheduler.Win.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Win
{
    public class Controller
    {
        private readonly IViewContainer _container;

        private IEmployeeView _employeeView;
        private IEmployeeListView _employeeListView;

        private ISchedulingService _schedulingService;

        public Controller(IViewContainer container, DependencyResolver dependencyResolver)
        {
            _container = container;

            ResolveDependencies(dependencyResolver);
            WireEvents();

            //_container.CurrentView = _employeeListView;
            NavigateToEmployeeList();
        }

        private void ResolveDependencies(DependencyResolver dependencyResolver)
        {
            _employeeView = dependencyResolver.GetDependency<IEmployeeView>();
            _employeeListView = dependencyResolver.GetDependency<IEmployeeListView>();

            _schedulingService = dependencyResolver.GetDependency<ISchedulingService>(); 
        }

        private void WireEvents()
        {
            _employeeListView.AddNewEmployee += (s, e) =>
            {
                _employeeView.EditingMode = EditingMode.Insert;
                _employeeView.Employee = new Lib.DTO.Employee();
                _container.CurrentView = _employeeView;
            };

            _employeeListView.EditEmployee += (s, e) =>
            {
                _employeeView.EditingMode = EditingMode.Update;
                _employeeView.Employee = _employeeListView.SelectedEmployee;
                _container.CurrentView = _employeeView;
            };

            _employeeView.Cancel += async (s, e) =>
            {
                await NavigateToEmployeeListAsync();
            };

            _employeeView.Save += async (s, e) =>
            {
                if (_employeeView.EditingMode == EditingMode.Insert)
                {
                    _employeeView.Employee = await _schedulingService.AddEmployeeAsync(_employeeView.Employee);
                    _employeeView.EditingMode = EditingMode.Update;
                }
                else if (_employeeView.EditingMode == EditingMode.Update)
                {
                    await _schedulingService.UpdateEmployeeAsync(_employeeView.Employee);
                }
                await NavigateToEmployeeListAsync();
            };
        }

        private void NavigateToEmployeeList()
        {
            _employeeListView.Employees = _schedulingService.GetEmployees(includeDeleted: true);
            _container.CurrentView = _employeeListView;
        }

        private async Task NavigateToEmployeeListAsync()
        {
            _employeeListView.Employees = await _schedulingService.GetEmployeesAsync(includeDeleted: true);
            _container.CurrentView = _employeeListView;
        }
    }
}
