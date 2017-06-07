namespace NServiceBus.AcceptanceTests.CastleWindsor
{
    using System.Threading.Tasks;
    using AcceptanceTesting;
    using AcceptanceTests;
    using EndpointTemplates;
    using NUnit.Framework;
    using Castle.Windsor;
    using Castle.Facilities.TypedFactory;

    public class When_using_TypedFactoryFacility : NServiceBusAcceptanceTest
    {
        [Test]
        public Task should_not_error()
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
                    config.UseContainer<WindsorBuilder>(b => b.ExistingContainer(container));
                });
            }

            public class MyMessageHandler : IHandleMessages<MyMessage>
            {
                public async Task Handle(MyMessage message, IMessageHandlerContext context)
                {
                    await context.SendLocal(new Reply());
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