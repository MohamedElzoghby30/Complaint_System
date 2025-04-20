using System.Net;
using System.Text.Json;


namespace ComplaintSystem.Api.Extension
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ExceptionMiddleware(RequestDelegate _next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment webHostEnvironment)
        {
            this.requestDelegate = _next;
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);//Development
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = webHostEnvironment.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var opption = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, opption);
                await httpContext.Response.WriteAsync(json);

            }

        }

    }
}
