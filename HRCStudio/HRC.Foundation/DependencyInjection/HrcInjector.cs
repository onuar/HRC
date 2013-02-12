using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HRC.Foundation.DependencyInjection
{
    public class HrcInjector : IInjectionContainer
    {
        private readonly Dictionary<Type, Func<object>> _providers = new Dictionary<Type, Func<object>>();

        public void Bind<TKey, TConcrete>() where TConcrete : TKey
        {
            _providers[typeof(TKey)] = () => ResolveByType(typeof(TConcrete));
        }

        public void Bind<T>(T instance)
        {
            _providers[typeof(T)] = () => instance;
        }

        public TKey Resolve<TKey>()
        {
            return (TKey)Resolve(typeof(TKey));
        }

        public object Resolve(Type type)
        {
            Func<object> provider;
            if (_providers.TryGetValue(type, out provider))
            {
                return provider();
            }
            return ResolveByType(type);
        }

        private object ResolveByType(Type type)
        {
            var constructor = type.GetConstructors().SingleOrDefault();
            if (constructor != null)
            {
                var arguments =
                    constructor.GetParameters()
                    .Select(p => Resolve(p.ParameterType)).ToArray();

                return constructor.Invoke(arguments);
            }
            var instanceProperty = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public);
            if (instanceProperty == null)
            {
                return Activator.CreateInstance(type);
            }
            return instanceProperty.GetValue(null, null);
        }
    }
}