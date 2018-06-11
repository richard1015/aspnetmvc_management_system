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
    public class AdaptMedicineUseController: BaseController
    {
        HTNResp.BLL.AdaptMedicineUse bll = new HTNResp.BLL.AdaptMedicineUse();

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

            string strSql = " 1=1 ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //strSql = "GroupName'" + GroupName + "' and GroupNameDetail'" + GroupNameDetail + "'";
            if (!string.IsNullOrEmpty(Request.Params["GroupName"]))
            {
                sb.Append(" and GroupName='" + Request.Params["GroupName"] + "' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["GroupNameDetail"]))
            {
                sb.Append(" and GroupNameDetail='" + Request.Params["GroupNameDetail"] + "' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["Symptom"]))
            {
                sb.Append(" and Symptom like '%" + Request.Params["Symptom"] + "%' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["SymptomType"]))
            {
                sb.Append(" and SymptomType='" + Request.Params["SymptomType"] + "' ");
            }
            if (!string.IsNullOrEmpty(Request.Params["EvalGuidId"]))
            {
                sb.Append(" and EvalGuidId='" + Request.Params["EvalGuidId"] + "'");
            }
            strSql += sb.ToString();

            List<HTNResp.Model.AdaptMedicineUse> list = bll.DataTableToList(bll.GetListByPage(strSql, " ", start + 1, start + length).Tables[0]);
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
                    data = list.Select(o => new
                    {
                        ID = o.ID,
                        GroupName = o.GroupName,
                        GroupNameDetail = o.GroupNameDetail,
                        Symptom = o.Symptom,
                        SymptomType = o.SymptomType,
                        EvalGuidId = o.EvalGuidId,
                        EvalGuid = bllEvalGrid.GetModel(o.EvalGuidId.Value).GuidName,
                    })

                }

            });
        }

        [Login(Feedback = 100)]
        public ActionResult Create(Model.AdaptMedicineUse model)
        {
            bool result = false;
            HTNResp.BLL.EvalGuid bllEvalGuid = new HTNResp.BLL.EvalGuid();
            if (model.EvalGuidId != null && bllEvalGuid.Exists(model.EvalGuidId.Value))
            {
                if (bll.Add(model) != 0)
                    result = true;
            }
            if (result)
                return this.Json(new { result = 1, data="" });
            else
                return this.Json(new { result = 0, msg = "新建失败" });
        }

        public override ActionResult Info(int id)
        {
            bool result = false;
            Model.AdaptMedicineUse model = bll.GetModel(id);
            if (model != null)
                result = true;
            if(result)
                return this.Json(new { result = result ? 1 : 0, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "新建失败" });
        }

        [Login(Feedback = 100)]
        public ActionResult Update(Model.AdaptMedicineUse model)
        {
            bool result = false;
            HTNResp.BLL.EvalGuid bllEvalGuid = new HTNResp.BLL.EvalGuid();
            if (model.EvalGuidId != null && bllEvalGuid.Exists(model.EvalGuidId.Value))
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
            result = bll.Delete(id);
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "删除失败" });
        }


    }
}
