using System;
namespace HTNResp.Model
{
	/// <summary>
	/// ControlContent:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ControlContent
	{
		public ControlContent()
		{}
		#region Model
		private string _controlname;
		private string _title;
		private string _remark;
		private string _controltype;
		/// <summary>
		/// 控件名称
		/// </summary>
		public string ControlName
		{
			set{ _controlname=value;}
			get{return _controlname;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
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
		/// 控件类型
		/// </summary>
		public string ControlType
		{
			set{ _controltype=value;}
			get{return _controltype;}
		}
		#endregion Model

	}
}

