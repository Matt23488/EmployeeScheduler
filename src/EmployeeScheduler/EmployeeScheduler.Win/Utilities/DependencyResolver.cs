using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Win.Utilities
{
    public static class DependencyResolver
    {
        private static readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

        public static void RegisterDependency<TTarget, TImplementation>()
            where TTarget : class
            where TImplementation : class, TTarget
        {
            _types[typeof(TTarget).FullName] = typeof(TImplementation);
        }

        public static TTarget GetDependency<TTarget>() where TTarget : class
            => (TTarget)GetDependency(typeof(TTarget));

        private static object GetDependency(Type target)
        {
            var name = target.FullName;
            if (!_types.ContainsKey(name)) throw new InvalidOperationException($"No dependency of type \"{name}\" was registered.");

            var implementationType = _types[name];

            var paramValues = new List<object>();
            var constructor = implementationType.GetConstructors().First();
            foreach (var parameter in constructor.GetParameters())
            {
                paramValues.Add(GetDependency(parameter.ParameterType));
            }

            return Activator.CreateInstance(implementationType, paramValues.ToArray());
        }
    }
}
