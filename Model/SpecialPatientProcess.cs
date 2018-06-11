using System;
namespace HTNResp.Model
{
	/// <summary>
	/// SpecialPatientProcess:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SpecialPatientProcess
	{
		public SpecialPatientProcess()
		{}
		#region Model
		private int _id;
		private string _decisionrule;
		private string _symptomname;
		private string _contents;
		private int? _status;
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
		/// 判断规则
		/// </summary>
		public string DecisionRule
		{
			set{ _decisionrule=value;}
			get{return _decisionrule;}
		}
		/// <summary>
		/// 症状名称
		/// </summary>
		public string SymptomName
		{
			set{ _symptomname=value;}
			get{return _symptomname;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// 标记
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
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

