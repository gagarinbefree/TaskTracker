using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskTracker.API.LogAttributes
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logger = context.HttpContext.RequestServices
                .GetService<ILogger<LogActionAttribute>>();

            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;

            logger?.LogInformation($"Executing action {actionName} in controller {controllerName}");

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var logger = context.HttpContext.RequestServices
                .GetService<ILogger<LogActionAttribute>>();

            logger?.LogInformation($"Action {context.ActionDescriptor} executed with result: {context.Result}");

            base.OnActionExecuted(context);
        }
    }
}
