using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Data.SqlClient;
using HTNResp.Models;
using Maticsoft.DBUtility;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using HTNResp.Codes;

namespace HTNResp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Session["status"] = Request.Params["status"];
            return View();
        }
        [Login(Feedback = -7)]
        public ActionResult Login(string Username, string Password)
        {
            // \ _ " " + 
            HTNResp.BLL.UserTable bllUser = new HTNResp.BLL.UserTable();
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return this.Json(new { result = 0, data = "" });
            }
            // Password = encryptPwd(Password);

            string strSql = "UserCode='" + Username + "' and Password='" + Password + "'";
            List<Model.UserTable> userList = bllUser.DataTableToList(bllUser.GetList(strSql).Tables[0]);

            //authority
            if (userList.Count > 0)
            {

                Model.UserTable userModel = userList[0];
                strSql = "select * from AccessTable where ID in (select TableID from PermissionTable where UserID = " + userModel.ID + ")";
                List<Model.AccessTable> tableList = new BLL.AccessTable().DataTableToList(DbHelperSQL.Query(strSql).Tables[0]);

                //get granted table
                string sessionString = "";
                if (tableList.Count != 0)
                {
                    sessionString += "[";
                    foreach (Model.AccessTable tableModel in tableList)
                    {
                        sessionString += new JavaScriptSerializer().Serialize(tableModel) + ",";
                    }
                    sessionString = sessionString.Remove(sessionString.Length - 1);
                    sessionString += "]";
                }
                //set session time out
                Session.Timeout = 30;
                Session["username"] = userModel.UserName;
                Session["access"] = sessionString;
                return this.Json(new { result = 1, data = "" });
            }
            else
            {
                return this.Json(new { result = 0, data = "" });
            }
        }

        public string encryptPwd(string pwd)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
        }

        public ActionResult Illegal()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            return RedirectToAction("Index", "Home");
            //return this.Json(new { result = 1, data = "" });
        }

    }
}