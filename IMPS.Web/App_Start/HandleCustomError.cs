﻿using IPMS.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;

namespace IPMS.Web.App_Start
{
    public class HandleCustomError : HandleErrorAttribute
{
        public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            //If the exeption is already handled we do nothing
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            else
            {
                //Determine the return type of the action
                string actionName = filterContext.RouteData.Values["action"].ToString();
                Type controllerType = filterContext.Controller.GetType() ;
                var method = controllerType.GetMethod(actionName);
                var returnType = method.ReturnType;

                //If the action that generated the exception returns JSON
                if (returnType.Equals(typeof(JsonResult)))
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = "Return data here"
                    };
                }

                //If the action that generated the exception returns a view
                if (returnType.Equals(typeof(ActionResult)) 
                || (returnType).IsSubclassOf(typeof(ActionResult)))
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "URL to the errror page"
                    };
                }

                
            }

            //Make sure that we mark the exception as handled
            filterContext.ExceptionHandled = true;
        }
    }
}