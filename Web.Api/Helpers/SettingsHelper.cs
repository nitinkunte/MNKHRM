using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Web.Api.Helpers
{
    /// <summary>
    /// Represents the AppSettings section in appsettings.json file.
    /// </summary>
    public class AppSettings
    {
        public string EmailApiKey { get; set; }
        public CloudStorage CloudStorage { get; set; }
        public string DefaultAvatarURL { get; set; }
        public string ResetPasswordSecurityKey { get; set; }
        public string MySQLVersion { get; set; }

        public double OTPTimeoutInMinutes { get; set; }

        public OTPSettings OTPSettings { get; set; }
        public SMSSettings SMSSettings { get; set; }
    }


    public class CloudStorage
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }

    public class OTPSettings
    {
        public string FromEmailAddress { get; set; }
        public string FromEmailName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class SMSSettings
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string FromPhoneNumber { get; set; }
        public string Body { get; set; }
    }


    /// <summary>
    /// Represents the JwtAuthentication section in appsettings.json file
    /// </summary>
    public class JwtAuthenticationSetting
    {
        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public double ExpirationMinutes { get; set; }
        public string CookieName { get; set; }

        public AllowedOriginsForCORS[] AllowedOrigins { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    }

    public class AllowedOriginsForCORS
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
