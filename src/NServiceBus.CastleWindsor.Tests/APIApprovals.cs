using NServiceBus.ObjectBuilder.CastleWindsor;
using NUnit.Framework;
using Particular.Approvals;
using PublicApiGenerator;

[TestFixture]
public class APIApprovals
{
    [Test]
    public void Approve()
    {
        var publicApi = ApiGenerator.GeneratePublicApi(typeof(WindsorObjectBuilder).Assembly, excludeAttributes: new[] { "System.Runtime.Versioning.TargetFrameworkAttribute" });
        Approver.Verify(publicApi);
    }
}