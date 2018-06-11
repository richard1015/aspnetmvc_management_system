using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using HTNResp.Models;
using HTNResp.Codes;

namespace HTNResp.Controllers
{
    public class EvalGuidController : BaseController
    {
        HTNResp.BLL.EvalGuid bll = new HTNResp.BLL.EvalGuid();
        HTNResp.DAL.EvalGuid dal = new HTNResp.DAL.EvalGuid();
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

            List<HTNResp.Model.EvalGuid> list = bll.DataTableToList(bll.GetListByPage("", "", start + 1, start + length).Tables[0]);
            int total = bll.GetRecordCount("");
            int recordsFiltered = bll.GetRecordCount("");
            return this.Json(new
            {
                result = 1,
                data = new
                {
                    draw = Request.Params["draw"],
                    recordsTotal = total,
                    recordsFiltered = total,
                    data = list
                }
            });
        }
        
        [Login(Feedback = 100)]
        public ActionResult Create(Model.EvalGuid model)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(model.GuidName))
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
        public ActionResult Update(Model.EvalGuid model)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(model.GuidName))
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
            if (result)
                return this.Json(new { result = 1, data = "" });
            else
                return this.Json(new { result = 0, msg = "删除失败" }); 
        }

        public override ActionResult Info(int id)
        {
            Model.EvalGuid model = bll.GetModel(id);
            bool result = model != null ? true : false;
            if (result)
                return this.Json(new { result = 1, data = model }, JsonRequestBehavior.AllowGet);
            else
                return this.Json(new { result = 0, msg = "查询失败" });
        }


    }
}
