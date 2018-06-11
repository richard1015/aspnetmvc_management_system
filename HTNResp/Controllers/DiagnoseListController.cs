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
    public class DiagnoseListController : BaseController
    {
        HTNResp.BLL.DiagnoseList bll = new HTNResp.BLL.DiagnoseList();

        public override ActionResult Index()
        {
            BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            List<Model.EvalGuid> evalGuidId = bllEvalGuid.GetModelList("");

            ViewBag.EvalGuidId = evalGuidId;
            return View();
        }

        public override ActionResult AjaxList()
        {
            int start = 0;  // 分页起始条目数
            int length = 0; // 分页每页分页数
            string strSql = "Status=1";

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            StringBuilder sb = new StringBuilder("", 100);
            if (!string.IsNullOrEmpty(Request.Params["DecisionRule"]))    //返回值是空，非空执行
            {
                strSql += " and DecisionRule like'%" + Request.Params["DecisionRule"] + "%'";
            }
            if (!string.IsNullOrEmpty(Request.Params["Name"]))
            {
                strSql += " and Name like'%" + Request.Params["Name"] + "%'";
            }
            if (!string.IsNullOrEmpty(Request.Params["Sex"]))
            {
                strSql += " and Sex ='" + Request.Params["Sex"] + "'";
            }
            if (!string.IsNullOrEmpty(Request.Params["starDescriptiont"]))
            {
                strSql += " and Description like'%" + Request.Params["stDescriptionart"] + "%'";
            }
            if (!string.IsNullOrEmpty(Request.Params["EvalGuidId"]))
            {
                strSql += " and EvalGuidId='" + Request.Params["EvalGuidId"] + "'";
            }

            sb.Append(strSql);

            List<HTNResp.Model.DiagnoseList> list = bll.DataTableToList(bll.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int total = bll.GetRecordCount("Status=1");
            int totalFilter = bll.GetRecordCount(strSql);

            BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            return this.Json(new
            {
                result = 1,
                data = new
                {
                    draw = Request.Params["draw"],
                    recordsTotal = total,
                    recordsFiltered = totalFilter,
                    data = list.Select(o => new
                    {
                        ID = o.ID,
                        DecisionRule = o.DecisionRule,
                        Name = o.Name,
                        Sex = o.Sex,
                        Description = o.Description,
                        EvalGuidId = o.EvalGuidId,
                        EvalGuid = bllEvalGuid.GetModel(o.EvalGuidId.Value).GuidName,
                    })
                }
            });
        }
        [Login(Feedback = 100)]
        public ActionResult Create(Model.DiagnoseList model)
        {
            bool result = false;
            model.Status = 1;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.DecisionRule) && String.IsNullOrEmpty(bllProgram.testContent(model.DecisionRule))&&model.EvalGuidId.Value!=null&&bllEvalGuid.Exists(model.EvalGuidId.Value))
            {
                if (bll.Add(model) != 0)
                    result = true;
            }
            if(result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "新建失败" });

        }
        [Login(Feedback = 100)]
        public ActionResult Update(Model.DiagnoseList model)
        {
            bool result = false;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.DecisionRule) && String.IsNullOrEmpty(bllProgram.testContent(model.DecisionRule)) && model.EvalGuidId.Value != null && bllEvalGuid.Exists(model.EvalGuidId.Value))
            {
                result = bll.Update(model);
            }

            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "修改失败" });
        }

        public override ActionResult Delete(int id)
        {
            Model.DiagnoseList model = bll.GetModel(id);
            bool result = false;
            if(model!=null){
                model.Status = null;
                result = bll.Update(model);
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "删除失败" });
        }

        public override ActionResult Info(int id)
        {
            Model.DiagnoseList model = bll.GetModel(id);
            bool result = model != null ? true : false;
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "没有此条信息" });
        }

        public override ActionResult Confirm()
        {
            string msg = "";
            string content = Request.Params["string"];
            bool result = true;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            msg = bllProgram.testContent(content);
            if (!String.IsNullOrEmpty(msg))
            {
                result = false;
                msg = msg.Remove(msg.Length - 1);
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = msg });

        }
    }
}
