using System;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace Microsoft.AspNet.WebApi.Owin.OwinSelfHostHelpPage
{
    internal class XmlDocumentationProvider : IDocumentationProvider
    {
        private string _xmlDocumentationFile;

        public XmlDocumentationProvider(string xmlDocumentationFile)
        {
            _xmlDocumentationFile = xmlDocumentationFile;
        }

        public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            return "";
        }

        public string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            return "";
        }

        public string GetDocumentation(HttpControllerDescriptor controllerDescriptor)
        {
            return "";
        }

        public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
        {
            return "";
        }
    }
}