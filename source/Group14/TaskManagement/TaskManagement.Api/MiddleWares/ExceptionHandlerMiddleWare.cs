#region [Using]
using System.Net;
#endregion [Using]


namespace TaskManagement.Api.MiddleWares
{
    #region [Summary]
    ///<author>sayyad, shaheena</author>
    ///<date>22-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>ExceptionHandlerMiddleWare</class>
    /// <summary>
    /// This is the Exception Handling for all action methods
    /// </summary>
    #endregion [Summary]
    public class ExceptionHandlerMiddleWare
    {
        private readonly ILogger<ExceptionHandlerMiddleWare> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleWare(ILogger<ExceptionHandlerMiddleWare> logger,
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
                logger.LogError(ex, $"{errorId} : {ex.Message}");

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
