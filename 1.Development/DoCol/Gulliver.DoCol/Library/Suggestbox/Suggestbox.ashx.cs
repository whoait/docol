//---------------------------------------------------------------------------
// Version			: 001
// Designer			: DungNH6-FPT
// Programmer		: DungNH6-FPT
// Date				: 2015/04/10
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.Library.Suggestbox
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Web;
	using System.Web.Script.Serialization;
	using System.Web.SessionState;
	using Gulliver.DoCol.BusinessServices.Common;
	using Gulliver.DoCol.Entities.Common;

	/// <summary>
	/// Summary description for Suggestbox
	/// </summary>
	public class Suggestbox : IHttpHandler, IRequiresSessionState
	{
		#region Declare Variables
		private readonly int maxItem = int.Parse( ConfigurationManager.AppSettings["SuggestionMaxResult"] );
		#endregion Declare Variables

		public void ProcessRequest( HttpContext context )
		{
			string key = context.Request.QueryString["key"];
			string model = context.Request.QueryString["model"];

			context.Response.ContentType = "application/javascript";
			var listItemResult = new List<CmnSuggestionModel>();
			if (!String.IsNullOrEmpty( key ))
			{
				switch (model)
				{
					case "CmnShopCd":
						{
							listItemResult = this.GetSuggestionShopCd( key, this.maxItem);
							break;
						}
					case "CmnShopName":
						{
							listItemResult = this.GetSuggestionShopName( key, this.maxItem );
							break;
						}
				}
			}

			context.Response.Write( new JavaScriptSerializer().Serialize( listItemResult ) );
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public List<CmnSuggestionModel> GetSuggestionShopCd( string tValue, int limit )
		{
			return CmnSuggestionService.GetSuggestionShopCd( tValue, limit );
		}

		public List<CmnSuggestionModel> GetSuggestionShopName( string tValue, int limit )
		{
			return CmnSuggestionService.GetSuggestionShopName( tValue, limit );
		}
	}
}