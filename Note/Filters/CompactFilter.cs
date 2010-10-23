using System.IO;
using System.Web.Mvc;

namespace Note.Filters
{
    public class CompactFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            response.Filter = new WhitespaceFilter(response.Filter);
            base.OnActionExecuting(filterContext);
        }
    }
}