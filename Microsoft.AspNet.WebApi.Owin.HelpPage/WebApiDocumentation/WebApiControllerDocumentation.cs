using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.WebApi.Owin.HelpPage.WebApiDocumentation
{
    public class WebApiControllerDocumentation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<WebApiControllerActionDocumentation> Actions { get; set; } = new List<WebApiControllerActionDocumentation>();
    }
}
