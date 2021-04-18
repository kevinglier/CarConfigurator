namespace CarConfigurator.Models.ApiResults
{
    /// <summary>
    /// API-Response for successful requests
    /// <see>https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api</see>
    /// </summary>
    public class ApiOkResponse : ApiResponse
    {
        public object Result { get; }

        public ApiOkResponse(object result)
            : base(200)
        {
            Result = result;
        }
    }
}