using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Win.Utilities
{
    public class DependencyResolver
    {
        private readonly Dictionary<string, ImplementationData> _types = new Dictionary<string, ImplementationData>();

        public void RegisterDependency<TTarget, TImplementation>()
            where TTarget : class
            where TImplementation : class, TTarget
            => RegisterDependency<TTarget, TImplementation>(obj => { });

        public void RegisterDependency<TTarget, TImplementation>(Action<TImplementation> setupFunc)
            where TTarget : class
            where TImplementation : class, TTarget
        {
            _types[typeof(TTarget).FullName] = new ImplementationData
            {
                Type = typeof(TImplementation),
                SetupFunction = obj => setupFunc((TImplementation)obj)
            };
        }

        public TTarget GetDependency<TTarget>() where TTarget : class
            => (TTarget)GetDependency(typeof(TTarget));

        private object GetDependency(Type target)
        {
            var name = target.FullName;
            if (!_types.ContainsKey(name)) throw new InvalidOperationException($"No dependency of type \"{name}\" was registered.");

            var implementationData = _types[name];

            var paramValues = new List<object>();
            var constructor = implementationData.Type.GetConstructors().First();
            foreach (var parameter in constructor.GetParameters())
            {
                paramValues.Add(GetDependency(parameter.ParameterType));
            }

            var implementationInstance = Activator.CreateInstance(implementationData.Type, paramValues.ToArray());
            implementationData.SetupFunction(implementationInstance);

            return implementationInstance;
        }

        private class ImplementationData
        {
            public Type Type { get; set; }
            public Action<object> SetupFunction { get; set; }
        }
    }
}
