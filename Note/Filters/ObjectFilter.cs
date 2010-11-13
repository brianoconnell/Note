using System;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;

namespace Note.Filters
{
    public class ObjectFilter : ActionFilterAttribute
    {
        public string Param { get; set; }
        public Type RootType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if((filterContext.HttpContext.Request.ContentType ?? string.Empty).Contains("application/json"))
            {
                var o = new DataContractJsonSerializer(RootType).ReadObject(filterContext.HttpContext.Request.InputStream);
                filterContext.ActionParameters[Param] = o;
            }
        }
    }
}