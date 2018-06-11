using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace HTNResp.DAL
{
    /// <summary>
    /// 数据访问类:ProjectDictionary
    /// </summary>
    public partial class ProjectDictionary
    {
        public ProjectDictionary()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProjectDictionary");
            strSql.Append(" where ID=" + ID + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(HTNResp.Model.ProjectDictionary model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ProjectName != null)
            {
                strSql1.Append("ProjectName,");
                strSql2.Append("'" + model.ProjectName + "',");
            }
            if (model.ProjectCode != null)
            {
                strSql1.Append("ProjectCode,");
                strSql2.Append("'" + model.ProjectCode + "',");
            }
            if (model.ProjectType != null)
            {
                strSql1.Append("ProjectType,");
                strSql2.Append("'" + model.ProjectType + "',");
            }
            strSql.Append("insert into ProjectDictionary(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
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
        /// 更新一条数据
        /// </summary>
        public bool Update(HTNResp.Model.ProjectDictionary model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProjectDictionary set ");
            if (model.ProjectName != null)
            {
                strSql.Append("ProjectName='" + model.ProjectName + "',");
            }
            else
            {
                strSql.Append("ProjectName= null ,");
            }
            if (model.ProjectCode != null)
            {
                strSql.Append("ProjectCode='" + model.ProjectCode + "',");
            }
            else
            {
                strSql.Append("ProjectCode= null ,");
            }
            if (model.ProjectType != null)
            {
                strSql.Append("ProjectType='" + model.ProjectType + "',");
            }
            else
            {
                strSql.Append("ProjectType= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + "");
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
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProjectDictionary ");
            strSql.Append(" where ID=" + ID + "");
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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProjectDictionary ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public HTNResp.Model.ProjectDictionary GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" ID,ProjectName,ProjectCode,ProjectType ");
            strSql.Append(" from ProjectDictionary ");
            strSql.Append(" where ID=" + ID + "");
            HTNResp.Model.ProjectDictionary model = new HTNResp.Model.ProjectDictionary();
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
        public HTNResp.Model.ProjectDictionary DataRowToModel(DataRow row)
        {
            HTNResp.Model.ProjectDictionary model = new HTNResp.Model.ProjectDictionary();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ProjectName"] != null)
                {
                    model.ProjectName = row["ProjectName"].ToString();
                }
                if (row["ProjectCode"] != null)
                {
                    model.ProjectCode = row["ProjectCode"].ToString();
                }
                if (row["ProjectType"] != null)
                {
                    model.ProjectType = row["ProjectType"].ToString();
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
            strSql.Append("select ID,ProjectName,ProjectCode,ProjectType ");
            strSql.Append(" FROM ProjectDictionary ");
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
            strSql.Append(" ID,ProjectName,ProjectCode,ProjectType ");
            strSql.Append(" FROM ProjectDictionary ");
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
            strSql.Append("select count(1) FROM ProjectDictionary ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from ProjectDictionary T ");
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

