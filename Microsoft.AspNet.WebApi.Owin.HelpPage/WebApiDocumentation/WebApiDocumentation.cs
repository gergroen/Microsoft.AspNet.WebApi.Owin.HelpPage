using System.Collections.Generic;

namespace Microsoft.AspNet.WebApi.Owin.HelpPage.WebApiDocumentation
{
    public class WebApiDocumentation
    {
        public List<WebApiControllerDocumentation> Controllers { get; set; } = new List<WebApiControllerDocumentation>();
    }
}
