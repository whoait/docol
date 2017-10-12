namespace Gulliver.DoCol.DataAccess.Common
{
	#region using

	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Web.Mvc;
    using Gulliver.DoCol.Constants;
    using Gulliver.DoCol.DataAccess.Framework;
    using Gulliver.DoCol.Entities;
    using Gulliver.DoCol.Entities.Common;

	#endregion using

	/// <summary>
	/// Common data access
	/// </summary>
	public class CmnDa : Gulliver.DoCol.DataAccess.BaseDa
	{
		public void GetSuggestion<T>( string textPattern, int maxResult, out List<T> listSuggest, string storeName ) where T : new()
		{
			using (var dbManager = new DBManager( storeName ))
			{
				dbManager.Add( SysStoreName.para_MaxResult, maxResult );
				dbManager.Add( SysStoreName.para_TextPattern, textPattern );

				DataTable dt = dbManager.GetDataTable();
				listSuggest = EntityHelper<T>.GetListObject( dt );
			}
		}
	}
}