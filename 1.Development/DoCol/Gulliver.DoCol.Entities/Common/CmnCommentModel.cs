using System;
namespace Gulliver.DoCol.Entities.Common
{
	public class CmnCommentModel
	{
		/// <summary>
		/// Gets or sets Order No
		/// </summary>
		public string OrderNo { get; set; }

		/// <summary>
		/// Gets or sets Comment Source Kbn
		/// </summary>
		public bool CommentSourceKbn { get; set; }

		/// <summary>
		/// Gets or sets Comment Source
		/// </summary>
		public string CommentSource { get; set; }

		/// <summary>
		/// Gets or sets Sosin Date
		/// </summary>
		public DateTime SosinDate { get; set; }

		/// <summary>
		/// Gets or sets Denpyo Type
		/// </summary>
		public byte DenpyoType { get; set; }

		/// <summary>
		/// Gets or sets Comment Kbn Seq No
		/// </summary>
		public int CommentKbnSeqNo { get; set; }

		/// <summary>
		/// Gets or sets Comment Naiyou
		/// </summary>
		public string CommentNaiyou { get; set; }

		/// <summary>
		/// Gets or sets Create User
		/// </summary>
		public string CreateUser { get; set; }

		/// <summary>
		/// Gets or sets Create Date
		/// </summary>
		public DateTime CreateDate { get; set; }

		/// <summary>
		/// Gets or sets Update User
		/// </summary>
		public string UpdateUser { get; set; }

		/// <summary>
		/// Gets or sets Update Date
		/// </summary>
		public DateTime UpdateDate { get; set; }
	}
}
