namespace NServiceBus.AcceptanceTests.CastleWindsor
{
    using System.Threading.Tasks;
    using AcceptanceTesting;
    using AcceptanceTests;
    using Castle.Facilities.TypedFactory;
    using Castle.Windsor;
    using EndpointTemplates;
    using NUnit.Framework;

    public class When_using_TypedFactoryFacility : NServiceBusAcceptanceTest
    {
        [Test]
        public Task Should_not_error()
        {
            return Scenario.Define<Context>()
                .WithEndpoint<Endpoint>(b => b.When(bus => bus.SendLocal(new MyMessage())))
                .Run();
        }

        public class Context : ScenarioContext
        {
        }

        public class Endpoint : EndpointConfigurationBuilder
        {
            public Endpoint()
            {
                var container = new WindsorContainer();
                container.AddFacility<TypedFactoryFacility>();

                EndpointSetup<DefaultServer>(config =>
                {
                    config.SendFailedMessagesTo("error");
#pragma warning disable 0618
                    config.UseContainer<WindsorBuilder>(b => b.ExistingContainer(container));
#pragma warning restore 0618
                });
            }

            public class MyMessageHandler : IHandleMessages<MyMessage>
            {
                public async Task Handle(MyMessage message, IMessageHandlerContext context)
                {
                    await context.SendLocal(new Reply()).ConfigureAwait(false);
                }
            }

        }

        public class Reply : ICommand
        {
        }

        public class MyMessage : ICommand
        {
        }
    }
}