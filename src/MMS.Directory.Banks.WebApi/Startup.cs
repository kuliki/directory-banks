using Autofac;
using Autofac.Integration.WebApi;
using MMS.Directory.Banks.EntityFramework;
using MMS.Directory.Banks.Gms;
using Newtonsoft.Json.Converters;
using Owin;
using Serilog;
using Swashbuckle.Application;
using System.Reflection;
using System.Web.Http;

namespace MMS.Directory.Banks.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var container = BuildContainer();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            RegisterRoutes(config);
            RegisterSwagger(config);

            app.UseWebApi(config);
            app.UseAutofacMiddleware(container);

            config.EnsureInitialized();
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(Log.Logger);

            builder.UseEntityFrameworkStorage();
            builder.UseBanksServices();
            builder.UseGms();

            builder.RegisterApiControllers(Assembly.GetAssembly(typeof(Startup))).InstancePerRequest();

            return builder.Build();
        }

        private void RegisterSwagger(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.RootUrl(req => AppConfig.ServiceUrl.Replace("+", req.RequestUri.DnsSafeHost));
                    c.SingleApiVersion("v1", "MMS.Directory.Banks");
                    c.UseFullTypeNameInSchemaIds();
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocExpansion(DocExpansion.List);
                    c.DisableValidator();
                });
        }

        private void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("default", "{controller}/{action}");
        }
    }
}