using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace HTNResp.DAL
{
    /// <summary>
    /// 数据访问类:Login
    /// </summary>
    public partial class Login
    {
        public Login()
        { }
        #region  Method


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Login");
            strSql.Append(" where Username='" + Username + "' ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(HTNResp.Model.Login model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Username != null)
            {
                strSql1.Append("Username,");
                strSql2.Append("'" + model.Username + "',");
            }
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.Password != null)
            {
                strSql1.Append("Password,");
                strSql2.Append("'" + model.Password + "',");
            }
            if (model.PermissionLevel != null)
            {
                strSql1.Append("PermissionLevel,");
                strSql2.Append("'" + model.PermissionLevel + "',");
            }
            strSql.Append("insert into Login(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HTNResp.Model.Login model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Login set ");
            if (model.Name != null)
            {
                strSql.Append("Name='" + model.Name + "',");
            }
            else
            {
                strSql.Append("Name= null ,");
            }
            if (model.Password != null)
            {
                strSql.Append("Password='" + model.Password + "',");
            }
            else
            {
                strSql.Append("Password= null ,");
            }
            if (model.PermissionLevel != null)
            {
                strSql.Append("PermissionLevel='" + model.PermissionLevel + "',");
            }
            else
            {
                strSql.Append("PermissionLevel= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Username='" + model.Username + "' ");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Login ");
            strSql.Append(" where Username='" + Username + "' ");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }		/// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Usernamelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Login ");
            strSql.Append(" where Username in (" + Usernamelist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HTNResp.Model.Login GetModel(string Username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" Username,Name,Password,PermissionLevel ");
            strSql.Append(" from Login ");
            strSql.Append(" where Username='" + Username + "' ");
            HTNResp.Model.Login model = new HTNResp.Model.Login();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HTNResp.Model.Login DataRowToModel(DataRow row)
        {
            HTNResp.Model.Login model = new HTNResp.Model.Login();
            if (row != null)
            {
                if (row["Username"] != null)
                {
                    model.Username = row["Username"].ToString();
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Password"] != null)
                {
                    model.Password = row["Password"].ToString();
                }
                if (row["PermissionLevel"] != null)
                {
                    model.PermissionLevel = row["PermissionLevel"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Username,Name,Password,PermissionLevel ");
            strSql.Append(" FROM Login ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Username,Name,Password,PermissionLevel ");
            strSql.Append(" FROM Login ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Login ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Username desc");
            }
            strSql.Append(")AS Row, T.*  from Login T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        */

        #endregion  Method
        #region  MethodEx

        #endregion  MethodEx
    }
}

