using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Data.SqlClient;
using HTNResp.Models;
using System.Text.RegularExpressions;
using HTNResp.Codes;
namespace HTNResp.Controllers
{
    [Login,Authority]
    public class AdaptSymptomController: BaseController
    {
        HTNResp.BLL.AdaptSymptom bll = new HTNResp.BLL.AdaptSymptom();
        HTNResp.BLL.Program bllProgram = new BLL.Program();
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
            string strSql = "Status=1";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(Request.Params["DecisionRule"]))
            {
                sb.Append(" and DecisionRule like '%" + Request.Params["DecisionRule"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["Contents"]))
            {
                sb.Append(" and Contents like '%" + Request.Params["Contents"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["Sex"]))
            {
                sb.Append(" and Sex='" + Request.Params["Sex"] + "' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["EvalGuidId"]))
            {
                sb.Append(" and EvalGuidId='" + Request.Params["EvalGuidId"] + "'");
            }

            strSql += sb.ToString();

            List<HTNResp.Model.AdaptSymptom> list = bll.DataTableToList(bll.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int filterTotal = bll.GetRecordCount(strSql);
            int total = bll.GetRecordCount("Status=1");

            BLL.EvalGuid bllEvalGrid = new BLL.EvalGuid();
            List<Model.EvalGuid> evalGridList = bllEvalGrid.GetModelList("");

            Dictionary<int, string> evalDic = new Dictionary<int, string>();
            foreach (Model.EvalGuid evalGrid in evalGridList)
            {
                evalDic.Add(evalGrid.ID, evalGrid.GuidName);

            }
            return this.Json(new
            {
                result = 1,
                data = new
                {
                    draw = Request.Params["draw"],
                    recordsTotal = total,
                    recordsFiltered = filterTotal,
                    data = list.Select(o => new
                    {
                        ID = o.ID,
                        DecisionRule = o.DecisionRule,
                        Contents = o.Contents,
                        EvalGuidId = evalDic[o.EvalGuidId.Value]
                    })
                }
            });
        }
        [Login(Feedback = 100)]
        public ActionResult Create(Model.AdaptSymptom model)
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
            bool result = false;
            Model.AdaptSymptom model = bll.GetModel(id);
            if (model != null)
                result = true;
            if (result) 
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "请求条目不存在" });
        }
        [Login(Feedback = 100)]
        public ActionResult Update(Model.AdaptSymptom model)
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
            bool result = false;
            Model.AdaptSymptom model = bll.GetModel(id);
            if (model != null)
            {
                model.Status = null;
                if (bll.Update(model)) result = true;
            }
            if(result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "没有这条数据" });
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
