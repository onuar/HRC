using System;

namespace HRC.Foundation.DependencyInjection
{
    public interface IInjectionContainer
    {
        void Bind<TKey, TConcrete>() where TConcrete : TKey;
        void Bind<T>(T instance);
        TKey Resolve<TKey>();
        //object ResolveByType(Type type);
        object Resolve(Type type);
    }
}