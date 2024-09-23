using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IPMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
          );


            routes.MapRoute(
               name: "portsDashboard",
               url: "Ports/DashBoard/{userid}",
               defaults: new { controller = "Ports", action = "DashBoard", userid = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "EditAgent",
                url: "AgentRegistration/Registration/{applicantId}",
                defaults: new { controller = "AgentRegistration", action = "Registration", applicantId = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AppplicantVerification",
                url: "AgentRegistration/ApplicantRegistration/{applicantId}",
                defaults: new { controller = "AgentRegistration", action = "ApplicantRegistration", applicantId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserVerification",
                url: "UserRegistration/UserRegistration/{applicantId}",
                defaults: new { controller = "UserRegistration", action = "UserRegistration", applicantId = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "PortDelete",
            url: "Ports/ManagePorts/{portId}/{isView}",
            defaults: new { controller = "Ports", action = "ManagePorts", portId = UrlParameter.Optional, isView = UrlParameter.Optional }
        );
            routes.MapRoute(
          name: "Berth",
          url: "Berth/GetBerths",
          defaults: new { controller = "Berth", action = "GetBerths" }
      );
           
        }
    }
}
