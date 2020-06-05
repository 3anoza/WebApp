using System.Web.Mvc;

namespace Gallery.Filters.MVC
{
    public class ValidateActionFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isValid = filterContext.Controller.ViewData.ModelState.IsValid;
            if (!isValid)
                filterContext.RequestContext.HttpContext.Response.StatusCode = 400;
            base.OnActionExecuting(filterContext);
        }
    }
}