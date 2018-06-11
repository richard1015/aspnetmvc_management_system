using System;
namespace HTNResp.Model
{
	/// <summary>
	/// EvalGuid:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EvalGuid
	{
		public EvalGuid()
		{}
		#region Model
		private int _id;
		private string _guidname;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GuidName
		{
			set{ _guidname=value;}
			get{return _guidname;}
		}
		#endregion Model

	}
}

