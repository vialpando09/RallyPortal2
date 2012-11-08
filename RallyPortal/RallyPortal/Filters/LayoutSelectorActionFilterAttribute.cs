using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RallyPortal.Filters
{
    public class LayoutSelectorActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string actionName = (string)filterContext.RouteData.Values["action"];
            string controllerName = (string)filterContext.RouteData.Values["controller"];

            dynamic viewBag = filterContext.Controller.ViewBag;
            
            if(controllerName == "Home"|| controllerName == "Account" || actionName == "Details" || actionName == "Delete")
                viewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            else
                viewBag.Layout = "~/Views/Shared/_Administration.cshtml";
            
        }
    }
}