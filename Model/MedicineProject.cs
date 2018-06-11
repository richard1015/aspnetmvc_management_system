using System;
namespace HTNResp.Model
{
	/// <summary>
	/// MedicineProject:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MedicineProject
	{
		public MedicineProject()
		{}
		#region Model
		private int _id;
		private string _bplevel;
		private string _projectcontent;
		private int? _projectcode;
		private int? _evalguidid;
		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 血压级别
		/// </summary>
		public string BPLevel
		{
			set{ _bplevel=value;}
			get{return _bplevel;}
		}
		/// <summary>
		/// 药物方案
		/// </summary>
		public string ProjectContent
		{
			set{ _projectcontent=value;}
			get{return _projectcontent;}
		}
		/// <summary>
		/// 方案编号
		/// </summary>
		public int? ProjectCode
		{
			set{ _projectcode=value;}
			get{return _projectcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EvalGuidId
		{
			set{ _evalguidid=value;}
			get{return _evalguidid;}
		}
		#endregion Model

	}
}

