//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.Entities.Common
{
	public class CmnPagingModel
	{
		/// <summary>
		/// Page index
		/// </summary>
		public int PageIndex { get; set; }

		/// <summary>
		/// Page size
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Sort item
		/// </summary>
		public string SortItem { get; set; }

		/// <summary>
		/// Sort Direction
		/// </summary>
		public byte SortDirection { get; set; }

		/// <summary>
		/// Total record
		/// </summary>
		public int TotalRow { get; set; }

		/// <summary>
		/// Page Begin
		/// </summary>
		public int PageBegin { get; set; }

		/// <summary>
		/// Page End
		/// </summary>
		public int PageEnd { get; set; }
	}
}