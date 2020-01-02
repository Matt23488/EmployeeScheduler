using EmployeeScheduler.Win.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Win
{
    public class Controller
    {
        private readonly Container _container;

        private Views.ITest1View _test1;
        private UserControls.Test2 _test2;

        public Controller(Container container)
        {
            _container = container;

            _test1 = DependencyResolver.GetDependency<Views.ITest1View>();
            _test2 = new UserControls.Test2();

            _container.CurrentControl = _test1.Control;

            _test1.TestEvent += (s, e) =>
            {
                _container.CurrentControl = _test2;
            };

            _test2.TestEvent += (s, e) =>
            {
                _container.CurrentControl = _test1.Control;
            };
        }
    }
}
