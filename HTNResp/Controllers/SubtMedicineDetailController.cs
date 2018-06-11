using HTNResp.Codes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTNResp.Controllers
{
    [Login, Authority]
    public class SubtMedicineDetailController : BaseController
    {
        HTNResp.BLL.SubtMedicineDetail bll = new HTNResp.BLL.SubtMedicineDetail();
        HTNResp.BLL.Program bllProgram = new BLL.Program();
        HTNResp.BLL.ProjectDictionary bllProject = new BLL.ProjectDictionary();
        public override ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase file = Request.Files[0];
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "File"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return this.Json(new {
                url= "/File/"+
                fileName
            });
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

            List<HTNResp.Model.SubtMedicineDetail> list = bll.DataTableToList(bll.GetListByPage(strSql, "", start + 1, start + length).Tables[0]);
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
                        Name = o.Name,
                        FilePath = o.FilePath,
                        ImgPath = o.ImgPath,

                    })
                }
            });
        }
        [Login(Feedback = 100)]
        public ActionResult Create(Model.SubtMedicineDetail model)
        {
            bool result = false;
            //model.ID = 1;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.EvalGuid bllEvalGuid = new BLL.EvalGuid();
            if (!String.IsNullOrEmpty(model.Name))
            {
                HTNResp.BLL.SubtMedicineDetail bll = new HTNResp.BLL.SubtMedicineDetail();
                HTNResp.BLL.PermissionTable per = new BLL.PermissionTable();
                var state = bll.Add(model);

                if (state != 0)
                {
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
            Model.SubtMedicineDetail model = bll.GetModel(id);
            if (model != null)
                result = true;
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "请求条目不存在" });
        }
        [Login(Feedback = 100)]
        public ActionResult Update(Model.SubtMedicineDetail model)
        {
            bool result = false;
            HTNResp.BLL.Program bllProgram = new BLL.Program();
            HTNResp.BLL.PermissionTable per = new BLL.PermissionTable();
            if (!String.IsNullOrEmpty(model.Name))
            {
                result = bll.Update(model);
                if (result)
                {
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