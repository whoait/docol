//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.Entities
{
	using System;

	/// <summary>
	/// Common entity Model
	/// </summary>
	public class CmnTabEntityModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CmnTabEntityModel"/> class.
		/// Initializes a new instance of the <see cref="CmnEntityModel"/> class.
		/// </summary>
		public CmnTabEntityModel()
		{
			this.CurrentScreenID = string.Empty;
			this.ScreenRoute = string.Empty;
		}

		/// <summary>
		/// Gets or sets the tab id.
		/// </summary>
		public string TabID { get; set; }

		/// <summary>
		/// Gets or sets the current screen identifier.
		/// </summary>
		/// <value>
		/// The current screen identifier.
		/// </value>
		public string CurrentScreenID { get; set; }

		/// <summary>
		/// Gets or sets the screen route.
		/// </summary>
		/// <value>
		/// The screen route.
		/// </value>
		public string ScreenRoute { get; set; }

		/// <summary>
		/// Gets or sets the parrent screen.
		/// </summary>
		/// <value>
		/// The parrent screen.
		/// </value>
		public string ParrentScreenID { get; set; }

		public Boolean? IsRefreshed { get; set; }
	}
}