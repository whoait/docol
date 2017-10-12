namespace Gulliver.DoCol.BusinessServices.Common
{
	#region Using
	using System.Collections.Generic;
	using System.Linq;
    using Gulliver.DoCol.Constants;
    using Gulliver.DoCol.DataAccess.Common;
    using Gulliver.DoCol.Entities.Common;
    using Gulliver.DoCol.UtilityServices;
    using Gulliver.DoCol.DataAccess.Framework;
	#endregion Using

	/// <summary>
	/// The common suggestion service.
	/// </summary>
	public static class CmnSuggestionService
	{
		public static List<CmnSuggestionModel> GetSuggestionShopCd( string textPattern, int limit, bool? isCheckSoshiki = null )
		{
			List<CmnSuggestionModel> listSuggestionResult;
			GetSuggestion( textPattern, limit, out listSuggestionResult, SysStoreName.STP_GET_SUGGESTION_SHOP_CD);

			var retValue = listSuggestionResult.Select( r => new CmnSuggestionModel
			{
				FieldCode = r.FieldCode.Trim(),
				FieldName = !string.IsNullOrWhiteSpace( r.FieldName ) ? r.FieldName.Trim() : "",
				FieldDisplay = r.FieldCode.Trim() + " " + r.FieldName.Trim()
			}).ToList();

			return retValue;
		}

		public static List<CmnSuggestionModel> GetSuggestionShopName( string textPattern, int limit, bool? isCheckSoshiki = null )
		{
			List<CmnSuggestionModel> listSuggestionResult;
			GetSuggestion( textPattern, limit, out listSuggestionResult, SysStoreName.STP_GET_SUGGESTION_SHOP_NAME);

			var retValue = listSuggestionResult.Select( r => new CmnSuggestionModel
			{
				FieldCode = r.FieldCode.Trim(),
				FieldName = !string.IsNullOrWhiteSpace( r.FieldName ) ? r.FieldName.Trim() : "",
				FieldDisplay = r.FieldCode.Trim() + " " + r.FieldName.Trim()
			} ).ToList();

			return retValue;
		}

		private static void GetSuggestion(string target, int limit,	out List<CmnSuggestionModel> listSuggestion, string storeName)
		{
			new CmnDa().GetSuggestion( target, limit, out listSuggestion, storeName );
			ForceDispose();
			//var listItem = listSuggestion;
			//var listItemFullSize = listSuggestion.Select( t => new CmnSuggestionModel
			//								{
			//									FieldCode = t.FieldCode,
			//									FieldName = t.FieldName
			//								}).ToList();

			//listSuggestion = FilterSuggestion( target, limit, listItem, listItemFullSize );
		}

		//private static List<CmnSuggestionModel> FilterSuggestion( string target, int limit, List<CmnSuggestionModel> listItem, List<CmnSuggestionModel> listItemFullsize )
		//{
		//	var result = new List<CmnSuggestionModel>();
		//	int count = limit;
		//	for (int indexList = 0; indexList < listItemFullsize.Count; indexList++)
		//	{
		//		if (!Utility.IsContentEqual( listItemFullsize[indexList].FieldCode.Trim(), target )
		//			&& !Utility.IsContentMatch( listItemFullsize[indexList].FieldName.Trim(), target ))
		//		{
		//			continue;
		//		}

		//		result.Add( listItem[indexList] );
		//		count--;
		//		if (count == 0)
		//		{
		//			break;
		//		}
		//	}

		//	return result;
		//}

		private static void ForceDispose()
		{
			DBManager.CommitTransaction();
			DBManager.CloseConnection();
		}
	}
}