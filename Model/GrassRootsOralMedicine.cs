using System;
namespace HTNResp.Model
{
	/// <summary>
	/// GrassRootsOralMedicine:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GrassRootsOralMedicine
	{
		public GrassRootsOralMedicine()
		{}
		#region Model
		private int _id;
		private string _commonname;
		private string _dose;
		private string _usage;
		private string _indications;
		private string _contraindications;
		private string _adversereaction;
		private string _classname;
		private string _classcode;
		private string _remark;
		private string _effectcategory;
		private string _classnamedetail;
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
		/// 适应症
		/// </summary>
		public string Indications
		{
			set{ _indications=value;}
			get{return _indications;}
		}
		/// <summary>
		/// 禁忌症
		/// </summary>
		public string Contraindications
		{
			set{ _contraindications=value;}
			get{return _contraindications;}
		}
		/// <summary>
		/// 主要不良反应
		/// </summary>
		public string AdverseReaction
		{
			set{ _adversereaction=value;}
			get{return _adversereaction;}
		}
		/// <summary>
		/// 类名
		/// </summary>
		public string ClassName
		{
			set{ _classname=value;}
			get{return _classname;}
		}
		/// <summary>
		/// 类别
		/// </summary>
		public string ClassCode
		{
			set{ _classcode=value;}
			get{return _classcode;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 效果类别
		/// </summary>
		public string EffectCategory
		{
			set{ _effectcategory=value;}
			get{return _effectcategory;}
		}
		/// <summary>
		/// 详细类名
		/// </summary>
		public string ClassNameDetail
		{
			set{ _classnamedetail=value;}
			get{return _classnamedetail;}
		}
		#endregion Model

	}
}

