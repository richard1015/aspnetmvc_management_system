using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HTNResp.Model;
namespace HTNResp.BLL
{
    /// <summary>
    /// Permission
    /// </summary>
    public partial class Permission
    {
        private readonly HTNResp.DAL.Permission dal = new HTNResp.DAL.Permission();
        public Permission()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string TableName)
        {
            return dal.Exists(TableName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(HTNResp.Model.Permission model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HTNResp.Model.Permission model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string TableName)
        {

            return dal.Delete(TableName);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string TableNamelist)
        {
            return dal.DeleteList(TableNamelist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HTNResp.Model.Permission GetModel(string TableName)
        {

            return dal.GetModel(TableName);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public HTNResp.Model.Permission GetModelByCache(string TableName)
        {

            string CacheKey = "PermissionModel-" + TableName;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(TableName);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (HTNResp.Model.Permission)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HTNResp.Model.Permission> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HTNResp.Model.Permission> DataTableToList(DataTable dt)
        {
            List<HTNResp.Model.Permission> modelList = new List<HTNResp.Model.Permission>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                HTNResp.Model.Permission model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

