using NServiceBus.ContainerTests;
using NServiceBus.ObjectBuilder.CastleWindsor;
using NUnit.Framework;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void Setup()
    {
        TestContainerBuilder.ConstructBuilder = () => new WindsorObjectBuilder();
    }
}