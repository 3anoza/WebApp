using System.Web.Mvc;
using System.Web.Routing;

namespace Gallery.Filters.MVC
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(
            ExceptionContext filterContext)
        {
            throw filterContext.Exception;
            // RedirectToRouteResult res = new RedirectToRouteResult("",new RouteValueDictionary());
        }
    }
}