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
        public async Task Should_not_error()
        {
            var context = await Scenario.Define<Context>()
                .WithEndpoint<Endpoint>(b => b.When(bus => bus.SendLocal(new MyMessage())))
                .Done(c => c.GotTheMessage)
                .Run();

            Assert.True(context.GotTheMessage);
        }

        public class Context : ScenarioContext
        {
            public bool GotTheMessage { get; set; }
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
                readonly Context testContext;

                public MyMessageHandler(Context testContext)
                {
                    this.testContext = testContext;
                }
                public Task Handle(MyMessage message, IMessageHandlerContext context)
                {
                    testContext.GotTheMessage = true;

                    return Task.FromResult(0);
                }
            }
        }

        public class MyMessage : ICommand
        {
        }
    }
}