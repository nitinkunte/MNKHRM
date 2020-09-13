using System;
namespace Web.Server.Helpers
{
    /// <summary>
    /// Represents the AppSettings section in appsettings.json file.
    /// </summary>
    public class AppSettings
    {
        public string BaseURLForApiService { get; set; }
        public string DefaultAvatarURL { get; set; }
        public AzureAd AzureAd { get; set; }
    }

    public class AzureAd
    {
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string CallbackPath { get; set; }
        public string ApplicationIDURI { get; set; }
    }
}


