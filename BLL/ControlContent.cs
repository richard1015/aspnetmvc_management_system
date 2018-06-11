using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HTNResp.Model;
namespace HTNResp.BLL
{
	/// <summary>
	/// ControlContent
	/// </summary>
	public partial class ControlContent
	{
		private readonly HTNResp.DAL.ControlContent dal=new HTNResp.DAL.ControlContent();
		public ControlContent()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ControlName)
		{
			return dal.Exists(ControlName);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HTNResp.Model.ControlContent model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(HTNResp.Model.ControlContent model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ControlName)
		{
			
			return dal.Delete(ControlName);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ControlNamelist )
		{
			return dal.DeleteList(ControlNamelist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HTNResp.Model.ControlContent GetModel(string ControlName)
		{
			
			return dal.GetModel(ControlName);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public HTNResp.Model.ControlContent GetModelByCache(string ControlName)
		{
			
			string CacheKey = "ControlContentModel-" + ControlName;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ControlName);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HTNResp.Model.ControlContent)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HTNResp.Model.ControlContent> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HTNResp.Model.ControlContent> DataTableToList(DataTable dt)
		{
			List<HTNResp.Model.ControlContent> modelList = new List<HTNResp.Model.ControlContent>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HTNResp.Model.ControlContent model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

