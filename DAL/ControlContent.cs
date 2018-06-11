using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace HTNResp.DAL
{
	/// <summary>
	/// 数据访问类:ControlContent
	/// </summary>
	public partial class ControlContent
	{
		public ControlContent()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ControlName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ControlContent");
			strSql.Append(" where ControlName=@ControlName ");
			SqlParameter[] parameters = {
					new SqlParameter("@ControlName", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ControlName;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HTNResp.Model.ControlContent model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ControlContent(");
			strSql.Append("ControlName,Title,Remark,ControlType)");
			strSql.Append(" values (");
			strSql.Append("@ControlName,@Title,@Remark,@ControlType)");
			SqlParameter[] parameters = {
					new SqlParameter("@ControlName", SqlDbType.NVarChar,50),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@ControlType", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ControlName;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Remark;
			parameters[3].Value = model.ControlType;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(HTNResp.Model.ControlContent model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ControlContent set ");
			strSql.Append("Title=@Title,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("ControlType=@ControlType");
			strSql.Append(" where ControlName=@ControlName ");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@ControlType", SqlDbType.NVarChar,50),
					new SqlParameter("@ControlName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Remark;
			parameters[2].Value = model.ControlType;
			parameters[3].Value = model.ControlName;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ControlName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ControlContent ");
			strSql.Append(" where ControlName=@ControlName ");
			SqlParameter[] parameters = {
					new SqlParameter("@ControlName", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ControlName;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string ControlNamelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ControlContent ");
			strSql.Append(" where ControlName in ("+ControlNamelist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public HTNResp.Model.ControlContent GetModel(string ControlName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ControlName,Title,Remark,ControlType from ControlContent ");
			strSql.Append(" where ControlName=@ControlName ");
			SqlParameter[] parameters = {
					new SqlParameter("@ControlName", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ControlName;

			HTNResp.Model.ControlContent model=new HTNResp.Model.ControlContent();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public HTNResp.Model.ControlContent DataRowToModel(DataRow row)
		{
			HTNResp.Model.ControlContent model=new HTNResp.Model.ControlContent();
			if (row != null)
			{
				if(row["ControlName"]!=null)
				{
					model.ControlName=row["ControlName"].ToString();
				}
				if(row["Title"]!=null)
				{
					model.Title=row["Title"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["ControlType"]!=null)
				{
					model.ControlType=row["ControlType"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ControlName,Title,Remark,ControlType ");
			strSql.Append(" FROM ControlContent ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ControlName,Title,Remark,ControlType ");
			strSql.Append(" FROM ControlContent ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM ControlContent ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ControlName desc");
			}
			strSql.Append(")AS Row, T.*  from ControlContent T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "ControlContent";
			parameters[1].Value = "ControlName";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

