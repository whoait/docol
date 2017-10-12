//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;

namespace Gulliver.DoCol.UtilityServices
{
	public class SettingsCommon
	{
		public static string XMLNS_NCX = "http://www.daisy.org/z3986/2005/ncx/";
		public static string XMLNS_OPF = "http://www.idpf.org/2007/opf";
		public static string XMLNS_DC = "http://purl.org/dc/elements/1.1/";

		private static SettingsData settingsData = new SettingsData();

		protected static string FilePath
		{
			set
			{
				Data.FilePath = value;
			}
		}

		protected static SettingsData Data
		{
			get
			{
				return settingsData;
			}
		}

		/// <summary>
		/// constructor
		/// </summary>
		static SettingsCommon()
		{
			Load();
		}

		/// <summary>
		/// Can call externally when needed
		/// </summary>
		public static void Load()
		{
			settingsData.FilePath = AppDomain.CurrentDomain.BaseDirectory + "AppSettings.config";
			settingsData.Load();

			if (settingsData.IsLoadComplete)
			{
				//load key data
			}
		}

		#region Setting Properties

		public static bool IsDebugMode
		{
			get
			{
				return Data.GetInt( "IsDebugMode" ) == 1;
			}
		}

		public static bool DeleteLog
		{
			get
			{
				return Data.GetInt( "DeleteLog" ) == 1;
			}
		}

		public static string MailServer
		{
			get
			{
				return Data.Get( "MailServer" );
			}
		}

		public static string MailFrom
		{
			get
			{
				return Data.Get( "MailFrom" );
			}
		}

		/* FPT TuNM3 Add Mail Server config Start.*/

		public static string MailUserName
		{
			get
			{
				return Data.Get( "MailUserName" );
			}
		}

		public static string MailPassword
		{
			get
			{
				return Data.Get( "MailPassword" );
			}
		}

		public static bool MailIsProxyRequired
		{
			get
			{
				return Data.GetInt( "MailIsProxyRequired" ) == 1;
			}
		}

		public static int MailPort
		{
			get
			{
				return Data.GetInt( "MailPort" );
			}
		}

		/* FPT TuNM3 Add Mail Server config End.*/

		public static string MailDiagnostics
		{
			get
			{
				return Data.Get( "MailDiagnostics" );
			}
		}

		public static string MailDiagnosticsSubject
		{
			get
			{
				return Data.Get( "AdminMailDiagnosticsSubject" );
			}
		}

		public static int CacheDurationInMinute
		{
			get
			{
				int time = Data.GetInt( "CacheDurationInMinute" );
				if (time < 0)
				{
					time = 0;
				}
				return time;
			}
		}

		public static int ErrorMailIntervalInMinute
		{
			get
			{
				int time = Data.GetInt( "ErrorMailIntervalInMinute" );
				if (time <= 10)
				{
					time = 120;
				}
				return time;
			}
		}

		public static string MailAdmin
		{
			get
			{
				return Data.Get( "MailAdmin" );
			}
		}

		public static string MailToSupport
		{
			get
			{
				return Data.Get( "MailToSupport" );
			}
		}

		public static int WeeklyBackDays
		{
			get
			{
				int WeeklyBackDays = 0;

				WeeklyBackDays = Data.GetInt( "WeeklyBackDays" );

				return WeeklyBackDays;
			}
		}

		public static int BackYear
		{
			get
			{
				int _backyear = 0;

				_backyear = Data.GetInt( "BackYear" );

				return _backyear;
			}
		}

		/// <summary>
		/// Massive Upload FTP Directory
		/// </summary>
		public static string MU_FTPDirectory
		{
			get
			{
				return Data.Get( "MU_FTPDirectory" );
			}
		}

		private static string CommonContentRepositoryRootPath
		{
			get
			{
				return Data.Get( "CommonContentRepositoryRootPath" );
			}
		}

		private static string CommonSFProductBasePath
		{
			get
			{
				return CommonContentRepositoryRootPath + @"SFProducts\";
			}
		}

		private static string CommonDEVProductBasePath
		{
			get
			{
				return CommonContentRepositoryRootPath + @"DevProducts\";
			}
		}

		#region StoreFrontID

