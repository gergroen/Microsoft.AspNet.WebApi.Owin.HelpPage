using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace Microsoft.AspNet.WebApi.Owin.HelpPage.WebApiDocumentation
{
    public static class WebApiDocumentationExtension
    {
        public static WebApiDocumentation GetWebApiDocumentation(this HttpConfiguration config)
        {
            var documatation = new WebApiDocumentation();
            IApiExplorer apiExplorer = config.Services.GetApiExplorer();
            var documentationProvider = config.Services.GetDocumentationProvider();
            HttpControllerDescriptor previousControllerDescriptor = null;
            WebApiControllerDocumentation currentControllerDocumentation = null;
            foreach (ApiDescription api in apiExplorer.ApiDescriptions)
            {
                if (previousControllerDescriptor == null || !previousControllerDescriptor.Equals(api.ActionDescriptor.ControllerDescriptor))
                {
                    previousControllerDescriptor = api.ActionDescriptor.ControllerDescriptor;
                    var controllerDescription = documentationProvider.GetDocumentation(previousControllerDescriptor);

                    currentControllerDocumentation = new WebApiControllerDocumentation
                    {
                        Name = previousControllerDescriptor.ControllerName,
                        Description = controllerDescription
                    };

                    documatation.Controllers.Add(currentControllerDocumentation);
                }
                var description = documentationProvider.GetDocumentation(api.ActionDescriptor);
                var returns = documentationProvider.GetResponseDocumentation(api.ActionDescriptor);

                var actionDocumentation = new WebApiControllerActionDocumentation();
                actionDocumentation.HttpMethod = api.HttpMethod.ToString();
                actionDocumentation.RelativePath = api.RelativePath;
                actionDocumentation.Description = description;
                actionDocumentation.ResponseDescription = returns;
                actionDocumentation.ResponseType = api.ResponseDescription.DeclaredType;

                foreach (ApiParameterDescription parameter in api.ParameterDescriptions)
                {
                    var parameterDescription = documentationProvider.GetDocumentation(parameter.ParameterDescriptor);

                    var parameterDocumentation = new WebApiControllerActionParameterDocumentation();
                    parameterDocumentation.Name = parameter.Name;
                    parameterDocumentation.Description = parameterDescription;
                    parameterDocumentation.Type = parameter.ParameterDescriptor.ParameterType;
                    parameterDocumentation.Source = parameter.Source.ToString();

                    actionDocumentation.Parameters.Add(parameterDocumentation);
                }
                currentControllerDocumentation.Actions.Add(actionDocumentation);
            }
            return documatation;
        }
    }
}
