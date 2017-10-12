//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.DataAccess
{
	using System;
	using System.Data;

	using Gulliver.DoCol.Constants;
	using Gulliver.DoCol.DataAccess.Framework;

	/// <summary>
	/// UtilityDa
	/// </summary>
	public static partial class UtilityDa
	{
		// TODO

		/// <summary>
		/// The sequence get value.
		/// </summary>
		/// <param name="seqKey">
		/// The seq key.
		/// </param>
		/// <param name="seqValue">
		/// The seq value.
		/// </param>
		/// <returns>
		/// The <see cref="ReturnCode"/>.
		/// </returns>
		public static ReturnCode SequenceGetValue( string seqKey, out string seqValue )
		{
			seqValue = String.Empty;
			using (var dbManager = new DBManager( "stp_Sequence_GetValue" ))
			{
				dbManager.Add( "@SeqKey", seqKey );
				dbManager.Add( "@SeqValue", seqValue, ParameterDirection.Output );
				dbManager.ExecuteNonQuery();

				return (ReturnCode)dbManager.ReturnValue;
			}
		}
		///// <summary>
		///// Get suggestion company
		///// </summary>
		///// <param name="TraderName"> Company name </param>
		///// <param name="CompanyId"> Company code </param>
		///// <param name="timeStamp"> time stamp </param>
		//public static void GetSuggestionCompany( string textPattern, int maxResult, out long timeStamp, out List<CmnCompanyModel> listSuggest )
		//{
		//	timeStamp = 0;
		//	using (DBManager dbManager = new DBManager( SysStoreName.stp_GetSuggCompany ))
		//	{
		//		dbManager.Add( SysStoreName.para_MaxResult, maxResult );
		//		dbManager.Add( SysStoreName.para_TextPattern, textPattern );
		//		dbManager.Add( SysStoreName.para_TimeStamp, timeStamp, ParameterDirection.Output );
		//		DataTable dt = dbManager.GetDataTable();
		//		listSuggest = EntityHelper<CmnCompanyModel>.GetListObject( dt );
		//		timeStamp = Convert.ToInt64( dbManager[SysStoreName.para_TimeStamp].Value );
		//	}
		//}

		///// <summary>
		/////
		///// </summary>
		///// <param name="masterTableName"></param>
		///// <param name="masterFieldName"></param>
		///// <param name="timeStamp"></param>
		///// <param name="masterNameDs"></param>
		//public static void GetMaster( string masterTableName,
		//								string masterFieldName,
		//								string msterFieldCode,
		//								out long timeStamp,
		//								out List<CmnSuggestionModel> listSuggest )
		//{
		//	timeStamp = 0;
		//	using (DBManager dbManager = new DBManager( "stp_Master_GetMaster" ))
		//	{
		//		dbManager.Add( "@MasterTableName", masterTableName );
		//		dbManager.Add( "@MasterFieldName", masterFieldName );
		//		dbManager.Add( "@MasterFieldCode", msterFieldCode );
		//		dbManager.Add( "@MasterTableTimeStamp", timeStamp, ParameterDirection.Output );
		//		DataTable dt = dbManager.GetDataTable();
		//		listSuggest = EntityHelper<CmnSuggestionModel>.GetListObject( dt );
		//		timeStamp = Convert.ToInt64( dbManager["@MasterTableTimeStamp"].Value );
		//	}
		//}

		///// <summary>
		/////
		///// </summary>
		///// <param name="masterTableName"></param>
		///// <param name="timeStamp"></param>
		///// <param name="isUpdated"></param>
		//public static void CheckMaster( string masterTableName,
		//								Int64 timeStamp,
		//								out bool isUpdated )
		//{
		//	isUpdated = false;
		//	int haveRecord = 0;
		//	using (DBManager dbManager = new DBManager( "stp_Master_CheckMaster" ))
		//	{
		//		dbManager.Add( "@MasterTableName", masterTableName );
		//		dbManager.Add( "@MasterTableTimeStamp", timeStamp );
		//		dbManager.Add( "@HaveRecord", haveRecord, ParameterDirection.Output );
		//		dbManager.ExecuteNonQuery();
		//		if (haveRecord > 0)
		//		{
		//			isUpdated = false;
		//		}
		//		else
		//		{
		//			isUpdated = true;
		//		}
		//	}
		//}
	}
}