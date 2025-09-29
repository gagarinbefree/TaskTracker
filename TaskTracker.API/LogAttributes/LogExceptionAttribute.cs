using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskTracker.API.LogAttributes
{
    public class LogExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices
                .GetService<ILogger<LogExceptionAttribute>>();

            logger?.LogError(context.Exception,
                $"Exception in {context.ActionDescriptor.DisplayName}: {context.Exception.Message}");

            base.OnException(context);
        }
    }
}
