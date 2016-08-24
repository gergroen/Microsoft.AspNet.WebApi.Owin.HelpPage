using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Xml.Linq;
using System.Xml.XPath;

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
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            appBuilder.UseWebApi(config);


            IApiExplorer apiExplorer = config.Services.GetApiExplorer();
            XDocument xml = XDocument.Load("Microsoft.AspNet.WebApi.Owin.OwinSelfHostHelpPage.XML");

            HttpControllerDescriptor previousControllerDescriptor = null;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            foreach (ApiDescription api in apiExplorer.ApiDescriptions)
            {
                if (previousControllerDescriptor == null || !previousControllerDescriptor.Equals(api.ActionDescriptor.ControllerDescriptor))
                {
                    previousControllerDescriptor = api.ActionDescriptor.ControllerDescriptor;
                    var controllerMemberElementName = GetMemberElementName(previousControllerDescriptor.ControllerType);
                    var controllerDescription = xml.XPathEvaluate($"string(/doc/members/member[@name='{controllerMemberElementName}']/summary)").ToString().Trim();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"|============================================================================");
                    Console.WriteLine($"|Controller {previousControllerDescriptor.ControllerName}");
                    Console.WriteLine($"|-Description: {controllerDescription}");
                    Console.WriteLine($"|============================================================================");
                }
                var actionMember = ((ReflectedHttpActionDescriptor)api.ActionDescriptor).MethodInfo;
                var actionMemberElementName = GetMemberElementName(actionMember);
                var description = xml.XPathEvaluate($"string(/doc/members/member[@name='{actionMemberElementName}']/summary)").ToString().Trim();
                Console.WriteLine($"|");
                Console.WriteLine($"|============================================================================");
                Console.WriteLine($"|Action {api.HttpMethod} {api.RelativePath}");
                Console.WriteLine($"|-Description: {description}");
                Console.WriteLine($"|============================================================================");
                foreach (ApiParameterDescription parameter in api.ParameterDescriptions)
                {
                    var parameterDescription = xml.XPathEvaluate($"string(/doc/members/member[@name='{actionMemberElementName}']/param[@name='{parameter.Name}'])").ToString().Trim();
                    Console.WriteLine($"|Parameter {parameter.Name}");
                    Console.WriteLine($"|-Description: {parameterDescription}");
                    Console.WriteLine($"|-Type: {parameter.ParameterDescriptor.ParameterType.FullName}");
                    Console.WriteLine($"|-Source: {parameter.Source}");
                    Console.WriteLine($"|============================================================================");
                }
                var returns = xml.XPathEvaluate($"string(/doc/members/member[@name='{actionMemberElementName}']/returns)").ToString().Trim();
                Console.WriteLine($"|Return");
                Console.WriteLine($"|-Description: {returns}");
                Console.WriteLine($"|-Type: {actionMember.ReturnType.FullName}");
                Console.WriteLine($"|============================================================================");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        public string GetMemberElementName(MemberInfo member)
        {
            char prefixCode;
            string memberName = (member is Type) ? ((Type)member).FullName  : (member.DeclaringType.FullName + "." + member.Name);

            switch (member.MemberType)
            {
                case MemberTypes.Constructor:
                    memberName = memberName.Replace(".ctor", "#ctor");
                    goto case MemberTypes.Method;
                case MemberTypes.Method:
                    prefixCode = 'M';
                    string paramTypesList = String.Join(
                        ",",
                        ((MethodBase)member).GetParameters()
                            .Cast<ParameterInfo>()
                            .Select(x => x.ParameterType.FullName
                        ).ToArray()
                    );
                    if (!String.IsNullOrEmpty(paramTypesList)) memberName += "(" + paramTypesList + ")";
                    break;

                case MemberTypes.Event: prefixCode = 'E'; break;
                case MemberTypes.Field: prefixCode = 'F'; break;

                case MemberTypes.NestedType:
                    memberName = memberName.Replace('+', '.');
                    goto case MemberTypes.TypeInfo;
                case MemberTypes.TypeInfo:
                    prefixCode = 'T';
                    break;

                case MemberTypes.Property: prefixCode = 'P'; break;

                default:
                    throw new ArgumentException("Unknown member type", "member");
            }

            return String.Format("{0}:{1}", prefixCode, memberName);
        }
    }
}