		/// <summary>
		/// Gets the store front ID.
		/// </summary>
		public static int StoreFrontID
		{
			get
			{
				int storeFrontID = 0;
				storeFrontID = Data.GetInt( "StoreFrontID" );
				if (storeFrontID == 0)
				{
					if (!ReferenceEquals( System.Web.HttpContext.Current.Session["StoreFrontID"], null ))
					{
						int.TryParse( System.Web.HttpContext.Current.Session["StoreFrontID"].ToString(), out storeFrontID );
					}
				}
				return storeFrontID;
				//return 4;
			}
		}

		#endregion StoreFrontID

		#region [ StoreFront Product Common ]

		public static string SFProductRootPathCommon
		{
			get
			{
				return CommonSFProductBasePath + "StoreFront_" + StoreFrontID.ToString() + @"\";
			}
		}

		public static string SFProductFilePathCommon
		{
			get
			{
				return SFProductRootPathCommon + @"Files\";
			}
		}

		public static string SFProductSampleFilePathCommon
		{
			get
			{
				return SFProductFilePathCommon + @"Samples\";
			}
		}

		public static string SFProductLicensePathCommon
		{
			get
			{
				return SFProductRootPathCommon + @"Licenses\";
			}
		}

		public static string SFProductImageRootPathCommon
		{
			get
			{
				return SFProductRootPathCommon + @"Images\";
			}
		}

		public static string SFProductWAPImagePathCommon
		{
			get
			{
				return SFProductImageRootPathCommon + @"WapImages\";
			}
		}

		public static string SFProductWebImagePathCommon
		{
			get
			{
				return SFProductImageRootPathCommon + @"WebImages\";
			}
		}

		#endregion [ StoreFront Product Common ]

		#region DeveloperPortalID

		/// <summary>
		/// Gets the Developer Portal ID.
		/// </summary>
		public static int DevPortalID
		{
			get
			{
				int devPortalID = 0;
				devPortalID = Data.GetInt( "DevPortalID" );
				if (devPortalID == 0)
				{
					if (!ReferenceEquals( System.Web.HttpContext.Current.Session["DevPortalID"], null ))
					{
						int.TryParse( System.Web.HttpContext.Current.Session["DevPortalID"].ToString(), out devPortalID );
					}
				}

				return devPortalID;
			}
		}

		#endregion DeveloperPortalID

		#region [ Developer Product Common ]

		public static string DEVProductRootPathCommon
		{
			get
			{
				return CommonDEVProductBasePath + "DevPortal_" + DevPortalID.ToString() + @"\";
			}
		}

		public static string DEVProductExcelPathCommon
		{
			get
			{
				return DEVProductRootPathCommon + @"Excel\";
			}
		}

		public static string DEVProductDailyReportPathCommon
		{
			get
			{
				return DEVProductExcelPathCommon + @"DailyReport\";
			}
		}

		public static string DEVProductMonthlyReportPathCommon
		{
			get
			{
				return DEVProductExcelPathCommon + @"MonthlyReport\";
			}
		}

		public static string DEVProductWeeklyReportPathCommon
		{
			get
			{
				return DEVProductExcelPathCommon + @"WeeklyReport\";
			}
		}

		public static string DEVProductFilesPathCommon
		{
			get
			{
				return DEVProductRootPathCommon + @"ProductFiles\";
			}
		}

		public static string DEVProductContentPathCommon
		{
			get
			{
				return DEVProductFilesPathCommon + @"Content\";
			}
		}

		public static string DEVProductExtractPathCommon
		{
			get
			{
				return DEVProductFilesPathCommon + @"Extract\";
			}
		}

		public static string DEVProductLicenseDocumentPathCommon
		{
			get
			{
				return DEVProductFilesPathCommon + @"LicenseDocument\";
			}
		}

		public static string DEVProductTempPathCommon
		{
			get
			{
				return DEVProductFilesPathCommon + @"Temp\";
			}
		}

		public static string DEVProductWapImagesPathCommon
		{
			get
			{
				return DEVProductFilesPathCommon + @"Wap\";
			}
		}

		public static string DEVProductWebImagesPathCommon
		{
			get
			{
				return DEVProductFilesPathCommon + @"Web\";
			}
		}

		#endregion [ Developer Product Common ]

		#region BSReaderURL

		public static string BSReaderURL
		{
			get
			{
				return Data.Get( "BSReaderURL" );
			}
		}

		#endregion BSReaderURL

		#region BSReaderRedirectURL

		public static string BSReaderRedirectURL
		{
			get
			{
				return Data.Get( "BSReaderRedirectURL" );
			}
		}

