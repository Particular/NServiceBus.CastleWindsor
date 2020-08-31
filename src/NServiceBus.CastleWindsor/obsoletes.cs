namespace NServiceBus
{
    // An internal type referenced by the API approvals test as it can't reference obsoleted types.
    class CastleWindsorInternalType
    {
    }

    /// <summary>
    /// Windsor Container
    /// </summary>
    [ObsoleteEx(
        Message = "Castle Windsor is no longer supported via the NServiceBus.CastleWindsor adapter. NServiceBus directly supports all containers compatible with Microsoft.Extensions.DependencyInjection.Abstractions via the externally managed container mode.",
        RemoveInVersion = "9.0.0",
        TreatAsErrorFromVersion = "8.0.0")]
    public class WindsorBuilder
    {
    }

    /// <summary>
    /// Windsor extension to pass an existing Windsor container instance.
    /// </summary>
    [ObsoleteEx(
        Message = "Castle Windsor is no longer supported via the NServiceBus.CastleWindsor adapter. NServiceBus directly supports all containers compatible with Microsoft.Extensions.DependencyInjection.Abstractions via the externally managed container mode.",
        RemoveInVersion = "9.0.0",
        TreatAsErrorFromVersion = "8.0.0")]
    public static class WindsorExtensions
    {
    }
}