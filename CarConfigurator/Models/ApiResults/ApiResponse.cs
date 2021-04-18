namespace CarConfigurator.Models.ApiResults
{
    /// <summary>
    /// API-Response
    /// <see>https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api</see>
    /// </summary>
    public class ApiResponse
    {
        public int StatusCode { get; }
        public string Message { get; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "BadRequest";
                case 404:
                    return "Resource not found";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }
    }
}