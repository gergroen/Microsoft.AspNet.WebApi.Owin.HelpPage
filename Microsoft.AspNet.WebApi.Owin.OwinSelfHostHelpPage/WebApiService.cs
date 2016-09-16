using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Web.Http;
using WebApplication1.Areas.HelpPage;
using Microsoft.AspNet.WebApi.Owin.HelpPage.WebApiDocumentation;

namespace Microsoft.AspNet.WebApi.Owin.OwinSelfHostHelpPage
{
    public class WebApiService
    {
        private IDisposable _webAppDisposible;

        public WebApiService()
        {
        }

        public bool Start()
        {
            string baseAddress = "http://localhost/";

            Console.WriteLine("Start OWIN host");
            _webAppDisposible = WebApp.Start(baseAddress, Configuration);
            return true;
        }

        public bool Stop()
        {
            Console.WriteLine("Stop OWIN host");
            _webAppDisposible.Dispose();
            return true;
        }

        private void Configuration(IAppBuilder appBuilder)
        {
            Console.WriteLine("Configure Web API for self-host");
            HttpConfiguration config = new HttpConfiguration();
            config.SetDocumentationProvider(new XmlDocumentationProvider("Microsoft.AspNet.WebApi.Owin.OwinSelfHostHelpPage.XML"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            appBuilder.UseWebApi(config);

            var documatation = config.GetWebApiDocumentation();
            CreateConsoleWebApiDocumentation(documatation);
        }

        private static void CreateConsoleWebApiDocumentation(WebApiDocumentation documatation)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            foreach (var controllerDocumentation in documatation.Controllers)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"|============================================================================");
                Console.WriteLine($"|Controller {controllerDocumentation.Name}");
                Console.WriteLine($"|-Description: {controllerDocumentation.Description}");
                Console.WriteLine($"|============================================================================");

                foreach (var actionDocumentation in controllerDocumentation.Actions)
                {
                    Console.WriteLine($"|");
                    Console.WriteLine($"|============================================================================");
                    Console.WriteLine($"|Action {actionDocumentation.HttpMethod} {actionDocumentation.RelativePath}");
                    Console.WriteLine($"|-Description: {actionDocumentation.Description}");
                    Console.WriteLine($"|============================================================================");
                    foreach (var parameterDocumentation in actionDocumentation.Parameters)
                    {
                        Console.WriteLine($"|Parameter {parameterDocumentation.Name}");
                        Console.WriteLine($"|-Description: {parameterDocumentation.Description}");
                        Console.WriteLine($"|-Type: {parameterDocumentation.Type.FullName}");
                        Console.WriteLine($"|-Source: {parameterDocumentation.Source}");
                        Console.WriteLine($"|============================================================================");
                    }

                    Console.WriteLine($"|Return");
                    Console.WriteLine($"|-Description: {actionDocumentation.ResponseDescription}");
                    Console.WriteLine($"|-Type: {actionDocumentation.ResponseType.FullName}");
                    Console.WriteLine($"|============================================================================");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}