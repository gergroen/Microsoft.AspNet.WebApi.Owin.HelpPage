using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.WebApi.Owin.HelpPage.WebApiDocumentation
{
    public class WebApiControllerActionDocumentation
    {
        public string HttpMethod { get; set; }
        public string RelativePath { get; set; }
        public string Description { get; set; }
        public List<WebApiControllerActionParameterDocumentation> Parameters { get; set; } = new List<WebApiControllerActionParameterDocumentation>();
        public string ResponseDescription { get; set; }
        public Type ResponseType { get; set; }
    }
}
