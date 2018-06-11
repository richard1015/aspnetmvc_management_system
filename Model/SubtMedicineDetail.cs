using System;
namespace HTNResp.Model
{
    /// <summary>
    /// SubtMedicineDetail:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
	public partial class SubtMedicineDetail
    {
		public SubtMedicineDetail()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _filepath;
		private string _imgpaht;
		/// <summary>
		/// 说明
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string FilePath
		{
			set{ _filepath=value;}
			get{return _filepath; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImgPath
		{
			set{ _imgpaht=value;}
			get{return _imgpaht; }
		}
		#endregion Model

	}
}

