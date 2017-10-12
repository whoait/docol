//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.UtilityServices
{
	using Gulliver.DoCol.Entities;
	using Gulliver.DoCol.UtilityServices.security;
	using System;
	using System.Security.Cryptography;
	using System.Text;
	using System.Web;

	/// <summary>
	/// URI utility
	/// </summary>
	public class UriUtility
	{
		/// <summary>
		/// The rout e_ para m_ name
		/// </summary>
		public const string ROUTE_PARAM_NAME = "screenroute";

		/// <summary>
		/// Gets the CMN entity model.
		/// </summary>
		/// <value>
		/// The CMN entity model.
		/// </value>
		private static CmnEntityModel CmnEntityModel
		{
			get
			{
				if (HttpContext.Current.Session["CmnEntityModel"] == null)
				{
					HttpContext.Current.Session["CmnEntityModel"] = new CmnEntityModel();
				}
				return (CmnEntityModel)HttpContext.Current.Session["CmnEntityModel"];
			}
		}

		/// <summary>
		/// Gets the route value.
		/// </summary>
		/// <value>
		/// The route value.
		/// </value>
		public static string RouteValue
		{
			get
			{
				if (string.IsNullOrEmpty( CmnEntityModel.TabID ))
				{
					CmnEntityModel.TabID = ConvertStringtoMD5( DateTime.Now.ToString() );
				}
				return Encrypt.GetUrlEncryptStr( CmnEntityModel.ScreenRoute + "-" + CmnEntityModel.TabID );
			}
		}

		/// <summary>
		/// Convert String to MD5 to add tab id
		/// </summary>
		/// <param name="strword">strword</param>
		/// <returns>string word after convert</returns>
		public static string ConvertStringtoMD5( string strword )
		{
			MD5 md5 = MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes( strword );
			byte[] hash = md5.ComputeHash( inputBytes );
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append( hash[i].ToString( "x2" ) );
			}
			return sb.ToString();
		}
		
		/// <summary>
        /// Get ScreenID From URL 
        /// </summary>
        /// <param name="url"></param>
        /// <returns>ScreenID</returns>
        public static string GetScreenIDFromURL(string url)
        {
            string[] arr = url.Replace("//", "/").Split('/');
            if (arr.Length > 3)
                return arr[3];

            return string.Empty;
        }

        /// <summary>
        /// Get Param Value From URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns>value of param</returns>
        public static string GetParamValueFromURL(string url, string paramName)
        {
            Uri myUri = new Uri(url);
            return HttpUtility.ParseQueryString(myUri.Query).Get(paramName);
        }
	}
}