using System;
namespace Gulliver.DoCol.Entities.Common
{
	public class CmnFaxModel
	{
		/// <summary>
		/// Gets or sets Order No
		/// </summary>
		public string OrderNo { get; set; }

		/// <summary>
		/// Gets or sets Send Fax Kbn
		/// </summary>
		public byte SendFaxKbn { get; set; }

		/// <summary>
		/// Gets or sets Batch Status
		/// </summary>
		public byte BatchStatus { get; set; }

		/// <summary>
		/// Gets or sets Create Date
		/// </summary>
		public DateTime CreateDate { get; set; }

		/// <summary>
		/// Gets or sets Create User
		/// </summary>
		public string CreateUser { get; set; }

		/// <summary>
		/// Gets or sets Update Date
		/// </summary>
		public DateTime UpdateDate { get; set; }

		/// <summary>
		/// Gets or sets Update User
		/// </summary>
		public string UpdateUser { get; set; }

		/// <summary>
		/// Gets or sets Sosin Date
		/// </summary>
		public DateTime SosinDate { get; set; }
	}
}
