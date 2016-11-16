namespace NServiceBus.CastleWindsor.AcceptanceTests
{
    using System;
    using System.Collections;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    class ContainerDecorator : IWindsorContainer
    {
        IWindsorContainer decorated;

        public ContainerDecorator(IWindsorContainer decorated)
        {
            this.decorated = decorated;
        }

        public void Dispose()
        {
            decorated.Dispose();
            Disposed = true;
        }

        public bool Disposed { get; private set; }

        public void AddChildContainer(IWindsorContainer childContainer)
        {
        }

        public IWindsorContainer AddFacility(IFacility facility)
        {
            return null;
        }

        public IWindsorContainer AddFacility<TFacility>() where TFacility : IFacility, new()
        {
            return null;
        }

        public IWindsorContainer AddFacility<TFacility>(Action<TFacility> onCreate) where TFacility : IFacility, new()
        {
            return null;
        }

        public IWindsorContainer GetChildContainer(string name)
        {
            return null;
        }

        public IWindsorContainer Install(params IWindsorInstaller[] installers)
        {
            return null;
        }

        public IWindsorContainer Register(params IRegistration[] registrations)
        {
            return null;
        }

        public void Release(object instance)
        {
        }

        public void RemoveChildContainer(IWindsorContainer childContainer)
        {
        }

        public object Resolve(string key, Type service)
        {
            return null;
        }

        public object Resolve(Type service)
        {
            return null;
        }

        public object Resolve(Type service, IDictionary arguments)
        {
            return null;
        }

        public object Resolve(Type service, object argumentsAsAnonymousType)
        {
            return null;
        }

        public T Resolve<T>()
        {
            return default(T);
        }

        public T Resolve<T>(IDictionary arguments)
        {
            return default(T);
        }

        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return default(T);
        }

        public T Resolve<T>(string key)
        {
            return default(T);
        }

        public T Resolve<T>(string key, IDictionary arguments)
        {
            return default(T);
        }

        public T Resolve<T>(string key, object argumentsAsAnonymousType)
        {
            return default(T);
        }

        public object Resolve(string key, Type service, IDictionary arguments)
        {
            return null;
        }

        public object Resolve(string key, Type service, object argumentsAsAnonymousType)
        {
            return null;
        }

        public T[] ResolveAll<T>()
        {
            return new T[]
            {
            };
        }

        public Array ResolveAll(Type service)
        {
            return null;
        }

        public Array ResolveAll(Type service, IDictionary arguments)
        {
            return null;
        }

        public Array ResolveAll(Type service, object argumentsAsAnonymousType)
        {
            return null;
        }

        public T[] ResolveAll<T>(IDictionary arguments)
        {
            return new T[]
            {
            };
        }

        public T[] ResolveAll<T>(object argumentsAsAnonymousType)
        {
            return new T[]
            {
            };
        }

        public IWindsorContainer AddComponent(string key, Type classType)
        {
            return null;
        }

        public IWindsorContainer AddComponent(string key, Type serviceType, Type classType)
        {
            return null;
        }

        public IWindsorContainer AddComponent<T>()
        {
            return null;
        }

        public IWindsorContainer AddComponent<T>(string key)
        {
            return null;
        }

        public IWindsorContainer AddComponent<I, T>() where T : class
        {
            return null;
        }

        public IWindsorContainer AddComponent<I, T>(string key) where T : class
        {
            return null;
        }

        public IWindsorContainer AddComponentLifeStyle(string key, Type classType, LifestyleType lifestyle)
        {
            return null;
        }

        public IWindsorContainer AddComponentLifeStyle(string key, Type serviceType, Type classType, LifestyleType lifestyle)
        {
            return null;
        }

        public IWindsorContainer AddComponentLifeStyle<T>(LifestyleType lifestyle)
        {
            return null;
        }

        public IWindsorContainer AddComponentLifeStyle<T>(string key, LifestyleType lifestyle)
        {
            return null;
        }

        public IWindsorContainer AddComponentLifeStyle<I, T>(LifestyleType lifestyle) where T : class
        {
            return null;
        }

        public IWindsorContainer AddComponentLifeStyle<I, T>(string key, LifestyleType lifestyle) where T : class
        {
            return null;
        }

        public IWindsorContainer AddComponentProperties<I, T>(IDictionary extendedProperties) where T : class
        {
            return null;
        }

        public IWindsorContainer AddComponentProperties<I, T>(string key, IDictionary extendedProperties) where T : class
        {
            return null;
        }

        public IWindsorContainer AddComponentWithProperties(string key, Type classType, IDictionary extendedProperties)
        {
            return null;
        }

        public IWindsorContainer AddComponentWithProperties(string key, Type serviceType, Type classType, IDictionary extendedProperties)
        {
            return null;
        }

        public IWindsorContainer AddComponentWithProperties<T>(IDictionary extendedProperties)
        {
            return null;
        }

        public IWindsorContainer AddComponentWithProperties<T>(string key, IDictionary extendedProperties)
        {
            return null;
        }

        public IWindsorContainer AddFacility(string idInConfiguration, IFacility facility)
        {
            return null;
        }

        public IWindsorContainer AddFacility<TFacility>(string idInConfiguration) where TFacility : IFacility, new()
        {
            return null;
        }

        public IWindsorContainer AddFacility<TFacility>(string idInConfiguration, Action<TFacility> configureFacility) where TFacility : IFacility, new()
        {
            return null;
        }

        public object Resolve(string key, IDictionary arguments)
        {
            return null;
        }

        public object Resolve(string key, object argumentsAsAnonymousType)
        {
            return null;
        }

        public IKernel Kernel { get; }
        public string Name { get; }
        public IWindsorContainer Parent { get; set; }

        object IWindsorContainer.this[string key]
        {
            get { return null; }
        }

        object IWindsorContainer.this[Type service]
        {
            get { return null; }
        }
    }
}