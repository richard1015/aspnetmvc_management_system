using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace HTNResp.Codes
{
    public class LoginAttribute : ActionFilterAttribute
    {
        private int feedback;
        private string name;
        public int Feedback
        {
            get { return feedback; }
            set { feedback = value; }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Buffer = true;
            filterContext.HttpContext.Response.Buffer = true;
            filterContext.HttpContext.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            filterContext.HttpContext.Response.Expires = 0;
            filterContext.HttpContext.Response.CacheControl = "no-cache";
            filterContext.HttpContext.Response.Cache.SetNoStore();
            if (feedback == -7)
            {
                name = (string)filterContext.HttpContext.Session["username"];
            }
            else
            {
                if (filterContext.HttpContext.Session["username"] == null || string.IsNullOrEmpty(filterContext.HttpContext.Session["username"].ToString()))
                {
                    switch (this.feedback)
                    {
                        case 200:
                            filterContext.Result = new RedirectResult("/Home/Index"); break;
                        case 100:
                            filterContext.Result = new JsonResult
                            {
                                Data = new { result = feedback, msg = "" }
                            }; break;
                    }
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }

            // session

        }
    }
}