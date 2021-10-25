namespace NServiceBus
{
    using Castle.Windsor;
    using Container;
    using ObjectBuilder.CastleWindsor;
    using Settings;

    /// <summary>
    /// Windsor Container
    /// </summary>
    [ObsoleteEx(
        Message = "Support for external dependency injection containers is no longer provided by NServiceBus adapters for each container library. Instead, the NServiceBus.Extensions.DependencyInjection library provides the ability to use any container that conforms to the Microsoft.Extensions.DependencyInjection container abstraction.",
        RemoveInVersion = "9.0.0",
        TreatAsErrorFromVersion = "8.0.0")]
    public class WindsorBuilder : ContainerDefinition
    {
        /// <summary>
        ///     Implementers need to new up a new container.
        /// </summary>
        /// <param name="settings">The settings to check if an existing container exists.</param>
        /// <returns>The new container wrapper.</returns>
        public override ObjectBuilder.Common.IContainer CreateContainer(ReadOnlySettings settings)
        {
            if (settings.TryGet(out ContainerHolder containerHolder))
            {
                settings.AddStartupDiagnosticsSection("NServiceBus.CastleWindsor", new
                {
                    UsingExistingContainer = true
                });

                return new WindsorObjectBuilder(containerHolder.ExistingContainer);

            }

            settings.AddStartupDiagnosticsSection("NServiceBus.CastleWindsor", new
            {
                UsingExistingContainer = false
            });

            return new WindsorObjectBuilder();
        }

        internal class ContainerHolder
        {
            public ContainerHolder(IWindsorContainer container)
            {
                ExistingContainer = container;
            }

            public IWindsorContainer ExistingContainer { get; }
        }
    }
}