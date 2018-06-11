using HTNResp.Codes;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTNResp.Controllers
{
    [Login, Authority]
    public class AuthListController : BaseController
    {
        HTNResp.BLL.AccessTable bll = new HTNResp.BLL.AccessTable();
        HTNResp.BLL.Program bllProgram = new BLL.Program();
        HTNResp.BLL.ProjectDictionary bllProject = new BLL.ProjectDictionary();
        public override ActionResult Index()
        {

           //string strSql = "select * from AccessTable";
           // List<Model.AccessTable> tableList = new BLL.AccessTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);


           // BLL.EvalGuid blLEvalGrid = new BLL.EvalGuid();
           // List<Model.EvalGuid> evalGridId = blLEvalGrid.GetModelList("");

           // ViewBag.EvalGridId = evalGridId;
            return View();
        }

        public override ActionResult AjaxList()
        {
            int start = 0;  // 分页起始条目数
            int length = 0; // 分页每页分页数

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            // todo: 读取数据库 获取分页数据
            //string strSql = "Status=1";

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //if (!string.IsNullOrEmpty(Request.Params["DecisionRule"]))
            //{
            //    sb.Append(" and DecisionRule like '%" + Request.Params["DecisionRule"] + "%' ");
            //}
            //if (!string.IsNullOrEmpty(Request.Params["Contents"]))
            //{
            //    sb.Append(" and Contents like '%" + Request.Params["Contents"] + "%' ");
            //}
            //if (!string.IsNullOrEmpty(Request.Params["Sex"]))
            //{
            //    sb.Append(" and Sex='" + Request.Params["Sex"] + "' ");
            //}
            //if (!string.IsNullOrEmpty(Request.Params["EvalGuidId"]))
            //{
            //    sb.Append(" and EvalGuidId='" + Request.Params["EvalGuidId"] + "'");
            //}

            //strSql += sb.ToString();
            var strSql = "";

            List<HTNResp.Model.AccessTable> list = bll.DataTableToList(bll.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int filterTotal = bll.GetRecordCount(strSql);
            int total = bll.GetRecordCount("");

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
                        TableName = o.TableName,
                        TableAdress = o.TableAdress
                    })
                }
            });
        }
        [Login(Feedback = 100)]
        public ActionResult Create(Model.AccessTable model)
        {
            bool result = false;
            //model.ID = 1;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.TableName) && String.IsNullOrEmpty(bllProgram.testContent(model.TableName)) && model.TableAdress != null )
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
            Model.AccessTable model = bll.GetModel(id);
            if (model != null)
                result = true;
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "请求条目不存在" });
        }
        [Login(Feedback = 100)]
        public ActionResult Update(Model.AccessTable model)
        {
            bool result = false;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.TableName) && String.IsNullOrEmpty(bllProgram.testContent(model.TableAdress))  && String.IsNullOrEmpty(bllProgram.testContent(model.TableName)))
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
            Model.AccessTable model = bll.GetModel(id);
            if (model != null)
            {
                //model.Status = null;
                if (bll.Update(model)) result = true;
            }
            if (result)
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
