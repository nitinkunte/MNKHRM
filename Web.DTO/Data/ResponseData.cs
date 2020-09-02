using System;
namespace Web.DTO.Data
{
    public class ResponseData
    {
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
                StatusCode = statusCode,
                Message = message,
                Data = objectData
            };

            return response;
        }
    }
}
