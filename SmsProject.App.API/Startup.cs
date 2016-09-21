using Microsoft.Owin;
using Owin;
using SmsProject.App.API.Common.Extensibility;
using SmsProject.App.API.App_Start;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Net.Http.Formatting;

[assembly: OwinStartup(typeof(SmsProject.App.API.Startup))]

namespace SmsProject.App.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            SwaggerConfig.Register(config);
            config.MapHttpAttributeRoutes();

            //activate .xml and .json in Uri Path Extension
            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "application/xml");
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");

            //Exception handler and logger added
            config.Services.Replace(typeof(IExceptionLogger), new ExceptionLoggerInterceptor());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            //Handlers(interceptors) added
            config.MessageHandlers.Add(new LoggingInterceptor());

            appBuilder.UseWebApi(config);
        }
    }
}