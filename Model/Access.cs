using System;
namespace HTNResp.Model
{
	/// <summary>
	/// AccessTable:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AccessTable
	{
		public AccessTable()
		{}
		#region Model
		private int _id;
		private string _tablename;
		private string _tableadress;
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
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TableAdress
		{
			set{ _tableadress=value;}
			get{return _tableadress;}
		}
		#endregion Model

	}
}

