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
    
    public class ProjectDictionaryController : BaseController
    {
        HTNResp.BLL.ProjectDictionary bll = new HTNResp.BLL.ProjectDictionary();

        public override ActionResult Index()
        {

            return View();
        }

        public override ActionResult AjaxList()
        {
            int start = 0;  // 分页起始条目数
            int length = 0; // 分页每页分页数

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            string strSql = " 1=1 ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (!string.IsNullOrEmpty(Request.Params["ProjectName"]))
            {
                sb.Append(" and ProjectName like '%" + Request.Params["ProjectName"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["ProjectCode"]))
            {
                sb.Append(" and ProjectCode like '%" + Request.Params["ProjectCode"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["ProjectType"]))
            {
                sb.Append(" and ProjectType like '%" + Request.Params["ProjectType"] + "%' ");
            }
            strSql += sb.ToString();

            List<HTNResp.Model.ProjectDictionary> list = bll.DataTableToList(bll.GetListByPage(strSql, " ", start + 1, start + length).Tables[0]);
            int total = bll.GetRecordCount("");                //总页数
            int totalFilter = bll.GetRecordCount(strSql);   //查到的页数

            BLL.EvalGuid bllEvalGrid = new BLL.EvalGuid();
            // 返回JSON结果，JSON统一格式为 { result = 1, data = new { } } 或 { result = 0, msg = "" }
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
        //zhushi
        [Login(Feedback=100)]
        public ActionResult Create(Model.ProjectDictionary model)
        {
            bool result = false;
            if (model.ProjectType == "string" || model.ProjectType == "bool" || model.ProjectType == "int")
            {
                result = bll.Add(model) != 0 ? true : false;
                
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "添加失败" });
        }

        public override ActionResult Info(int id)
        {
            Model.ProjectDictionary model = bll.GetModel(id);
            bool result = model != null ? true : false;
            if(result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "" });
        }
        [Login(Feedback=100)]
        public ActionResult Update(Model.ProjectDictionary model)
        {
            bool result = false;
            if (model.ProjectType == "string" || model.ProjectType == "bool" || model.ProjectType == "int")
            {
                result = bll.Update(model);
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "修改失败" });
        }

        public override ActionResult Delete(int ID)
        {
            bool result = bll.Delete(ID);
            if(result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "删除失败" });
        }



    }
}
