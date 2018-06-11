using System;
namespace HTNResp.Model
{
	/// <summary>
	/// AdaptMedicineUse:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AdaptMedicineUse
	{
		public AdaptMedicineUse()
		{}
		#region Model
		private int _id;
		private string _groupname;
		private string _groupnamedetail;
		private string _symptom;
		private string _symptomtype;
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
		/// 类名
		/// </summary>
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 详细类名
		/// </summary>
		public string GroupNameDetail
		{
			set{ _groupnamedetail=value;}
			get{return _groupnamedetail;}
		}
		/// <summary>
		/// 症状
		/// </summary>
		public string Symptom
		{
			set{ _symptom=value;}
			get{return _symptom;}
		}
		/// <summary>
		/// 症状类别
		/// </summary>
		public string SymptomType
		{
			set{ _symptomtype=value;}
			get{return _symptomtype;}
		}
		/// <summary>
		/// 评估指南ID
		/// </summary>
		public int? EvalGuidId
		{
			set{ _evalguidid=value;}
			get{return _evalguidid;}
		}
		#endregion Model

	}
}

