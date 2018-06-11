using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTNResp.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult UsageDetail()
        {
            return View();
        }
        public virtual ActionResult Confirm()
        {
            return View();
        }
        public virtual ActionResult Delete(int i)
        {
            return View();
        }
        public virtual ActionResult Remove()
        {
            return View();
        }

        public virtual ActionResult AjaxList()
        {
            return View();
        }
        public virtual ActionResult Info(int i)
        {
            return View();
        }

        public virtual ActionResult Modify()
        {
            return View();
        }

        public virtual ActionResult Detail()
        {
            return View();
        }

        public virtual ActionResult ProjectDetail()
        {
            return View();
        }
    }

}
