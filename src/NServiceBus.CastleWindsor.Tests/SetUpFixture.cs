using NServiceBus.ContainerTests;
using NServiceBus.ObjectBuilder.CastleWindsor;
using NUnit.Framework;

[SetUpFixture]
public class SetUpFixture
{
    [SetUp]
    public void Setup()
    {
        TestContainerBuilder.ConstructBuilder = () => new WindsorObjectBuilder();
    }

}