		#endregion BSReaderRedirectURL

		#region Bitway DownloadURL

		public static string BitwayDownloadURL
		{
			get
			{
				return Data.Get( "BitwayDownloadURL" );
			}
		}

		#endregion Bitway DownloadURL

		#region Bitway StoreID

		public static string BitwayStoreID
		{
			get
			{
				return Data.Get( "BitwayStoreID" );
			}
		}

		#endregion Bitway StoreID

		#region Bitway PurchaseReserveURL

		public static string BitwayPurchaseReserveURL
		{
			get
			{
				return Data.Get( "BitwayPurchaseReserveURL" );
			}
		}

		#endregion Bitway PurchaseReserveURL

		#region Bitway DownloadReserveURL

		public static string BitwayDownloadReserveURL
		{
			get
			{
				return Data.Get( "BitwayDownloadReserveURL" );
			}
		}

		#endregion Bitway DownloadReserveURL

		#region CelsysCDC DownloadURL

		public static string CelsysCdcDownloadURL
		{
			get
			{
				return Data.Get( "CelsysCdcDownloadURL" );
			}
		}

		#endregion CelsysCDC DownloadURL

		#region CelsysCDC Cert Life

		public static string CelsysCDCCertLife
		{
			get
			{
				return Data.Get( "CelsysCDCCertLife" );
			}
		}

		#endregion CelsysCDC Cert Life

		#region Celsys Crypt Key

		public static string CelsysCryptKey
		{
			get
			{
				return Data.Get( "CelsysCryptKey" );
			}
		}

		#endregion Celsys Crypt Key

		#endregion Setting Properties

		# region MobileBookJP

		public static string MobileBookJPCertLife
		{
			get
			{
				return Data.Get( "MobileBookJPCertLife" );
			}
		}

		public static string MobileBookJPCPIDDocomo
		{
			get
			{
				return Data.Get( "MobileBookJPCPIDDocomo" );
			}
		}

		public static string MobileBookJPCPIDAu
		{
			get
			{
				return Data.Get( "MobileBookJPCPIDAu" );
			}
		}

		public static string MobileBookJPCPIDOthers
		{
			get
			{
				return Data.Get( "MobileBookJPCPIDOthers" );
			}
		}

		public static string MobileBookJPDownloadURL
		{
			get
			{
				return Data.Get( "MobileBookJPDownloadURL" );
			}
		}

		public static string MobileBookJPCertPasswordOthers
		{
			get
			{
				return Data.Get( "MobileBookJPCertPasswordOthers" );
			}
		}

		public static string MobileBookJPCertPasswordDocomo
		{
			get
			{
				return Data.Get( "MobileBookJPCertPasswordDocomo" );
			}
		}

		public static string MobileBookJPCertPasswordAu
		{
			get
			{
				return Data.Get( "MobileBookJPCertPasswordAu" );
			}
		}

		public static string MobileBookJPDownloadReserveURL
		{
			get
			{
				return Data.Get( "MobileBookJPDownloadReserveURL" );
			}
		}

		public static string MobileBookJPSiteProviderID
		{
			get
			{
				return Data.Get( "MobileBookJPSiteProviderID" );
			}
		}

		public static string MobileBookJPDownloadType
		{
			get
			{
				return Data.Get( "MobileBookJPDownloadType" );
			}
		}

		public static string MobileBookJPReDownloadType
		{
			get
			{
				return Data.Get( "MobileBookJPReDownloadType" );
			}
		}

		public static string MobileBookJPSampleDownloadType
		{
			get
			{
				return Data.Get( "MobileBookJPSampleDownloadType" );
			}
		}

		public static string MobileBookJPDownloadPurchaseURL
		{
			get
			{
				return Data.Get( "MobileBookJPDownloadPurchaseURL" );
			}
		}

		public static string MobileBookJPSiteIDDocomo
		{
			get
			{
				return Data.Get( "MobileBookJPSiteIDDocomo" );
			}
		}

		public static string MobileBookJPSiteIDAu
		{
			get
			{
				return Data.Get( "MobileBookJPSiteIDAu" );
			}
		}

		public static string MobileBookJPSiteIDOthers
		{
			get
			{
				return Data.Get( "MobileBookJPSiteIDOthers" );
			}
		}

		#endregion

		public static string AU_FTPDirectory
		{
			get
			{
				return Data.Get( "AU_FTPDirectory" );
			}
		}
	}
}