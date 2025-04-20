namespace ComplaintSystem.Api.Extension
{
    public class ApiExceptionResponse : APIResponse
    {
        public string Details { get; set; }
        public ApiExceptionResponse(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
           Details = details;
        }
    }

}
