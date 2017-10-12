//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Caching;

namespace Gulliver.DoCol.UtilityServices
{
	public class DataCaching
	{
		private static double CacheTimeSpanInMinute = SettingsCommon.CacheDurationInMinute;

		public static void Add( string _key, object _dv )
		{
			Add( _key, _dv, false );
		}

		public static void Add( string _key, object _dv, bool isSlidingExpiration )
		{
			///for sliding expiration
			if (isSlidingExpiration)
			{
				HttpRuntime.Cache.Insert( _key, _dv, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes( CacheTimeSpanInMinute ), CacheItemPriority.High, null );
			}
			else
			{
				///for absolute expiration
				HttpRuntime.Cache.Insert( _key, _dv, null, DateTime.Now.Add( TimeSpan.FromMinutes( CacheTimeSpanInMinute ) ), Cache.NoSlidingExpiration, CacheItemPriority.High, null );
			}
		}

		public static object Get( string _key )
		{
			try
			{
				return HttpRuntime.Cache[_key];
			}
			catch
			{
				return null;
			}
		}

		public static void Clear( string _key )
		{
			try
			{
				HttpRuntime.Cache.Remove( _key );
			}
			catch { }
		}
	}
}