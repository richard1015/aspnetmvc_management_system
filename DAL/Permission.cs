using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace HTNResp.DAL
{
    /// <summary>
    /// 数据访问类:Permission
    /// </summary>
    public partial class Permission
    {
        public Permission()
        { }
        #region  Method


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Permission");
            strSql.Append(" where TableName='" + TableName + "' ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(HTNResp.Model.Permission model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.TableName != null)
            {
                strSql1.Append("TableName,");
                strSql2.Append("'" + model.TableName + "',");
            }
            if (model.PermissionLevel != null)
            {
                strSql1.Append("PermissionLevel,");
                strSql2.Append("" + model.PermissionLevel + ",");
            }
            if (model.TableRoute != null)
            {
                strSql1.Append("TableRoute,");
                strSql2.Append("'" + model.TableRoute + "',");
            }
            strSql.Append("insert into Permission(");
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
        public bool Update(HTNResp.Model.Permission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Permission set ");
            if (model.PermissionLevel != null)
            {
                strSql.Append("PermissionLevel=" + model.PermissionLevel + ",");
            }
            else
            {
                strSql.Append("PermissionLevel= null ,");
            }
            if (model.TableRoute != null)
            {
                strSql.Append("TableRoute='" + model.TableRoute + "',");
            }
            else
            {
                strSql.Append("TableRoute= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where TableName='" + model.TableName + "' ");
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
        public bool Delete(string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Permission ");
            strSql.Append(" where TableName='" + TableName + "' ");
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
        public bool DeleteList(string TableNamelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Permission ");
            strSql.Append(" where TableName in (" + TableNamelist + ")  ");
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
        public HTNResp.Model.Permission GetModel(string TableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" TableName,PermissionLevel,TableRoute ");
            strSql.Append(" from Permission ");
            strSql.Append(" where TableName='" + TableName + "' ");
            HTNResp.Model.Permission model = new HTNResp.Model.Permission();
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
        public HTNResp.Model.Permission DataRowToModel(DataRow row)
        {
            HTNResp.Model.Permission model = new HTNResp.Model.Permission();
            if (row != null)
            {
                if (row["TableName"] != null)
                {
                    model.TableName = row["TableName"].ToString();
                }
                if (row["PermissionLevel"] != null && row["PermissionLevel"].ToString() != "")
                {
                    model.PermissionLevel = int.Parse(row["PermissionLevel"].ToString());
                }
                if (row["TableRoute"] != null)
                {
                    model.TableRoute = row["TableRoute"].ToString();
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
            strSql.Append("select TableName,PermissionLevel,TableRoute ");
            strSql.Append(" FROM Permission ");
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
            strSql.Append(" TableName,PermissionLevel,TableRoute ");
            strSql.Append(" FROM Permission ");
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
            strSql.Append("select count(1) FROM Permission ");
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
                strSql.Append("order by T.TableName desc");
            }
            strSql.Append(")AS Row, T.*  from Permission T ");
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

