using System;
namespace HTNResp.Model
{
    /// <summary>
    /// ProjectDictionary:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProjectDictionary
    {
        public ProjectDictionary()
        { }
        #region Model
        private int _id;
        private string _projectname;
        private string _projectcode;
        private string _projecttype;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectType
        {
            set { _projecttype = value; }
            get { return _projecttype; }
        }
        #endregion Model

    }
}

