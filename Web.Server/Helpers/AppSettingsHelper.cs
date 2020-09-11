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
    }
}
