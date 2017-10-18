namespace NServiceBus.Features
{
    /// <summary>
    /// Adds diagnostics information for NServiceBus.CastleWindsor
    /// </summary>
    public class WindsorDiagnostics : Feature
    {
        /// <summary>
        /// Constructor for NServiceBus.CastleWindsor diagnostics feature
        /// </summary>
        public WindsorDiagnostics()
        {
            EnableByDefault();
        }

        /// <summary>
        /// Sets up CastleWindsor diagnostics
        /// </summary>
        /// <param name="context"></param>
        protected override void Setup(FeatureConfigurationContext context)
        {
            context.Settings.AddStartupDiagnosticsSection("NServiceBus.CastleWindsor", new
            {
                UsingExistingContainer = context.Settings.HasSetting<WindsorBuilder.ContainerHolder>()
            });
        }
    }
}
