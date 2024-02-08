using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace DiscussionForumUserInterface.Common
{
    /// <summary author="Kirti Garg">
    /// This is exception logging class.
    /// </summary>
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;
        private readonly RequestDelegate next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Log This Exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                // Return A Custom Exrror Response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are looking into resolving this."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
