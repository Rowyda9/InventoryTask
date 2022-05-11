namespace BackCore.Settings
{
    public class AppSettings
    {
        public string MvcClient { get; set; }

        public string CallingAPIClient { get; set; }

        public bool UseCustomizationData { get; set; }

        public string IdentityUrl { get; set; }

        public string ClientId { get; set; }

        public string Secret { get; set; }

        public string TechnicanRole { get; set; }

        public string DispatcherRole { get; set; }

        public string SupervisorDispatcherRole { get; set; }

        public string logoPath { get; set; }

    }
}