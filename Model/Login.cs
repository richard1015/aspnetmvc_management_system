using System;
namespace HTNResp.Model
{
    /// <summary>
    /// Login:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Login
    {
        public Login()
        { }
        #region Model
        private string _username;
        private string _name;
        private string _password;
        private string _permissionlevel;
        /// <summary>
        /// 
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PermissionLevel
        {
            set { _permissionlevel = value; }
            get { return _permissionlevel; }
        }
        #endregion Model

    }
}

