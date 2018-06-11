using HTNResp.Codes;
using HTNResp.Common;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HTNResp.Controllers
{
    [Login, Authority]
    public class UserListController : BaseController
    {
        HTNResp.BLL.UserTable bll = new HTNResp.BLL.UserTable();
        HTNResp.BLL.Program bllProgram = new BLL.Program();
        HTNResp.BLL.ProjectDictionary bllProject = new BLL.ProjectDictionary();
        public override ActionResult Index()
        {
            DataTable dt = DbHelperSQL.Query("select * from AccessTable a join PermissionTable p on (p.TableID=a.ID)").Tables[0];
            string authAll = ModelConvertHelper.ConvertToModel(dt);

            BLL.AccessTable bllaccess = new BLL.AccessTable();
            List<Model.AccessTable> AccessList = bllaccess.GetModelList("");


            ViewBag.AccessList = AccessList;
            ViewBag.EvalGridUserAuth = authAll;
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

            List<HTNResp.Model.UserTable> list = bll.DataTableToList(bll.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
            int filterTotal = bll.GetRecordCount(strSql);
            int total = bll.GetRecordCount("");

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
                        UserName = o.UserName,
                        UserCode = o.UserCode,
                        Password = o.Password,

                    })
                }
            });
        }
        [Login(Feedback = 100)]
        public ActionResult Create(Model.UserTable model, string authIdsa)
        {
            bool result = false;
            //model.ID = 1;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.UserName) && !String.IsNullOrEmpty(model.UserCode) && !String.IsNullOrEmpty(model.Password))
            {
                HTNResp.BLL.UserTable bll = new HTNResp.BLL.UserTable();
                HTNResp.BLL.PermissionTable per = new BLL.PermissionTable();
                var state = bll.Add(model);
                var list = authIdsa.Split(',');
                if (state != 0)
                {
                    if (string.IsNullOrEmpty(authIdsa))
                    {
                        list = new List<string>().ToArray();
                    }
                    foreach (var item in list)
                    {
                        if (per.Add(new Model.PermissionTable()
                        {
                            TableID = int.Parse(item),
                            UserID = state
                        }) != 0)
                        {
                            continue;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                    result = true;
                }

            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "新建失败" });
        }
        public override ActionResult Info(int id)
        {
            bool result = false;
            Model.UserTable model = bll.GetModel(id);
            if (model != null)
                result = true;
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "请求条目不存在" });
        }
        [Login(Feedback = 100)]
        public ActionResult Update(Model.UserTable model, string authIdsa)
        {
            bool result = false;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.PermissionTable per = new BLL.PermissionTable();
            if (!String.IsNullOrEmpty(model.UserName) && !String.IsNullOrEmpty(model.UserCode) && !String.IsNullOrEmpty(model.Password))
            {
                result = bll.Update(model);
                var list = authIdsa.Split(',');
                if (result)
                {
                    if (string.IsNullOrEmpty(authIdsa))
                    {
                        list = new List<string>().ToArray();
                    }
                    else
                    {
                        int temp = DbHelperSQL.ExecuteSql("delete from PermissionTable where UserID=" + model.ID);
                    }
                    foreach (var item in list)
                    {
                        if (per.Add(new Model.PermissionTable()
                        {
                            TableID = int.Parse(item),
                            UserID = model.ID
                        }) != 0)
                        {
                            continue;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                    result = true;
                }
            }
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "修改失败" });

        }
        public override ActionResult Delete(int id)
        {
            if (bll.Delete(id))
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
