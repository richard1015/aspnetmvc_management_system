using System;
namespace HTNResp.Model
{
	/// <summary>
	/// DiagnoseList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DiagnoseList
	{
		public DiagnoseList()
		{}
		#region Model
		private int _id;
		private string _decisionrule;
		private string _name;
		private string _sex;
		private string _description;
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
		/// 名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
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
		/// 评估指南
		/// </summary>
		public int? EvalGuidId
		{
			set{ _evalguidid=value;}
			get{return _evalguidid;}
		}
		#endregion Model

	}
}

