namespace NServiceBus.CastleWindsor.Tests
{
    using System;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using NUnit.Framework;
    using ObjectBuilder.CastleWindsor;

    [TestFixture]
    public class DisposalTests
    {
        [Test]
        public void Owned_container_should_be_disposed()
        {
            var fakeContainer = new FakeContainer();

            var container = new WindsorObjectBuilder(fakeContainer, true);
            container.Dispose();

            Assert.True(fakeContainer.Disposed);
        }

        [Test]
        public void Externally_owned_container_should_not_be_disposed()
        {
            var fakeContainer = new FakeContainer();

            var container = new WindsorObjectBuilder(fakeContainer, false);
            container.Dispose();

            Assert.False(fakeContainer.Disposed);
        }

        class FakeContainer : IWindsorContainer
        {
            public bool Disposed { get; private set; }

            public void Dispose()
            {
                Disposed = true;
            }

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

            public T Resolve<T>()
            {
                return default(T);
            }

            public T Resolve<T>(string key)
            {
                return default(T);
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

            public object Resolve(Type service, Arguments arguments)
            {
                return null;
            }

            public T Resolve<T>(Arguments arguments)
            {
                return default(T);
            }

            public T Resolve<T>(string key, Arguments arguments)
            {
                return default(T);
            }

            public object Resolve(string key, Type service, Arguments arguments)
            {
                return null;
            }

            public Array ResolveAll(Type service, Arguments arguments)
            {
                return null;
            }

            public T[] ResolveAll<T>(Arguments arguments)
            {
                return new T[]
                {
                };
            }

            public IKernel Kernel { get; }
            public string Name { get; }
            public IWindsorContainer Parent { get; set; }
        }
    }
}