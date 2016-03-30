namespace NServiceBus.ObjectBuilder.CastleWindsor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Castle.Core;
    using Castle.MicroKernel.Lifestyle;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using NServiceBus.Logging;
    using NServiceBus.ObjectBuilder.Common;

    class WindsorObjectBuilder : IContainer
    {
        public WindsorObjectBuilder() : this(new WindsorContainer())
        {
        }

        public WindsorObjectBuilder(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container), "The object builder must be initialized with a valid windsor container");
            }

            this.container = container;
            scope = container.BeginScope();
        }

        public void Dispose()
        {
            //Injected at compile time
        }

        public IContainer BuildChildContainer()
        {
            return new WindsorObjectBuilder(container)
            {
                isChild = true
            };
        }


        public void Configure(Type concreteComponent, DependencyLifecycle dependencyLifecycle)
        {
            ThrowIfCalledOnChildContainer();

            var registrations = container.Kernel.GetAssignableHandlers(concreteComponent).Select(x => x.ComponentModel);

            if (registrations.Any())
            {
                Logger.Info("Component " + concreteComponent.FullName + " was already registered in the container.");
                return;
            }

            var lifestyle = GetLifestyleTypeFrom(dependencyLifecycle);
            var services = GetAllServiceTypesFor(concreteComponent);

            container.Register(Component.For(services).ImplementedBy(concreteComponent).LifeStyle.Is(lifestyle));
        }

        void IContainer.Configure<T>(Func<T> componentFactory, DependencyLifecycle dependencyLifecycle)
        {
            ThrowIfCalledOnChildContainer();

            var componentType = typeof(T);
            var registrations = container.Kernel.GetAssignableHandlers(componentType).Select(x => x.ComponentModel);

            if (registrations.Any())
            {
                Logger.Info("Component " + componentType.FullName + " was already registered in the container.");
                return;
            }

            var lifestyle = GetLifestyleTypeFrom(dependencyLifecycle);
            var services = GetAllServiceTypesFor(componentType);

            container.Register(Component.For(services).UsingFactoryMethod(componentFactory).LifeStyle.Is(lifestyle));
        }

        public void RegisterSingleton(Type lookupType, object instance)
        {
            ThrowIfCalledOnChildContainer();

            var lookupTypeFullname = lookupType.FullName;
            var registration = container.Kernel.GetHandler(lookupTypeFullname);

            if (registration != null)
            {
                registration.ComponentModel.ExtendedProperties["instance"] = instance;
                return;
            }

            var services = GetAllServiceTypesFor(instance.GetType()).Union(new[]
            {
                lookupType
            });

            container.Register(Component.For(services).Activator<ExternalInstanceActivatorWithDecommissionConcern>().Instance(instance).NamedAutomatically(lookupTypeFullname).LifestyleSingleton());
        }

        public object Build(Type typeToBuild)
        {
            return container.Resolve(typeToBuild);
        }

        public IEnumerable<object> BuildAll(Type typeToBuild)
        {
            return container.ResolveAll(typeToBuild).Cast<object>();
        }

        public bool HasComponent(Type componentType)
        {
            return container.Kernel.HasComponent(componentType);
        }

        public void Release(object instance)
        {
            container.Release(instance);
        }

        void DisposeManaged()
        {
            scope.Dispose();

            //if we are in a child scope dispose of that but not the parent container
            if (!isChild)
            {
                container?.Dispose();
            }
        }

        void ThrowIfCalledOnChildContainer()
        {
            if (isChild)
            {
                throw new InvalidOperationException("Reconfiguration of child containers is not allowed.");
            }
        }

        static LifestyleType GetLifestyleTypeFrom(DependencyLifecycle dependencyLifecycle)
        {
            switch (dependencyLifecycle)
            {
                case DependencyLifecycle.InstancePerCall:
                    return LifestyleType.Transient;
                case DependencyLifecycle.SingleInstance:
                    return LifestyleType.Singleton;
                case DependencyLifecycle.InstancePerUnitOfWork:
                    return LifestyleType.Scoped;
            }

            throw new ArgumentException("Unhandled lifecycle - " + dependencyLifecycle);
        }

        static IEnumerable<Type> GetAllServiceTypesFor(Type t)
        {
            return t.GetInterfaces()
                .Where(x => !x.FullName.StartsWith("System."))
                .Concat(new[] { t });
        }

        IWindsorContainer container;
        bool isChild;
        IDisposable scope;
        static ILog Logger = LogManager.GetLogger<WindsorObjectBuilder>();
    }
}