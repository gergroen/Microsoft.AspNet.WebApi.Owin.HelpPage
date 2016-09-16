using System;

namespace Microsoft.AspNet.WebApi.Owin.HelpPage.WebApiDocumentation
{
    public class WebApiControllerActionParameterDocumentation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public Type Type { get; set; }
    }
}
