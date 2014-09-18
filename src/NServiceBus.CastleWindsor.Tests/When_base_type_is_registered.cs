using NServiceBus.ObjectBuilder.CastleWindsor;
using NUnit.Framework;

[TestFixture]
public class When_base_type_is_registered
{
    [Test]
    [Explicit]
    public void Should_be_able_to_resolve_base_when_child_registered_first()
    {
        using (var builder = new WindsorObjectBuilder())
        {
            var instance = new Child();
            builder.RegisterSingleton(typeof(Child), instance);
            builder.RegisterSingleton(typeof(Base), instance);
            Assert.IsNotNull(builder.Build(typeof(Child)));
            Assert.IsNotNull(builder.Build(typeof(Base)));
        }
    }

    public class Child : Base
    {
    }

    public class Base
    {
    }
}