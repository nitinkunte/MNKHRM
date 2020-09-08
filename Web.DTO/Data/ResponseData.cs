using System;
using System.Net;

namespace Web.DTO.Data
{
    public class ResponseData
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }

        public static ResponseData Create(bool success, string message = null, Object objectData = null)
        {
            int statusCode;
            if (success)
            { statusCode = 200; }
            else { statusCode = 400; }

            var response = new ResponseData
            {
                IsSuccess = success,
                StatusCode = statusCode,
                Message = message,
                Data = objectData
            };

            return response;
        }

        public static ResponseData Create(HttpStatusCode httpStatusCode, string message = null, Object objectData = null)
        {
            int statusCode = (int)httpStatusCode;
            bool isSuccess = (statusCode.ToString().StartsWith("20"));

            var response = new ResponseData
            {
                IsSuccess = isSuccess,
                StatusCode = statusCode,
                Message = httpStatusCode.ToString(),
                Data = objectData
            };

            return response;
        }
    }
}
