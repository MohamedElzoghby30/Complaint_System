namespace ComplaintSystem.Api.Extension
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }


        public APIResponse(int _StatusCode, string? _Message = null)
        {
            StatusCode = _StatusCode;

            Message = _Message ?? GetStatusCodeAndMessage(_StatusCode);
        }
        private string GetStatusCodeAndMessage(int StatusCode)
        {

            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unexpected Error"
            };
        }
    }
}
