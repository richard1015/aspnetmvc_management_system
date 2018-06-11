using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace HTNResp.DAL
{
	/// <summary>
	/// 数据访问类:GrassRootsOralMedicine
	/// </summary>
	public partial class GrassRootsOralMedicine
	{
		public GrassRootsOralMedicine()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "GrassRootsOralMedicine"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from GrassRootsOralMedicine");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(HTNResp.Model.GrassRootsOralMedicine model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GrassRootsOralMedicine(");
			strSql.Append("CommonName,Dose,Usage,Indications,Contraindications,AdverseReaction,ClassName,ClassCode,Remark,EffectCategory,ClassNameDetail)");
			strSql.Append(" values (");
			strSql.Append("@CommonName,@Dose,@Usage,@Indications,@Contraindications,@AdverseReaction,@ClassName,@ClassCode,@Remark,@EffectCategory,@ClassNameDetail)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CommonName", SqlDbType.NVarChar,20),
					new SqlParameter("@Dose", SqlDbType.NVarChar,15),
					new SqlParameter("@Usage", SqlDbType.NVarChar,4),
					new SqlParameter("@Indications", SqlDbType.NVarChar,255),
					new SqlParameter("@Contraindications", SqlDbType.NVarChar,255),
					new SqlParameter("@AdverseReaction", SqlDbType.NVarChar,255),
					new SqlParameter("@ClassName", SqlDbType.NVarChar,20),
					new SqlParameter("@ClassCode", SqlDbType.NVarChar,10),
					new SqlParameter("@Remark", SqlDbType.NVarChar,255),
					new SqlParameter("@EffectCategory", SqlDbType.NVarChar,255),
					new SqlParameter("@ClassNameDetail", SqlDbType.NVarChar,255)};
			parameters[0].Value = model.CommonName;
			parameters[1].Value = model.Dose;
			parameters[2].Value = model.Usage;
			parameters[3].Value = model.Indications;
			parameters[4].Value = model.Contraindications;
			parameters[5].Value = model.AdverseReaction;
			parameters[6].Value = model.ClassName;
			parameters[7].Value = model.ClassCode;
			parameters[8].Value = model.Remark;
			parameters[9].Value = model.EffectCategory;
			parameters[10].Value = model.ClassNameDetail;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(HTNResp.Model.GrassRootsOralMedicine model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GrassRootsOralMedicine set ");
			strSql.Append("CommonName=@CommonName,");
			strSql.Append("Dose=@Dose,");
			strSql.Append("Usage=@Usage,");
			strSql.Append("Indications=@Indications,");
			strSql.Append("Contraindications=@Contraindications,");
			strSql.Append("AdverseReaction=@AdverseReaction,");
			strSql.Append("ClassName=@ClassName,");
			strSql.Append("ClassCode=@ClassCode,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("EffectCategory=@EffectCategory,");
			strSql.Append("ClassNameDetail=@ClassNameDetail");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CommonName", SqlDbType.NVarChar,20),
					new SqlParameter("@Dose", SqlDbType.NVarChar,15),
					new SqlParameter("@Usage", SqlDbType.NVarChar,4),
					new SqlParameter("@Indications", SqlDbType.NVarChar,255),
					new SqlParameter("@Contraindications", SqlDbType.NVarChar,255),
					new SqlParameter("@AdverseReaction", SqlDbType.NVarChar,255),
					new SqlParameter("@ClassName", SqlDbType.NVarChar,20),
					new SqlParameter("@ClassCode", SqlDbType.NVarChar,10),
					new SqlParameter("@Remark", SqlDbType.NVarChar,255),
					new SqlParameter("@EffectCategory", SqlDbType.NVarChar,255),
					new SqlParameter("@ClassNameDetail", SqlDbType.NVarChar,255),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.CommonName;
			parameters[1].Value = model.Dose;
			parameters[2].Value = model.Usage;
			parameters[3].Value = model.Indications;
			parameters[4].Value = model.Contraindications;
			parameters[5].Value = model.AdverseReaction;
			parameters[6].Value = model.ClassName;
			parameters[7].Value = model.ClassCode;
			parameters[8].Value = model.Remark;
			parameters[9].Value = model.EffectCategory;
			parameters[10].Value = model.ClassNameDetail;
			parameters[11].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GrassRootsOralMedicine ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GrassRootsOralMedicine ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public HTNResp.Model.GrassRootsOralMedicine GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CommonName,Dose,Usage,Indications,Contraindications,AdverseReaction,ClassName,ClassCode,Remark,EffectCategory,ClassNameDetail from GrassRootsOralMedicine ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			HTNResp.Model.GrassRootsOralMedicine model=new HTNResp.Model.GrassRootsOralMedicine();
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
		public HTNResp.Model.GrassRootsOralMedicine DataRowToModel(DataRow row)
		{
			HTNResp.Model.GrassRootsOralMedicine model=new HTNResp.Model.GrassRootsOralMedicine();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CommonName"]!=null)
				{
					model.CommonName=row["CommonName"].ToString();
				}
				if(row["Dose"]!=null)
				{
					model.Dose=row["Dose"].ToString();
				}
				if(row["Usage"]!=null)
				{
					model.Usage=row["Usage"].ToString();
				}
				if(row["Indications"]!=null)
				{
					model.Indications=row["Indications"].ToString();
				}
				if(row["Contraindications"]!=null)
				{
					model.Contraindications=row["Contraindications"].ToString();
				}
				if(row["AdverseReaction"]!=null)
				{
					model.AdverseReaction=row["AdverseReaction"].ToString();
				}
				if(row["ClassName"]!=null)
				{
					model.ClassName=row["ClassName"].ToString();
				}
				if(row["ClassCode"]!=null)
				{
					model.ClassCode=row["ClassCode"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["EffectCategory"]!=null)
				{
					model.EffectCategory=row["EffectCategory"].ToString();
				}
				if(row["ClassNameDetail"]!=null)
				{
					model.ClassNameDetail=row["ClassNameDetail"].ToString();
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
			strSql.Append("select ID,CommonName,Dose,Usage,Indications,Contraindications,AdverseReaction,ClassName,ClassCode,Remark,EffectCategory,ClassNameDetail ");
			strSql.Append(" FROM GrassRootsOralMedicine ");
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
			strSql.Append(" ID,CommonName,Dose,Usage,Indications,Contraindications,AdverseReaction,ClassName,ClassCode,Remark,EffectCategory,ClassNameDetail ");
			strSql.Append(" FROM GrassRootsOralMedicine ");
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
			strSql.Append("select count(1) FROM GrassRootsOralMedicine ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from GrassRootsOralMedicine T ");
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
			parameters[0].Value = "GrassRootsOralMedicine";
			parameters[1].Value = "ID";
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

