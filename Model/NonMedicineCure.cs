using System;
namespace HTNResp.Model
{
	/// <summary>
	/// NonMedicineCure:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class NonMedicineCure
	{
		public NonMedicineCure()
		{}
		#region Model
		private int _id;
		private string _decisionrule;
		private string _contents;
		private string _aim;
		private string _measure;
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
		/// 内容
		/// </summary>
		public string Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// 目标
		/// </summary>
		public string Aim
		{
			set{ _aim=value;}
			get{return _aim;}
		}
		/// <summary>
		/// 措施
		/// </summary>
		public string Measure
		{
			set{ _measure=value;}
			get{return _measure;}
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

