using System;
namespace HTNResp.Model
{
	/// <summary>
	/// SubMedicineProject:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SubMedicineProject
	{
		public SubMedicineProject()
		{}
		#region Model
		private int _id;
		private string _commonname;
		private string _dose;
		private string _usage;
		private int? _projectid;
		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 通用名
		/// </summary>
		public string CommonName
		{
			set{ _commonname=value;}
			get{return _commonname;}
		}
		/// <summary>
		/// 每次剂量
		/// </summary>
		public string Dose
		{
			set{ _dose=value;}
			get{return _dose;}
		}
		/// <summary>
		/// 每日次
		/// </summary>
		public string Usage
		{
			set{ _usage=value;}
			get{return _usage;}
		}
		/// <summary>
		/// 药物方案编号
		/// </summary>
		public int? ProjectId
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		#endregion Model

	}
}

