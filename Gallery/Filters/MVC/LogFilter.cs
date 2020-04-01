using System;
using System.Web;
using System.Web.Mvc;

namespace Gallery.Filters.MVC
{
    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(
            ActionExecutedContext filterContext)
        {
            LogConfig.WriteToLogs("======== Response Information =======");
            LogConfig.WriteToLogs("[OK]");
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(
            ActionExecutingContext filterContext)
        {
            /*string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string action = filterContext.ActionDescriptor.ActionName;
            string ip = filterContext.HttpContext.Request.UserHostAddress;
            string dns = filterContext.HttpContext.Request.UserHostName;
            string browser = filterContext.HttpContext.Request.Browser.Browser;
            DateTime dateTime = filterContext.HttpContext.Timestamp;
            string username = filterContext.HttpContext.User.Identity.Name;*/
            LogConfig.WriteToLogs("======== Request Information =====");
            LogConfig.WriteToLogs(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
            LogConfig.WriteToLogs(filterContext.ActionDescriptor.ActionName);
            LogConfig.WriteToLogs(filterContext.HttpContext.Request.UserHostAddress);
            LogConfig.WriteToLogs(filterContext.HttpContext.Request.UserHostName);
            LogConfig.WriteToLogs(filterContext.HttpContext.Request.Browser.Browser);
            LogConfig.WriteToLogs(filterContext.HttpContext.Timestamp.ToString());
            LogConfig.WriteToLogs(filterContext.HttpContext.User.Identity.Name);
            base.OnActionExecuting(filterContext);
        }
    }
}