using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Data.SqlClient;
using HTNResp.Models;
using HTNResp.Codes;

namespace HTNResp.Controllers
{
    [Login,Authority]
    public class ControlContentController : BaseController
    {
        HTNResp.BLL.ControlContent bll = new HTNResp.BLL.ControlContent();
        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult AjaxList()
        {
            int start = 0;  // 分页起始条目数
            int length = 0; // 分页每页分页数
            string strSql = "";
            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            StringBuilder sb = new StringBuilder("", 100);
            if (!string.IsNullOrEmpty(Request.Params["Title"]))    //返回值是空，非空执行
            {
                strSql += "Title LIKE'%" + Request.Params["Title"] + "%'";
            }

            if (!string.IsNullOrEmpty(Request.Params["Remark"]))
            {
                if (!string.IsNullOrEmpty(strSql))
                    strSql += " and  Remark LIKE'%" + Request.Params["Remark"] + "%'";
                else
                    strSql += "Remark LIKE'%" + Request.Params["Remark"] + "%'";
            }
            if (!string.IsNullOrEmpty(Request.Params["ControlType"]))
            {
                if (strSql != "")
                    strSql += " and ControlType ='" + Request.Params["ControlType"] + "'";
                else
                    strSql += "ControlType ='" + Request.Params["ControlType"] + "'";
            }
            sb.Append(strSql);

            List<HTNResp.Model.ControlContent> list = bll.DataTableToList(bll.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int total = bll.GetRecordCount("");
            int totalFilter = bll.GetRecordCount(strSql);   //查到的页数

            return this.Json(new
            {
                result = 1,
                data = new
                {
                    draw = Request.Params["draw"],
                    recordsTotal = total,
                    recordsFiltered = totalFilter,
                    data = list
                }
            });
        }

        [Login(Feedback = 100)]
        public ActionResult Add(HTNResp.Model.ControlContent model)
        {
            bool result = false;
            if ((model.ControlType == "input" || model.ControlType == "checkbox")&&!bll.Exists(model.ControlName))
            {
                if (bll.Add(model))
                    result = true;
            }
            if(result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "增加失败" });
        }

        [Login(Feedback = 100)]
        public ActionResult Update(Model.ControlContent model)
        {
            bool result = false;
            if (model != null)
            {
                if (model.ControlType == "input" || model.ControlType == "checkbox")
                {
                    result = bll.Update(model);
                }
            }

            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "修改失败" });
        }

        public override ActionResult Remove()
        {
            bool result = false;
            if (Request.Params["ControlName"] != null)
            {
                result = bll.Delete(Request.Params["ControlName"]);
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "删除失败" });
        }

        [Login(Feedback = 100)]
        public new ActionResult Info(string controlName)
        {
            bool result = false;
            Model.ControlContent model = null;
            if (controlName != null)
            {
                model = bll.GetModel(controlName);
                result = model == null ? false : true;
            }
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "没有此项" });
        }
    }
}
