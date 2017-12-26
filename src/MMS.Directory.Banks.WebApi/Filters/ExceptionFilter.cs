using Microsoft.Owin.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace MMS.Directory.Banks.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ErrorHandlerAttribute : Attribute, IExceptionFilter
    {
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext ctx, CancellationToken cancellationToken)
        {
            if (ctx.Exception != null)
            {
                var logger = (ILogger)ctx.ActionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ILogger));
                var ex = ctx.Exception;
                var action = ctx.ActionContext.ActionDescriptor;

                logger.WriteError($"{ action.ActionName} error", ex);
            }

            return Task.FromResult<object>(null);
        }

        public bool AllowMultiple => false;
    }
}
