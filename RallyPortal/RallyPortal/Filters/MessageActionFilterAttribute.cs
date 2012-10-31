using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RallyPortal.Filters
{
    public class MessageActionFilterAttribute : ActionFilterAttribute
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

            dynamic viewBag = filterContext.Controller.ViewBag;
            dynamic tempData = filterContext.Controller.TempData;

            if (tempData["ViewData"] != null)
            {
                filterContext.Controller.ViewData = (ViewDataDictionary)tempData["ViewData"];
            }

            viewBag.GlobalMessageType = tempData["GlobalMessageType"];
            viewBag.GlobalHeader = tempData["ViewBag.GlobalHeader"];
            viewBag.GlobalMessage = tempData["ViewBag.GlobalMessage"];
        }
    }
}