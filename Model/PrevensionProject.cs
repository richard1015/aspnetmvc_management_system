using System;
namespace HTNResp.Model
{
	/// <summary>
	/// PrevensionProject:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PrevensionProject
	{
		public PrevensionProject()
		{}
		#region Model
		private int _id;
		private string _projectname;
		private string _dangerousgroup;
		private string _decisionrule;
		private string _adaptscope;
		private string _measure;
		private string _aim;
		private int? _status;
		private int? _evalguidid;
		/// <summary>
		/// 方案编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 方案名称
		/// </summary>
		public string ProjectName
		{
			set{ _projectname=value;}
			get{return _projectname;}
		}
		/// <summary>
		/// 危险分组
		/// </summary>
		public string DangerousGroup
		{
			set{ _dangerousgroup=value;}
			get{return _dangerousgroup;}
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
		/// 适用范围
		/// </summary>
		public string AdaptScope
		{
			set{ _adaptscope=value;}
			get{return _adaptscope;}
		}
		/// <summary>
		/// 防治措施
		/// </summary>
		public string Measure
		{
			set{ _measure=value;}
			get{return _measure;}
		}
		/// <summary>
		/// 控制目标
		/// </summary>
		public string Aim
		{
			set{ _aim=value;}
			get{return _aim;}
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

