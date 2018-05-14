using NServiceBus;
using NServiceBus.ContainerTests;
using NServiceBus.ObjectBuilder.CastleWindsor;
using NUnit.Framework;
using System.Linq;

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

    [Test]
    public void Registering_the_same_singleton_for_different_interfaces_should_only_build_one_result_when_building_all()
    {
        using (var builder = new WindsorObjectBuilder())
        {
            var singleton = new SingletonThatImplementsToInterfaces();
            builder.RegisterSingleton(typeof(ISingleton1), singleton);
            builder.RegisterSingleton(typeof(ISingleton2), singleton);

            builder.Configure(typeof(ComponentThatDependsOnMultiSingletons), DependencyLifecycle.InstancePerCall);

            var dependency = (ComponentThatDependsOnMultiSingletons)builder.Build(typeof(ComponentThatDependsOnMultiSingletons));

            Assert.NotNull(dependency.Singleton1);
            Assert.NotNull(dependency.Singleton2);

            var builtTypes = builder.BuildAll(typeof(ISingleton1));

            Assert.AreEqual(builtTypes.Count(), 1);
        }

        //Not supported by,typeof(SpringObjectBuilder));
    }

    public class Child : Base
    {
    }

    public class Base
    {
    }
}