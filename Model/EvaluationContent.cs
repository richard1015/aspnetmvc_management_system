using System;
namespace HTNResp.Model
{
	/// <summary>
	/// EvaluationContent:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EvaluationContent
	{
		public EvaluationContent()
		{}
		#region Model
		private int _id;
		private string _decisionrule;
		private string _contents;
		private string _sex;
		private int? _status;
		private int? _sort;
		private string _separator;
		private int? _typename;
		private string _remark;
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
		/// 评估内容
		/// </summary>
		public string Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
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
		/// 显示顺序
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 分隔符
		/// </summary>
		public string Separator
		{
			set{ _separator=value;}
			get{return _separator;}
		}
		/// <summary>
		/// 类别
		/// </summary>
		public int? TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 说明
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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

