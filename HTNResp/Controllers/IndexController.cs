using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTNResp.Codes;
using HTNResp.Common;
using Maticsoft.DBUtility;

namespace HTNResp.Controllers
{
    
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        [Login(Feedback=200)]
        public ActionResult Index()
        {
            DataTable dt = DbHelperSQL.Query("SELECT ( case when a.name = 'AccessTable' then '权限数据' "+

           " when a.name = 'AdaptMedicineUse' then '适应症药物使用' " +

            " when a.name = 'AdaptSymptom' then '适应症诊断' " +

           "  when a.name = 'ControlContent' then '输入框提示' " +

             " when a.name = 'DiagnoseList' then '诊断项列表' " +

           "  when a.name = 'EvalGuid' then '评估指南' " +

            " when a.name = 'EvaluationContent' then '评估项内容' " +

           "  when a.name = 'GrassRootsOralMedicine' then '基层口腔医学' " + 

           "  when a.name = 'MedicineProject' then '医药项目' " + 

           "  when a.name = 'NonMedicineCure' then '非药物临床治疗' " + 

           "  when a.name = 'PermissionTable' then '关联表' " + 

           "  when a.name = 'PrevensionProject' then '药物治疗' " + 

          "   when a.name = 'ProjectDictionary' then '评估字典项维护' " + 

          "   when a.name = 'SpecialPatientProcess' then '特殊人群高血压' " + 
 
          "   when a.name = 'SubMedicineProject' then '基层常见药物维护' " + 

            " when a.name = 'SubtMedicineDetail' then '基层高血压用药参考方案及明细' " + 

           "  when a.name = 'User' then '用户old(已废除)' " + 

           "  when a.name = 'UserTable' then '用户列表'  " +
             " else a.name end) as name, " +
     "  b.rows FROM sysobjects AS a INNER JOIN sysindexes AS b ON a.id = b.id WHERE(a.type = 'u') AND(b.indid IN(0, 1)) ORDER BY a.name, b.rows DESC").Tables[0];
            string bigData = ModelConvertHelper.ConvertToModel(dt);
            ViewBag.BigData = bigData;
            return View();
        }

    }
}
