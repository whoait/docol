//---------------------------------------------------------------------------
// Version			: 001
// Designer			: NguyenPTV1
// Programmer		: NguyenPTV1
// Date				: 2014/06/08
// Comment			: Create new
//---------------------------------------------------------------------------

using Gulliver.DoCol.Constants;
using Gulliver.DoCol.Constants.Resources;
using Gulliver.DoCol.Entities;
using Gulliver.DoCol.UtilityServices.security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Gulliver.DoCol.UtilityServices
{
	/// <summary>
	/// Provide method to save/get cache, avoid the session issue on load balancing Web farm
	/// </summary>
	public static class CacheUtil
	{
		#region Application
		/// <summary>
		/// Short Live Cache is cache that existed during live of a request.
		/// After server response to client, the Short Live Cache will be clear.
		/// </summary>
		/// <param name="key">key string</param>
		/// <returns>object value</returns>
		public static object GetShortLiveCache( string key )
		{
			Dictionary<string, Dictionary<string, object>> cachedDictionary
				= (Dictionary<string, Dictionary<string, object>>)HttpContext.Current.Application["shortLiveCache"];
			if (!cachedDictionary.ContainsKey( HttpContext.Current.Session.SessionID ))
			{
				Dictionary<string, object> sessionCachedDictionary = cachedDictionary[HttpContext.Current.Session.SessionID];
				if (sessionCachedDictionary.ContainsKey( key ))
				{
					return sessionCachedDictionary[key];
				}
			}
			return null;
		}

		/// <summary>
		/// Short Live Cache is cache that existed during live of a request.
		/// After server response to client, the Short Live Cache will be clear.
		/// </summary>
		/// <param name="key">key string</param>
		/// <param name="value">object value</param>
		public static void SaveShortLiveCache( string key, object value )
		{
			Dictionary<string, Dictionary<string, object>> cachedDictionary
				= (Dictionary<string, Dictionary<string, object>>)HttpContext.Current.Application["shortLiveCache"];
			Dictionary<string, object> sessionCachedDictionary;
			if (cachedDictionary.ContainsKey( HttpContext.Current.Session.SessionID ))
			{
				sessionCachedDictionary = cachedDictionary[HttpContext.Current.Session.SessionID];
				if (sessionCachedDictionary.ContainsKey( key ))
				{
					sessionCachedDictionary[key] = value;
				}
				else
				{
					sessionCachedDictionary.Add( key, value );
				}
			}
			else
			{
				sessionCachedDictionary = new Dictionary<string, object>();
				sessionCachedDictionary.Add( key, value );
				cachedDictionary.Add( HttpContext.Current.Session.SessionID, sessionCachedDictionary );
			}
		}

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
		/// Short Live Cache is cache that existed during live of a request.
		/// After server response to client, the Short Live Cache will be clear.
		/// </summary>
		public static void ClearShortLiveCache()
		{
			Dictionary<string, Dictionary<string, object>> cachedDictionary
				= (Dictionary<string, Dictionary<string, object>>)HttpContext.Current.Application["shortLiveCache"];
			if (cachedDictionary.ContainsKey( HttpContext.Current.Session.SessionID ))
			{
				cachedDictionary.Remove( HttpContext.Current.Session.SessionID );
			}
		}
		#endregion

		#region Session
		public static void SaveCache( string key, object value )
		{
			//if (!string.IsNullOrEmpty( CmnTabEntityModel.TabID ))
			//{
			//	if (key != "GLV_SYS_SQLTimeOut" && key != "SQL_TimeOutMessage" && key != "GLV_SYS_RunTimeErrorMsg" && key != "GLV_SYS_PERMISION" && key != "GLV_SYS_Cache_RunTimeErrorMsg" && key != "SQL_TimeOutMessage" && key != "GLV_SYS_PERMISION_SCREEN_ROOT" && key != "CmnEntityModel")
			//	{
			//		key += CmnTabEntityModel.TabID;
			//	}
			//}
			HttpContext.Current.Session[key] = SerializeObject( value );
		}

		public static T GetCache<T>( string key, object defaultValue = null )
		{
			//if (!string.IsNullOrEmpty( CmnTabEntityModel.TabID ))
			//{
			//	if (key != "GLV_SYS_SQLTimeOut" && key != "SQL_TimeOutMessage" && key != "GLV_SYS_RunTimeErrorMsg" && key != "GLV_SYS_PERMISION" && key != "GLV_SYS_Cache_RunTimeErrorMsg" && key != "SQL_TimeOutMessage" && key != "GLV_SYS_PERMISION_SCREEN_ROOT" && key != "CmnEntityModel")
			//	{
			//		key += CmnTabEntityModel.TabID;
			//	}
			//}

			if (HttpContext.Current.Session[key] == null)
			{
				return (T)defaultValue;
			}

			return (T)DeSerializeObject( HttpContext.Current.Session[key], typeof( T ) );
		}

		public static void RemoveCache( string key )
		{
			//if (!string.IsNullOrEmpty( CmnTabEntityModel.TabID ))
			//{
			//	if (key != "GLV_SYS_SQLTimeOut" && key != "SQL_TimeOutMessage" && key != "GLV_SYS_RunTimeErrorMsg" && key != "GLV_SYS_PERMISION" && key != "GLV_SYS_Cache_RunTimeErrorMsg" && key != "SQL_TimeOutMessage" && key != "GLV_SYS_PERMISION_SCREEN_ROOT" && key != "CmnEntityModel")
			//	{
			//		key += CmnTabEntityModel.TabID;
			//	}
			//}
			HttpContext.Current.Session.Remove( key );
		}

		public static void RemoveCache( string tabID, string screenID )
		{
			List<string> sessionKey = new List<string>();
			// Loop all key session
			foreach (string key in HttpContext.Current.Session.Keys)
			{
				// Select key which contains tabID and contain screen ID and add it to list
				if (key.Contains( tabID ) && key.Contains( screenID ))
				{
					sessionKey.Add( key );
				}
			}

			// Remove cache key after add to list
			foreach (string key in sessionKey)
			{
				RemoveCache( key );
			}
		}

		public static void RemoveCacheInTab( string tabId )
		{
			List<string> sessionKey = new List<string>();

			// Loop all key session
			foreach (string key in HttpContext.Current.Session.Keys)
			{
				// Select key which contains tabID and not contain CmnTabEntityModel and add it to list
				if (key.Contains( tabId ) && !key.Contains( "CmnTabEntityModel" ))
				{
					sessionKey.Add( key );
				}
			}

			// Remove cache key after add to list
			foreach (string key in sessionKey)
			{
				RemoveCache( key );
			}
		}

		public static void RemoveCacheInScreenRoute( string tabId, string screenRoute )
		{
			string screenType;
			ResourceManager managerResource = new ResourceManager( typeof( ScreenList ) );
			if (string.IsNullOrEmpty( screenRoute ))
			{
				return;
			}

			string[] screenList = screenRoute.Split( ',' );
			foreach (var screenId in screenList)
			{
				screenType = managerResource.GetString( screenId );
				if (string.IsNullOrEmpty( screenType ) && screenType.Equals( "SearchOrListPage" ))
				{
					continue;
				}

				RemoveCache( tabId, screenId );
			}
		}

		public static void CloneTabCache( string oldTabId, string newTabId )
		{
			List<string> sessionKey = new List<string>();

			// Loop all key session
			foreach (string key in HttpContext.Current.Session.Keys)
			{
				// Select key which contains tabID and add it to list
				if (key.Contains( oldTabId ))
				{
					sessionKey.Add( key );
				}
			}

			Regex regex = new Regex( @"^_\w*_" );

			// Add cache key after add to list
			foreach (string key in sessionKey)
			{
				string newKey = regex.Replace( key, "_" + newTabId + "_" );
				HttpContext.Current.Session[newKey] = HttpContext.Current.Session[key];
			}

		}

		public static void RemoveAllCache()
		{
			HttpContext.Current.Session.Clear();
		}
		#endregion

		private static string SerializeObject<T>( this T toSerialize )
		{
			return new JavaScriptSerializer().Serialize( toSerialize );
		}

		private static object DeSerializeObject( object myObject, Type objectType )
		{
			var resultObj = new JavaScriptSerializer().Deserialize( Convert.ToString( myObject ), objectType );

			// Avoid DateTime losing a day
			foreach (PropertyInfo propertyInfo in objectType.GetProperties())
			{
				if (propertyInfo.CanRead)
				{
					if ((propertyInfo.PropertyType == typeof( DateTime ) || propertyInfo.PropertyType == typeof( DateTime? ))
						&& propertyInfo.GetValue( resultObj, null ) != null)
					{
						propertyInfo.SetValue( resultObj, ((DateTime)propertyInfo.GetValue( resultObj, null )).ToLocalTime() );
					}
				}
			}

			return resultObj;
		}
	}
}