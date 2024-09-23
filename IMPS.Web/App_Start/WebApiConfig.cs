using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
namespace IPMS.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            if (config != null)
            {
                // Web API routes
                config.MapHttpAttributeRoutes();

                // Web API configuration and services
                config.Routes.MapHttpRoute("DefaultApiWithstatus", "Api/{controller}/{action}/{status}",
                    new {status = RouteParameter.Optional});
                config.Routes.MapHttpRoute("DefaultApiWithId", "Api/{controller}/{action}/{id}",
                    new {id = RouteParameter.Optional}, new {id = @"\d+"});
                config.Routes.MapHttpRoute("DefaultApiWithAction", "Api/{controller}/{action}");
                config.Routes.MapHttpRoute("DefaultApiGet", "Api/{controller}", new {action = "Get"});
                config.Routes.MapHttpRoute("DefaultApiPut", "Api/{controller}", new {action = "Put"});



                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new {id = RouteParameter.Optional}
                    );

                var json = config.Formatters.JsonFormatter;
                json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
                config.Formatters.Remove(config.Formatters.XmlFormatter);
            }
        }
    }

}