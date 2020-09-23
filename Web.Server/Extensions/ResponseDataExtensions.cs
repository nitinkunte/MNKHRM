using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web.DTO.Data;

namespace Web.Server.Extensions
{
    public static class ResponseDataExtensions
    {
        public static T Deserialize<T>(this ResponseData responseData)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
            };
            var json = responseData.Data.ToString();
            return JsonSerializer.Deserialize<T>(json, options);
        }



    }


}
