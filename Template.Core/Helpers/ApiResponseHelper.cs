using System.Net;

namespace Template.Core.Helpers
{
    public static class ApiResponseHelper
    {
        public static ApiResponse<T> Create<T>(HttpStatusCode status, string message, T data, string url = null)
        {
            return new ApiResponse<T>
            {
                Status = (int)status,
                Message = message,
                Url = url,
                Data = data
            };
        }
    }

    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public T Data { get; set; }
    }
}
