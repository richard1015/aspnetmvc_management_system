using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace HTNResp.Codes
{
    public class AuthorityAttribute:ActionFilterAttribute
    {
        private int feedback = 0;

        public int Feedback
        {
            get { return feedback; }
            set { feedback = value; }
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            HttpRequestBase bases = (HttpRequestBase)filterContext.HttpContext.Request;
            string url = bases.RawUrl.ToString().ToLower();
            if (!string.IsNullOrEmpty(url))
            {
                string[] temp = Regex.Split(url, "/");
                if (filterContext.HttpContext.Session["access"] != null && !string.IsNullOrEmpty(filterContext.HttpContext.Session["access"].ToString()))
                {
                    string session = filterContext.HttpContext.Session["access"].ToString().ToLower();
                    if (!session.Contains(temp[1]))
                    {
                        if (this.feedback == 100)
                            filterContext.Result = new RedirectResult("/Home/Illegal");
                        else if (this.feedback == 300)
                            filterContext.Result = new JsonResult
                            {
                                Data = new { result = feedback, msg = "" }
                            };
                    }
                }
            }
        }
    }
}