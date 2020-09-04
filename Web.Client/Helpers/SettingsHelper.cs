using System;
namespace Web.Client.Helpers
{
    public class LocalConfig
    {
        public AppSettings AppSettings { get; set; }
    }

    /// <summary>
    /// Represents the AppSettings section in appsettings.json file.
    /// </summary>
    public class AppSettings
    {
        public string ApiBaseURL { get; set; }

    }
}
