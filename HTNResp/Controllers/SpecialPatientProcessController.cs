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
    public class SpecialPatientProcessController : BaseController
    {
        HTNResp.BLL.SpecialPatientProcess bll = new HTNResp.BLL.SpecialPatientProcess();
        public override ActionResult Index()
        {
            BLL.EvalGuid blLEvalGrid = new BLL.EvalGuid();
            List<Model.EvalGuid> evalGridId = blLEvalGrid.GetModelList("");

            ViewBag.EvalGridId = evalGridId;
            return View();
        }

        public override ActionResult AjaxList()
        {
            int start = 0;  // 分页起始条目数
            int length = 0; // 分页每页分页数

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            // todo: 读取数据库 获取分页数据
            string strSql = " Status=1 ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(Request.Params["DecisionRule"]))
            {
                sb.Append(" and DecisionRule like '%" + Request.Params["DecisionRule"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["Contents"]))
            {
                sb.Append(" and Contents like '%" + Request.Params["Contents"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["SymptomName"]))
            {
                sb.Append(" and SymptomName like '%" + Request.Params["SymptomName"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["EvalGuidId"]))
            {
                sb.Append(" and EvalGuidId='" + Request.Params["EvalGuidId"] + "'");
            }

            strSql += sb.ToString();

            List<HTNResp.Model.SpecialPatientProcess> list = bll.DataTableToList(bll.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int total = bll.GetRecordCount("");
            int totalFilter = bll.GetRecordCount(strSql);

            BLL.EvalGuid bllEvalGrid = new BLL.EvalGuid();

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
                        SymptomName = o.SymptomName,
                        Contents = o.Contents,
                        EvalGuidId = o.EvalGuidId,
                        EvalGuid = bllEvalGrid.GetModel(o.EvalGuidId.Value).GuidName,
                    })
                }
            });
        }

        [Login(Feedback = 100)]
        public ActionResult Create(Model.SpecialPatientProcess model)
        {
            bool result = false;
            model.Status = 1;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.DecisionRule) && String.IsNullOrEmpty(bllProgram.testContent(model.DecisionRule)) && bllEvalGuid.Exists(model.EvalGuidId.Value))
            {
                if (bll.Add(model) != 0)
                    result = true;
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "新建失败" });
        }

        public override ActionResult Info(int id)
        {
            Model.SpecialPatientProcess model = bll.GetModel(id);
            bool result = model != null ? true : false;
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 1, msg = "" });
        }

        [Login(Feedback = 100)]
        public ActionResult Update(Model.SpecialPatientProcess model)
        {
            bool result = false;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.DecisionRule) && String.IsNullOrEmpty(bllProgram.testContent(model.DecisionRule))  && bllEvalGuid.Exists(model.EvalGuidId.Value))
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
            Model.SpecialPatientProcess model = bll.GetModel(id);
            bool result = false;
            if (model != null)
            {
                model.Status = null;
                result = bll.Update(model);
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "删除失败" });
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
