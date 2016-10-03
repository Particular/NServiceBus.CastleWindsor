namespace NServiceBus.AcceptanceTests
{
    using Castle.Windsor;
    using NServiceBus;
    using AcceptanceTesting;
    using EndpointTemplates;
    using CastleWindsor.AcceptanceTests;
    using NUnit.Framework;

    public class When_using_externally_owned_container : NServiceBusAcceptanceTest
    {
        [Test]
        public void Should_shutdown_properly()
        {
            Scenario.Define<Context>()
                .WithEndpoint<Endpoint>()
                .Done(c => c.EndpointsStarted)
                .Run();

            Assert.IsFalse(Endpoint.Decorator.Disposed);
            Assert.DoesNotThrow(() => Endpoint.Container.Dispose());
        }

        private class Context : ScenarioContext
        {
        }

        class Endpoint : EndpointConfigurationBuilder
        {
            public static IWindsorContainer Container { get; set; }
            public static ContainerDecorator Decorator { get; set; }

            public Endpoint()
            {
                EndpointSetup<DefaultServer>(config =>
                {
                    var container = new WindsorContainer();
                    var decorator = new ContainerDecorator(container);

                    config.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

                    Container = container;
                    Decorator = decorator;
                });
            }
        }
    }
}