using NServiceBus.ContainerTests;
using NServiceBus.ObjectBuilder.CastleWindsor;
using NUnit.Framework;

[SetUpFixture]
public class SetUpFixture
{
    public SetUpFixture()
    {
        TestContainerBuilder.ConstructBuilder = () => new WindsorObjectBuilder();
    }

}