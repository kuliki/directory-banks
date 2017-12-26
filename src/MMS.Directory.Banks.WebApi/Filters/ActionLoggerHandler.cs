using Serilog;
using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MMS.Directory.Banks.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionLoggerHandler : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var logger = (ILogger)actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ILogger));

            using (var textWriter = new StringWriter())
            {
                var action = $"{actionContext.ControllerContext.ControllerDescriptor.ControllerType.Name}.{actionContext.ActionDescriptor.ActionName}";
                var json = JsonSerialize(actionContext.RequestContext.Configuration.Formatters.JsonFormatter, actionContext.ActionArguments);

                logger.Information($"Action '{action}' called: {json}");
            }
        }

        private string JsonSerialize<T>(JsonMediaTypeFormatter formatter, T value)
        {
            using (var textWriter = new StringWriter())
            {
                var serializer = formatter.CreateJsonSerializer();
                serializer.Serialize(textWriter, value);

                return textWriter.GetStringBuilder().ToString();
            }
        }
    }
}
