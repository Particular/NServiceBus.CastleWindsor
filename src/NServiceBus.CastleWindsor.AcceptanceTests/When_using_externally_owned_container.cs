namespace NServiceBus.CastleWindsor.AcceptanceTests
{
    using System.Threading.Tasks;
    using Castle.Windsor;
    using NServiceBus;
    using AcceptanceTesting;
    using NServiceBus.AcceptanceTests;
    using NServiceBus.AcceptanceTests.EndpointTemplates;
    using NUnit.Framework;

    public class When_using_externally_owned_container : NServiceBusAcceptanceTest
    {
        [Test]
        public async Task Should_shutdown_properly()
        {
            var context = await Scenario.Define<Context>()
                .WithEndpoint<Endpoint>()
                .Done(c => c.EndpointsStarted)
                .Run().ConfigureAwait(false);

            Assert.IsFalse(context.Decorator.Disposed);
            Assert.DoesNotThrow(() => context.Container.Dispose());
        }

        class Context : ScenarioContext
        {
            public IWindsorContainer Container { get; set; }
            public ContainerDecorator Decorator { get; set; }
        }

        class Endpoint : EndpointConfigurationBuilder
        {
            public Endpoint()
            {
                EndpointSetup<DefaultServer>((config, desc) =>
                {
                    var container = new WindsorContainer();
                    var decorator = new ContainerDecorator(container);

                    config.SendFailedMessagesTo("error");
                    config.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

                    var context = (Context)desc.ScenarioContext;
                    context.Container = container;
                    context.Decorator = decorator;
                });
            }
        }
    }
}