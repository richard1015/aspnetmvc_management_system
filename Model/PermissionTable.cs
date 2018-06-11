using System;
namespace HTNResp.Model
{
	/// <summary>
	/// PermissionTable:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PermissionTable
	{
		public PermissionTable()
		{}
		#region Model
		private int _id;
		private int _tableid;
		private int _userid;
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
		public int TableID
		{
			set{ _tableid=value;}
			get{return _tableid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		#endregion Model

	}
}

