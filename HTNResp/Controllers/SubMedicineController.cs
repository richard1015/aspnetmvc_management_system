using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTNResp.Codes;

namespace HTNResp.Controllers
{
    public class SubMedicineController : BaseController
    {
        //
        // GET: /SubMedicine/
        BLL.MedicineProject bllMedicine = new BLL.MedicineProject();
        BLL.SubMedicineProject bllSubMedicine = new BLL.SubMedicineProject();
        public override ActionResult Index()
        {
            return View();
        }

        //获取方案分页
        public override ActionResult ProjectDetail()
        {
            int start = 0;  // 分页起始条目数
            int length = 0; // 分页每页分页数

            string sqlString = "";
            string level = Request.Params["level"];
            string category = Request.Params["category"];
            string search = Request.Params["search"];

            sqlString += String.IsNullOrEmpty(level) ? "1 = 1" : ("BPLevel='" + level + "'");
            sqlString += " AND ";
            sqlString += String.IsNullOrEmpty(category) ? "1 = 1" : ("ProjectCode=" + category + "");
            sqlString += " AND ";
            sqlString += String.IsNullOrEmpty(search) ? "1 = 1" : ("ProjectContent Like ('%" + search + "%')");

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            List<HTNResp.Model.MedicineProject> list = bllMedicine.DataTableToList(bllMedicine.GetListByPage(sqlString, " ", start + 1, start + length).Tables[0]);

            int filterTotal = bllMedicine.GetRecordCount(sqlString);
            int total = bllMedicine.GetRecordCount("");

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
                        BPLevel = o.BPLevel,
                        ProjectContent = o.ProjectContent,
                        ProjectCode = "类型" + o.ProjectCode,
                        EvalGuidId = evalDic[o.EvalGuidId.Value]
                    })
                }
            });
        }

        //获取明细分页
        public override ActionResult UsageDetail()
        {
            int start = 0;  // 分页起始条目数
            int length = 0; // 分页每页分页数

            string sqlString = "";

            string id = Request.Params["id"];
            sqlString += String.IsNullOrEmpty(id) ? "1 = 1" : ("ProjectId=" + id );

            Session["pid"] = id;

            start = Convert.ToInt32(Request.Params["start"]);
            length = Convert.ToInt32(Request.Params["length"]);

            List<HTNResp.Model.SubMedicineProject> list = bllSubMedicine.DataTableToList(bllSubMedicine.GetListByPage(sqlString, " ", start + 1, start + length).Tables[0]);

            int filterTotal = bllSubMedicine.GetRecordCount(sqlString);
            int total = bllSubMedicine.GetRecordCount("");

            return this.Json(new
            {
                result = 1,
                data = new
                {
                    draw = Request.Params["draw"],
                    recordsTotal = total,
                    recordsFiltered = filterTotal,
                    data = list
                }
            });
        }

        //删除方案
        [Login(Feedback = 100)]
        public ActionResult ProjectDelete(int id)
        {
            bool r = bllMedicine.Delete(id);
            string sqlString = " ProjectId=" + id;
            List<Model.SubMedicineProject> subMedicineList = bllSubMedicine.GetModelList(sqlString);

            foreach (Model.SubMedicineProject t in subMedicineList)
                bllSubMedicine.Delete(t.ID);

            return this.Json(new { result = r?1:0, data = "" });
        }

        //删除明细
        [Login(Feedback = 100)]
        public ActionResult DetailDelete(int id)
        {
            Model.SubMedicineProject temp = bllSubMedicine.GetModel(id);
            bool r = bllSubMedicine.Delete(id);

            string pId = temp.ProjectId.Value.ToString();
            string sqlString = " ProjectId=" + pId;
            
            
            List<Model.SubMedicineProject> subMedicineList = bllSubMedicine.GetModelList(sqlString);

            string content = "";
            foreach (Model.SubMedicineProject t in subMedicineList)
                content += t.CommonName + t.Dose + "," + t.Usage + " ";
            content = content.Trim().Replace(' ', '+');
            //get project
            Model.MedicineProject usingProjectModel = bllMedicine.GetModel(Convert.ToInt32(pId));
            usingProjectModel.ProjectContent = content;
            bllMedicine.Update(usingProjectModel);

            return this.Json(new { result = r?1:0, data = "" });
        }

        //修改方案
        [Login(Feedback = 100)]
        public ActionResult ProjectEdit(int id, int status, Model.MedicineProject model)
        {
            if (status == 1)
            {
                Model.MedicineProject m = bllMedicine.GetModel(id);
                if (m == null) return this.Json(new { result = 0, data = "" });
                else return this.Json(new { result = 1, data = m }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool r = bllMedicine.Update(model);

                return this.Json(new { result = r?1:0, data = "" });
            }
        }
        
        //修改细节
        [Login(Feedback = 100)]
        public ActionResult DetailEdit(int id, int status, Model.SubMedicineProject model)
        {
            int i = id;
            if (status == 1)
            {
                Model.SubMedicineProject m = bllSubMedicine.GetModel(id);
                if (m == null) return this.Json(new { result = 0, data = "" });
                else return this.Json(new { result = 1, data = m }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool r = bllSubMedicine.Update(model);
                int projectId;
                if(model.ProjectId.HasValue){
                    projectId = model.ProjectId.Value;
                    string sqlString = " ProjectId=" + projectId;
                    //select * from SubMedicineProject where ProjectId=projectId;
                    //get detail
                    List<Model.SubMedicineProject> subMedicineList = bllSubMedicine.GetModelList(sqlString); 
                    
                    string content = "";
                    foreach (Model.SubMedicineProject t in subMedicineList)
                        content += t.CommonName + t.Dose + "," + t.Usage + " ";
                    content = content.Trim().Replace(' ', '+');
                    //get project
                    Model.MedicineProject usingProjectModel = bllMedicine.GetModel(projectId);
                    usingProjectModel.ProjectContent = content;
                    bllMedicine.Update(usingProjectModel);
                }

                return this.Json(new { result = r?1:0, data = "" });
            }
        }


        //新建方案
        [Login(Feedback = 100)]
        public ActionResult ProjectCreate(Model.MedicineProject model)
        {
            bool result = false;
            if (model.ProjectContent == null) model.ProjectContent = "暂未填写";
            if (new BLL.EvalGuid().Exists(model.EvalGuidId.Value))
            {
                bllMedicine.Add(model);
                result = true;
            }
            
                return this.Json(new { result = result?1:0, data = "" });
        }

        //新建明细
        [Login(Feedback = 100)]
        public ActionResult DetailCreate(Model.SubMedicineProject model)
        {

            string pId = (string)Session["pid"];
            int pid = Convert.ToInt32(pId);
            model.ProjectId = pid;
            bllSubMedicine.Add(model);
            string sqlString = " ProjectId=" + pid;
            //select * from SubMedicineProject where ProjectId=projectId;
            //get detail
            List<Model.SubMedicineProject> subMedicineList = bllSubMedicine.GetModelList(sqlString);

            string content = "";
            foreach (Model.SubMedicineProject t in subMedicineList)
                content += t.CommonName + t.Dose + "," + t.Usage + " ";
            content = content.Trim().Replace(' ','+');

            //get project
            Model.MedicineProject usingProjectModel = bllMedicine.GetModel(pid);
            usingProjectModel.ProjectContent = content;
            bllMedicine.Update(usingProjectModel);
            return this.Json(new { result = 1, data = "" });
        }
    }
}
