using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTNResp.Codes;
using System.Text.RegularExpressions;

namespace HTNResp.Controllers
{
    [Login,Authority]
    public class GrassRootsController : BaseController
    {
        //
        // GET: /GrassRoots/
        BLL.GrassRootsOralMedicine bll = new BLL.GrassRootsOralMedicine();

        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult Modify()
        {
            return View();
        }

        public override ActionResult Detail()
        {
            int start = 0;
            int length = 0;
            string sqlString = "";
            string common = Request.Params["common"];
            
            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);
            if (!String.IsNullOrEmpty(common))
                sqlString += " CommonName LIKE ('%" + common + "%')";


            List<Model.GrassRootsOralMedicine> list = bll.DataTableToList(bll.GetListByPage(sqlString, "ID ASC", start + 1, start + length).Tables[0]);

            int filterTotal = bll.GetRecordCount(sqlString);
            int total = bll.GetRecordCount("");

            return this.Json(new
            {
                result = 1,
                data = new
                {
                    draw = Request.Params["draw"],
                    recordsTotal = total,
                    recordsFiltered = filterTotal,
                    data = list
                }
            });
        }

        public override ActionResult Info(int id)
        {
            bool result = false;
            Model.GrassRootsOralMedicine m = bll.GetModel(id);
            result = m != null ? true : false;
            if(result)
                return this.Json(new { result = 1, data = m }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg="查询失败" });
                
        }

        [Login(Feedback=100)]
        public ActionResult Update(Model.GrassRootsOralMedicine model)
        {
            bool r = bll.Update(model);
            if (r)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "修改失败" });
        }

        public override ActionResult Delete(int id)
        {
            bool r = bll.Delete(id);
            if (r)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "删除失败" });
        }

        [Login(Feedback = 100)]
        public ActionResult Create(Model.GrassRootsOralMedicine model)
        {
            int result = bll.Add(model);
            if (result!=0)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "增加失败" });
        }
    }
}
