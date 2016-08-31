namespace NServiceBus.AcceptanceTests.CastleWindsor
{
    using System.Threading.Tasks;
    using NServiceBus.AcceptanceTesting;
    using NServiceBus.AcceptanceTests;
    using NServiceBus.AcceptanceTests.EndpointTemplates;
    using NUnit.Framework;
    using Castle.Windsor;
    using Castle.Facilities.TypedFactory;
    using System;
    using Newtonsoft.Json;
    public class When_using_TypedFactoryFacility : NServiceBusAcceptanceTest
    {
        [Test]
        public async Task should_not_error()
        {
            var res = await Scenario.Define<Context>()
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
                    config.ExcludeAssemblies(typeof(NewtonsoftSerializer).Assembly.FullName);
                    config.UseSerialization<NewtonsoftSerializer>();
                    config.UseContainer<WindsorBuilder>(b => b.ExistingContainer(container));
                });
            }

            public class MyMessageHandler : IHandleMessages<MyMessage>
            {
                public async Task Handle(MyMessage message, IMessageHandlerContext context)
                {
                    await context.SendLocal(new Reply { });
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