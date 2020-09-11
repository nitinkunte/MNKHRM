using System;
using System.Security.Claims;
using System.Threading.Tasks;
namespace Web.Server.Helpers
{
    public class TokenProvider
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
