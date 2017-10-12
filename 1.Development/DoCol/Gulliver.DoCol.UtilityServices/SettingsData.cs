//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Caching;
using System.Xml;

namespace Gulliver.DoCol.UtilityServices
{
	public class SettingsData
	{
		private bool isLoadComplete = false;

		private XmlDocument xmlDocument = new XmlDocument();

		private string strConfigFilePath = string.Empty;

		public bool IsLoadComplete
		{
			get
			{
				return isLoadComplete;
			}
		}

		//public bool IsWCFCall
		//{
		//    get
		//    {
		//        if (ReferenceEquals(HttpContext.Current, null)) //From WCF
		//        {
		//            return true;
		//        }
		//        else
		//        {
		//            return false;
		//        }
		//    }

		//}

		/// <summary>
		/// get Data XML
		/// </summary>
		protected XmlDocument Data
		{
			get
			{
				if (!AppDomain.CurrentDomain.FriendlyName.Contains( ".exe" ))
				{
					if (ReferenceEquals( System.Web.HttpRuntime.Cache["AppSettings"], null ))
					{
						this.LoadSettings();
					}
				}
				return xmlDocument;
			}
			set
			{
				if (!AppDomain.CurrentDomain.FriendlyName.Contains( ".exe" ))
				{
					this.xmlDocument = value;
				}
			}
		}

		/// <summary>
		/// Get or set filepath
		/// </summary>
		public string FilePath
		{
			get
			{
				return strConfigFilePath;
			}
			set
			{
				strConfigFilePath = value;
			}
		}

		/// <summary>
		/// Load Settings Xml
		/// </summary>
		public void Load()
		{
			/*
			if (File.Exists(this.FilePath))
			{
				loadSettings();
			}
			*/
			this.LoadSettings();
		}

		/// <summary>
		/// Select specific node by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private XmlNode selectNode( string key )
		{
			string xPath = string.Empty;
			xPath = string.Format( "//appSettings/add[@key='{0}']/@value", key );

			return this.Data.SelectSingleNode( xPath );
		}

		/// <summary>
		/// get text as string data type
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string Get( string key )
		{
			return XMLHelper.GetInnerText( selectNode( key ) );
		}

		/// <summary>
		/// get text as int data type
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public int GetInt( string key )
		{
			return XMLHelper.GetInnerTextInt( selectNode( key ) );
		}

		/// <summary>
		/// get text as long data type
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public long GetLong( string key )
		{
			return XMLHelper.GetInnerTextLong( selectNode( key ) );
		}

		/// <summary>
		/// get text as bool data type
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool GetBool( string key )
		{
			return XMLHelper.GetInnerText( selectNode( key ) ).ToLower().Equals( "true" );
		}

		/// <summary>
		/// for mail content
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetXML( string key )
		{
			//<![CDATA[]]>
			string xPath = string.Empty;
			xPath = string.Format( "//appSettings/add[@key='{0}']", key );

			string xmlText = XMLHelper.GetInnerXml( this.Data.SelectSingleNode( xPath ) ).Trim();
			if (xmlText.StartsWith( "<![CDATA[" ) && xmlText.EndsWith( "]]>" ))
			{
				xmlText = xmlText.Replace( "<![CDATA[", "" ).Replace( "]]>", "" );
			}
			return xmlText;
		}

		/// <summary>
		/// Load Settings
		/// </summary>
		private void LoadSettings()
		{
			this.Data = null;
			this.Data = new XmlDocument();

			try
			{
				string data = string.Empty;

				if (!AppDomain.CurrentDomain.FriendlyName.Contains( ".exe" ))
				{
					GetAppSettings();
					DataTable dt = new DataTable();

					if (!ReferenceEquals( System.Web.HttpRuntime.Cache["AppSettings"], null ))
					{
						dt = (DataTable)System.Web.HttpRuntime.Cache["AppSettings"];
						foreach (DataRow dr in dt.Rows)
						{
							data += dr[0].ToString();
						}
					}
				}
				else
				{
					data = System.IO.File.ReadAllText( this.FilePath );
				}
				this.Data.LoadXml( data );
				isLoadComplete = true;
			}
			catch (Exception)
			{
				isLoadComplete = false;
			}
		}

		#region APP_SETTINGS

		public void GetAppSettings()
		{
			string ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

			if ((!string.IsNullOrEmpty( System.Configuration.ConfigurationManager.AppSettings["LoadBalancedEnvironment"] )) && System.Configuration.ConfigurationManager.AppSettings["LoadBalancedEnvironment"] == "true")
			{
				if (ReferenceEquals( System.Web.HttpRuntime.Cache["AppSettings"], null ))
				{
					System.Web.HttpRuntime.Cache.Insert( "AppSettings", GetDataFromDB() );
				}
			}
			else
			{
				if (ReferenceEquals( System.Web.HttpRuntime.Cache["AppSettings"], null ))
				{
					SqlDependency.Start( ConnectionString );
					AggregateCacheDependency aggDepCustomer = new AggregateCacheDependency();
					aggDepCustomer.Add( new SqlCacheDependency( "MobiCacheDependency", "tbl_App_Settings" ) );
					System.Web.HttpRuntime.Cache.Insert( "AppSettings", GetDataFromDBCache(), aggDepCustomer );
				}
			}
		}

		private static DataTable GetDataFromDBCache()
		{
			DataTable thedatatable = null;
			using (SqlConnection connection = new SqlConnection( ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString ))
			{
				connection.Open();
				SqlCommand cmd = new SqlCommand( "select [key],Value [value] from tbl_App_Settings AS [add] FOR XML AUTO, ROOT('appSettings')", connection );
				using (SqlDataAdapter dapt = new SqlDataAdapter( cmd ))
				{
					SqlCacheDependency dependency = new SqlCacheDependency( cmd );
					thedatatable = new DataTable();
					dapt.Fill( thedatatable );
				}
				connection.Close();
			}
			return thedatatable;
		}

		private static DataTable GetDataFromDB()
		{
			DataTable thedatatable = null;
			using (SqlConnection connection = new SqlConnection( ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString ))
			{
				connection.Open();
				SqlCommand cmd = new SqlCommand( "select [key],Value [value] from tbl_App_Settings AS [add] FOR XML AUTO, ROOT('appSettings')", connection );
				using (SqlDataAdapter dapt = new SqlDataAdapter( cmd ))
				{
					thedatatable = new DataTable();
					dapt.Fill( thedatatable );
				}
				connection.Close();
			}
			return thedatatable;
		}

		#endregion APP_SETTINGS
	}
}