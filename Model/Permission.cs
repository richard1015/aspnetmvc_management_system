using System;
namespace HTNResp.Model
{
    /// <summary>
    /// Permission:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Permission
    {
        public Permission()
        { }
        #region Model
        private string _tablename;
        private int? _permissionlevel;
        private string _tableroute;
        /// <summary>
        /// 
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PermissionLevel
        {
            set { _permissionlevel = value; }
            get { return _permissionlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TableRoute
        {
            set { _tableroute = value; }
            get { return _tableroute; }
        }
        #endregion Model

    }
}

