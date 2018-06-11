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
    public class PrevensionProjectController : BaseController
    {
        HTNResp.BLL.PrevensionProject bll = new HTNResp.BLL.PrevensionProject();
        HTNResp.BLL.ProjectDictionary bllProject = new BLL.ProjectDictionary();
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
            if (!string.IsNullOrEmpty(Request.Params["ProjectName"]))
            {
                sb.Append(" and ProjectName like '%" + Request.Params["ProjectName"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["DangerousGroup"]))
            {
                sb.Append(" and DangerousGroup='" + Request.Params["DangerousGroup"] + "' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["DecisionRule"]))
            {
                sb.Append(" and DecisionRule like '%" + Request.Params["DecisionRule"] + "%' ");
            }

            if (!string.IsNullOrEmpty(Request.Params["AdaptScope"]))
            {
                sb.Append(" and AdaptScope like '%" + Request.Params["AdaptScope"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["Measure"]))
            {
                sb.Append(" and Measure like '%" + Request.Params["Measure"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["Aim"]))
            {
                sb.Append(" and Aim like '%" + Request.Params["Aim"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["EvalGuidId"]))
            {
                sb.Append(" and EvalGuidId='" + Request.Params["EvalGuidId"] + "'");
            }

            strSql += sb.ToString();

            List<HTNResp.Model.PrevensionProject> list = bll.DataTableToList(bll.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int total = bll.GetRecordCount("Status=1");
            int totalFilter = bll.GetRecordCount(strSql);

            BLL.EvalGuid bllEvalGrid = new BLL.EvalGuid();

            // 返回JSON结果
            // JSON统一格式为 { result = 1, data = new { } } 或 { result = 0, msg = "" }
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
                        ProjectName = o.ProjectName,
                        DangerousGroup = o.DangerousGroup,
                        DecisionRule = o.DecisionRule,
                        AdaptScope = o.AdaptScope,
                        Measure = o.Measure,                        
                        Aim = o.Aim,                        
                        EvalGuidId = o.EvalGuidId,
                        EvalGuid = bllEvalGrid.GetModel(o.EvalGuidId.Value).GuidName,
                    })
                }
            });
        }

        [Login(Feedback = 100)]
        public ActionResult Create(Model.PrevensionProject model)
        {
            bool result = false;
            model.Status = 1;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.DecisionRule) && String.IsNullOrEmpty(bllProgram.testContent(model.DecisionRule)) && model.EvalGuidId.Value != null && bllEvalGuid.Exists(model.EvalGuidId.Value))
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
            Model.PrevensionProject model = bll.GetModel(id);
            bool result = model != null ? true : false;
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 1, msg = "" });
        }

        [Login(Feedback = 100)]
        public ActionResult Update(Model.PrevensionProject model)
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
            Model.PrevensionProject model = bll.GetModel(id);
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
