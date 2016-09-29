namespace NServiceBus
{
    using Castle.Windsor;
    using Container;
    using ObjectBuilder.CastleWindsor;
    using Settings;

    /// <summary>
    /// Windsor Container
    /// </summary>
    public class WindsorBuilder : ContainerDefinition
    {
        /// <summary>
        ///     Implementers need to new up a new container.
        /// </summary>
        /// <param name="settings">The settings to check if an existing container exists.</param>
        /// <returns>The new container wrapper.</returns>
        public override ObjectBuilder.Common.IContainer CreateContainer(ReadOnlySettings settings)
        {
            ContainerHolder containerHolder;

            if (settings.TryGet(out containerHolder))
            {
                return new WindsorObjectBuilder(containerHolder.ExistingContainer);

            }

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