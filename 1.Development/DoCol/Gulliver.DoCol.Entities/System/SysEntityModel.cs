//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.Entities
{
	using System.Data;

	/// <summary>
	/// Common entity Model
	/// </summary>
	public class CmnEntityModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CmnEntityModel"/> class.
		/// </summary>
		public CmnEntityModel()
		{
			this.Clear();
			this.CurrentScreenID = string.Empty;
			this.ParentScreenID = string.Empty;
			this.ScreenRoute = string.Empty;
		}

		public string TabID { get; set; }

		/// <summary>
		/// Gets or sets the company code.
		/// </summary>
		/// <value>
		/// The company code.
		/// </value>
		public string KaishaCd { get; set; }

		/// <summary>
		/// Gets or sets the name of the company.
		/// </summary>
		/// <value>
		/// The name of the company.
		/// </value>
		public string KaishaName { get; set; }

		/// <summary>
		/// Gets or sets the deparment code.
		/// </summary>
		/// <value>
		/// The deparment code.
		/// </value>
		public string BushoCd { get; set; }

		/// <summary>
		/// Gets or sets the name of the deparment.
		/// </summary>
		/// <value>
		/// The name of the deparment.
		/// </value>
		public string BushoName { get; set; }

		/// <summary>
		/// Gets or sets the pic tantosha code.
		/// </summary>
		/// <value>
		/// The tantosha code.
		/// </value>
		public int TantoshaCd { get; set; }

		/// <summary>
		/// Gets or sets the section cd.
		/// </summary>
		/// <value>
		/// The section cd.
		/// </value>
		public string SectionCd { get; set; }

		/// <summary>
		/// Gets or sets the name of the section.
		/// </summary>
		/// <value>
		/// The name of the section.
		/// </value>
		public string SectionName { get; set; }

		/// <summary>
		/// Gets or sets the corporation cd.
		/// </summary>
		/// <value>
		/// The corporation cd.
		/// </value>
		public string CorporationCd { get; set; }

		/// <summary>
		/// Gets or sets the name of the corporation.
		/// </summary>
		/// <value>
		/// The name of the corporation.
		/// </value>
		public string CorporationName { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public string UserID { get; set; }

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>
		/// The role.
		/// </value>
		public string Role { get; set; }

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <value>
        /// The password of the user.
        /// </value>
        public string Password { get; set; }
		/// <summary>
		/// Gets or sets the store identifier.
		/// </summary>
		/// <value>
		/// The store identifier.
		/// </value>
		public string StoreId { get; set; }

		/// <summary>
		/// Gets or sets the error MSG cd.
		/// </summary>
		/// <value>
		/// The error MSG cd.
		/// </value>
		public string ErrorMsgCd { get; set; }

		/// <summary>
		/// Gets or sets the error MSG replace string.
		/// </summary>
		/// <value>
		/// The error MSG replace string.
		/// </value>
		public string ErrorMsgReplaceString { get; set; }

		/// <summary>
		/// Gets or sets the user authority.
		/// </summary>
		/// <value>
		/// The user authority.
		/// </value>
		public LoginUserAuthorityModel UserAuthority { get; set; }


		/// <summary>
		/// Gets or sets the current screen identifier.
		/// </summary>
		/// <value>
		/// The current screen identifier.
		/// </value>
		public string CurrentScreenID { get; set; }

		/// <summary>
		/// Gets or sets the parent screen identifier.
		/// </summary>
		/// <value>
		/// The parent screen identifier.
		/// </value>
		public string ParentScreenID { get; set; }

		/// <summary>
		/// Gets or sets the screen route.
		/// </summary>
		/// <value>
		/// The screen route.
		/// </value>
		public string ScreenRoute { get; set; }

		/// <summary>
		/// Gets or sets the seq no.
		/// </summary>
		/// <value>
		/// The seq no.
		/// </value>
		public int SeqNo { get; set; }

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>
		/// The factory display name.
		/// </value>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the Shop Code.
		/// </summary>
		/// <value>
		/// The Shop Code.
		/// </value>
		public string TempoCd { get; set; }

		/// <summary>
		/// Gets or sets the Shop name.
		/// </summary>
		/// <value>
		/// The Shop name.
		/// </value>
		public string TempoName { get; set; }

		/// <summary>
		/// Gets or sets the Employee No.
		/// </summary>
		/// <value>
		/// The Employee No.
		/// </value>
		public string ShainNo { get; set; }

		/// <summary>
		/// Gets or sets the Employee name.
		/// </summary>
		/// <value>
		/// The Employee name.
		/// </value>
		public string ShainName { get; set; }

		/// <summary>
		/// Check mode login is HQ or Management or Staff
		/// </summary>
		public string ModeLogin { get; set; }

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public void Clear()
		{
			this.KaishaCd = string.Empty;
			this.BushoCd = string.Empty;
			this.SectionCd = string.Empty;
			this.SectionName = string.Empty;
			this.CorporationCd = string.Empty;
			this.CorporationName = string.Empty;
			this.UserID = string.Empty;
			this.UserName = string.Empty;
			this.StoreId = string.Empty;
			this.ErrorMsgCd = string.Empty;
			this.ErrorMsgReplaceString = string.Empty;
			this.UserAuthority = null;
			this.Role = string.Empty;
			this.SeqNo = 0;
			this.TantoshaCd = 0;
			this.DisplayName = string.Empty;
			this.TempoCd = string.Empty;
			this.TempoName = string.Empty;
			this.ShainNo = string.Empty;
			this.ShainName = string.Empty;
		}
	}
}