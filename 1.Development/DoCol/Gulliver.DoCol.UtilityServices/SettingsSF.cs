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
	public class SettingsSF : SettingsCommon
	{
		static SettingsSF()
		{
			if (AppDomain.CurrentDomain.FriendlyName.Contains( ".exe" ))
			{
				SettingsCommon.FilePath = AppDomain.CurrentDomain.BaseDirectory + "AppSettings.config";
			}
			else
			{
				FilePath = string.Empty;
			}
		}

		public static string SFProductCommonPath
		{
			get
			{
				return Data.Get( "SFProductCommonPath" );
			}
		}

		public static string PagePersistDuration
		{
			get
			{
				return Data.Get( "PagePersistDuration" );
			}
		}

		#region SSLView

		/// <summary>
		/// Url to place the order form
		/// </summary>
		public static string SSLView
		{
			get
			{
				return Data.Get( "SSLView" );
			}
		}

		#endregion SSLView

		public static string MasterSystemSiteRootPath
		{
			get
			{
				return Data.Get( "MasterSystemSiteRootPath" );
			}
		}

		public static string SFSchedulerRootPath
		{
			get
			{
				return Data.Get( "SFSchedulerRootPath" );
			}
		}

		public static string EPubHandlerPath
		{
			get
			{
				return MasterSystemSiteRootPath + Data.Get( "EPubHandlerPath" );
			}
		}

		public static string DownloadHandlerPath
		{
			get
			{
				return MasterSystemSiteRootPath + Data.Get( "DownloadHandlerPath" );
			}
		}

		public static string BBResourcePath
		{
			get
			{
				return MasterSystemSiteRootPath + Data.Get( "BBResourcePath" );
			}
		}

		public static string SymResourcePath
		{
			get
			{
				return MasterSystemSiteRootPath + Data.Get( "SymResourcePath" );
			}
		}

		#region MassiveUploadExe

		private static string _massiveUploadExe = string.Empty;

		/// <summary>
		/// EPubPath
		/// </summary>
		public static string MassiveUploadExe
		{
			get
			{
				return SFSchedulerRootPath + "MassiveUpload/" + "Forside.ConsoleMonitor.exe";
			}
		}

		#endregion MassiveUploadExe

		#region CouponMailerExe

		private static string _couponMailerExe = string.Empty;

		/// <summary>
		/// Gets the coupon mailer exe.
		/// </summary>
		public static string CouponMailerExe
		{
			get
			{
				return SFSchedulerRootPath + "CouponMailer/" + "Forside.ConsoleMonitor.exe";
			}
		}

		#endregion CouponMailerExe

		/// <summary>
		/// Gets or sets the site URL.
		/// </summary>
		/// <value>The site URL.</value>
		public static string SiteURL
		{
			get { return Data.Get( "SiteUIURL" ); }
		}

		#region StoreFrontName

		/// <summary>
		/// Gets the name of the store front.
		/// </summary>
		/// <value>
		/// The name of the store front.
		/// </value>
		public static string StoreFrontName
		{
			get
			{
				return Data.Get( "StoreFrontName" );
			}
		}

		#endregion StoreFrontName

		#region StoreFrontCountry

		/// <summary>
		/// Gets the country code of the store front.
		/// </summary>
		public static string StoreFrontCountry
		{
			get
			{
				return Data.Get( "StoreFrontCountry" );
			}
		}

		#endregion StoreFrontCountry

		/// <summary>
		/// Gets a value indicating whether [SF point system].
		/// </summary>
		/// <value>
		///   <c>true</c> if [SF point system]; otherwise, <c>false</c>.
		/// </value>
		public static bool SFPointSystem
		{
			get
			{
				return Data.GetBool( "SFPointSystem" );
			}
		}

		/// <summary>
		/// Gets a value indicating whether [SFPC viewer books].
		/// </summary>
		/// <value>
		///   <c>true</c> if [SFPC viewer books]; otherwise, <c>false</c>.
		/// </value>
		public static bool SFPCViewerBooks
		{
			get
			{
				return Data.GetBool( "SFPCViewerBooks" );
			}
		}

		public static string SFProductRootPath
		{
			get
			{
				return Data.Get( "SFProductPath" ) + "StoreFront_" + StoreFrontID.ToString() + @"\";
			}
		}

		public static string SFProductLicensePath
		{
			get
			{
				return SFProductRootPath + @"Licenses\";
			}
		}

		public static string SFProductImageRootPath
		{
			get
			{
				return SFProductRootPath + @"Images\";
			}
		}

		public static string SFProductFilePath
		{
			get
			{
				return SFProductRootPath + @"Files\";
			}
		}

		public static string SFProductSampleFilePath
		{
			get
			{
				return SFProductFilePath + @"Samples\";
			}
		}

		public static string SFProductWAPImagePath
		{
			get
			{
				return SFProductImageRootPath + @"WapImages\";
			}
		}

		public static string SFProductWebImagePath
		{
			get
			{
				return SFProductImageRootPath + @"WebImages\";
			}
		}

		#region ShowAllyouCanRead

		public static bool ShowBookMate
		{
			get
			{
				return Data.GetBool( "ShowBookMate" );
			}
		}

		#endregion ShowAllyouCanRead

		#region ShowBookShelfRead

		public static bool ShowBookShelfRead
		{
			get
			{
				return Data.GetBool( "ShowBookShelfRead" );
			}
		}

		#endregion ShowBookShelfRead

		#region ShowBango

		public static bool ShowBango
		{
			get
			{
				return Data.GetBool( "ShowBango" );
			}
		}

		#endregion ShowBango

		#region ShowBoku Boku_Integation

		public static bool ShowBoku
		{
			get
			{
				return Data.GetBool( "ShowBoku" );
			}
		}

		#endregion ShowBoku Boku_Integation

		#region ShowAuPayment

		public static bool ShowAuPayment
		{
			get
			{
				return Data.GetBool( "ShowAuPayment" );
			}
		}

		#endregion ShowAuPayment

		//wakin

		#region ShowAlipay

		public static bool ShowAlipay
		{
			get
			{
				return Data.GetBool( "ShowAlipay" );
			}
		}

		public static string AlipayPartnerID
		{
			get
			{
				return Data.Get( "AlipayPartnerID" );
			}
		}

		public static string AlipaySecurityCode
		{
			get
			{
				return Data.Get( "AlipaySecurityCode" );
			}
		}

		public static string AlipayBuyURL
		{
			get
			{
				return Data.Get( "AlipayBuyURL" );
			}
		}

		public static string AlipayReturnURL
		{
			get
			{
				return Data.Get( "AlipayReturnURL" );
			}
		}

		public static string AlipayNotifyURL
		{
			get
			{
				return Data.Get( "AlipayNotifyURL" );
			}
		}

		public static string AlipayProReturnURL
		{
			get
			{
				return Data.Get( "AlipayProReturnURL" );
			}
		}

		public static string AlipayProNotifyURL
		{
			get
			{
				return Data.Get( "AlipayProNotifyURL" );
			}
		}

		public static int AlipayCurrencyID
		{
			get
			{
				return Data.GetInt( "AlipayCurrencyID" );
			}
		}

		public static string AlipayCurrencyString
		{
			get
			{
				return Data.Get( "AlipayCurrencyString" );
			}
		}

		#endregion ShowAlipay

		#region IsVIBOSystem

		public static bool IsVIBOSystem
		{
			get
			{
				return Data.GetBool( "IsVIBOSystem" );
			}
		}

		#endregion IsVIBOSystem

		#region ShowPayAsYouGo

		public static bool ShowPayAsYouGo
		{
			get
			{
				return Data.GetBool( "ShowPayAsYouGo" );
			}
		}

		#endregion ShowPayAsYouGo

		#region ShowSalesTax

		public static bool ShowSalesTax
		{
			get
			{
				return Data.GetBool( "ShowSalesTax" );
			}
		}

		#endregion ShowSalesTax

		#region ShowCybersource

		public static bool ShowCybersource
		{
			get
			{
				return Data.GetBool( "ShowCybersource" );
			}
		}

		#endregion ShowCybersource

		#region ShowVibo

		public static bool ShowVibo
		{
			get
			{
				return Data.GetBool( "ShowVibo" );
			}
		}

		#endregion ShowVibo

		#region ViboFirstURL

		public static string ViboFirstURL
		{
			get
			{
				return Data.Get( "ViboFirstURL" );
			}
		}

		#endregion ViboFirstURL

		#region ViboRedirectURL

		public static string ViboRedirectURL
		{
			get
			{
				return Data.Get( "ViboRedirectURL" );
			}
		}

		#endregion ViboRedirectURL

		#region IsViboDebug

		public static bool IsViboDebug
		{
			get
			{
				return Data.GetBool( "IsViboDebug" );
			}
		}

		#endregion IsViboDebug

		#region IsViboTest

		public static bool IsViboTest
		{
			get
			{
				return Data.GetBool( "IsViboTest" );
			}
		}

		#endregion IsViboTest

		#region ShowCurrencyOption

		public static bool ShowCurrencyOption
		{
			get
			{
				return Data.GetBool( "ShowCurrencyOption" );
			}
		}

		#endregion ShowCurrencyOption

		#region ShowMonthlyPlan

		public static bool ShowMonthlyPlan
		{
			get
			{
				return Data.GetBool( "ShowMonthlyPlan" );
			}
		}

		#endregion ShowMonthlyPlan

		#region ShowIdea

		public static bool ShowIdea
		{
			get
			{
				return Data.GetBool( "ShowIdea" );
			}
		}

		#endregion ShowIdea

		#region AggregatorAppPath

		public static bool AggregatorAppPath
		{
			get
			{
				return Data.GetBool( "AggregatorAppPath" );
			}
		}

		#endregion AggregatorAppPath

		#region ShowStorage

		public static bool ShowStorage
		{
			get
			{
				return Data.GetBool( "ShowStorage" );
			}
		}

		#endregion ShowStorage

		#region ShowDocomo

		public static bool ShowDocomo
		{
			get
			{
				return Data.GetBool( "ShowDocomo" );
			}
		}

		#endregion ShowDocomo

		#region IsDocomoLive

		public static bool IsDocomoLive
		{
			get
			{
				return Data.GetBool( "IsDocomoLive" );
			}
		}

		#endregion IsDocomoLive

		#region DocomoShopCode

		public static string DocomoShopCode
		{
			get
			{
				return Data.Get( "DocomoShopCode" );
			}
		}

		#endregion DocomoShopCode

		#region ShowDevPortalUtility

		public static bool ShowDevPortalUtility
		{
			get
			{
				return Data.GetBool( "ShowDevPortalUtility" );
			}
		}

		#endregion ShowDevPortalUtility

		#region IsAuLive

		public static bool IsAuLive
		{
			get
			{
				return Data.GetBool( "IsAuLive" );
			}
		}

		#endregion IsAuLive

		#region AuOpenIdConsumerPublic

		public static string AuOpenIdConsumerPublic
		{
			get
			{
				return Data.Get( "AuOpenIdConsumerPublic" );
			}
		}

		#endregion AuOpenIdConsumerPublic

		#region AuOpenIdRealm

		public static string AuOpenIdRealm
		{
			get
			{
				return Data.Get( "AuOpenIdRealm" );
			}
		}

		#endregion AuOpenIdRealm

		#region AuShopId

		public static string AuShopId
		{
			get
			{
				return Data.Get( "AuShopId" );
			}
		}

		#endregion AuShopId

		#region AuServiceId

		public static string AuServiceId
		{
			get
			{
				return Data.Get( "AuServiceId" );
			}
		}

		#endregion AuServiceId

		#region ShowCoda

		public static bool ShowCoda
		{
			get
			{
				return Data.GetBool( "ShowCoda" );
			}
		}

		#endregion ShowCoda

		public static long DEV_PToolMaxSize
		{
			get
			{
				return Data.GetLong( "DEV_PToolMaxSize" );
			}
		}

		public static string DEV_PToolDirectory
		{
			get
			{
				return Data.Get( "DEV_PToolDirectory" );
			}
		}

		#region ProfilePhoto File Size

		public static string StoragePerPhotoFileSize
		{
			get
			{
				return Data.Get( "ProfilePhotoFileSize" );
			}
		}

		#endregion ProfilePhoto File Size

		public static bool ShowFreeGiveAway
		{
			get
			{
				return Data.GetBool( "ShowFreeGiveAway" );
			}
		}

		#region SSOAuthKey

		public static string SSOAuthKey
		{
			get
			{
				return Data.Get( "SSOAuthKey" );
			}
		}

		#endregion SSOAuthKey

		public static bool IsEmailOptional
		{
			get
			{
				return Data.GetBool( "IsEmailOptional" );
			}
		}

		#region Cerquit

		/// <summary>
		/// Gets the value for key GetEnableSmartSMSC
		/// </summary>
		/// <returns></returns>
		public static bool GetEnableSmartSMSC()
		{
			return Data.GetBool( "EnableSmartSMSC" );
		}

		/// <summary>
		/// Gets the value for the key SMART_MPS_IP
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_MPS_IP()
		{
			return Data.Get( "SMART_MPS_IP" );
		}

		/// <summary>
		/// Gets the value for the key SMART_MPS_PORT_ADDRESS
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_MPS_PORT_ADDRESS()
		{
			return Data.Get( "SMART_MPS_PORT_ADDRESS" );
		}

		/// <summary>
		/// Gets the value for the key SMART_MPS_ABSOLUTE_URL
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_MPS_ABSOLUTE_URL()
		{
			return Data.Get( "SMART_MPS_ABSOLUTE_URL" );
		}

		/// <summary>
		/// Gets the value for the key SMART_PUSH_IP
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_PUSH_IP()
		{
			return Data.Get( "SMART_PUSH_IP" );
		}

		/// <summary>
		/// Gets the value for the key SMART_PUSH_PORT_ADDRESS
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_PUSH_PORT_ADDRESS()
		{
			return Data.Get( "SMART_PUSH_PORT_ADDRESS" );
		}

		/// <summary>
		/// Gets the value for the key SMART_PUSH_ABSOLUTE_URL
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_PUSH_ABSOLUTE_URL()
		{
			return Data.Get( "SMART_PUSH_ABSOLUTE_URL" );
		}

		/// <summary>
		/// Gets the value for the key SMART_SETTING_USERNAME
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_SETTING_USERNAME()
		{
			return Data.Get( "SMART_SETTING_USERNAME" );
		}

		/// <summary>
		/// Gets the value for the key SMART_SETTING_PASSWORD
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_SETTING_PASSWORD()
		{
			return Data.Get( "SMART_SETTING_PASSWORD" );
		}

		/// <summary>
		/// Gets the value for the key SMART_SETTING_ACCESS_CODE
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_SETTING_ACCESS_CODE()
		{
			return Data.Get( "SMART_SETTING_ACCESS_CODE" );
		}

		/// <summary>
		/// Gets the value for the key SMART_SETTING_SERVICE_ID
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_SETTING_SERVICE_ID()
		{
			return Data.Get( "SMART_SETTING_SERVICE_ID" );
		}

		/// <summary>
		/// Gets the value for the key SMART_SETTING_TARIFF_CLASS
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_SETTING_TARIFF_CLASS()
		{
			return Data.Get( "SMART_SETTING_TARIFF_CLASS" );
		}

		/// <summary>
		/// Gets the value for the key SMART_USE_SSL
		/// </summary>
		/// <returns></returns>
		public static string GetSMART_USE_SSL()
		{
			return Data.Get( "SMART_USE_SSL" );
		}

		#endregion Cerquit

		#region CelsysCDC Cert CPID

		public static string CelsysCdcCertCPID
		{
			get
			{
				return Data.Get( "CelsysCdcCertCPID" );
			}
		}

		#endregion CelsysCDC Cert CPID

		#region CelsysCDC Cert Password

		public static string CelsysCdcCertPassword
		{
			get
			{
				return Data.Get( "CelsysCdcCertPassword" );
			}
		}

		#endregion CelsysCDC Cert Password

		#region Contact Us

		public static string ContactUs_MailTo
		{
			get
			{
				return Data.Get( "ContactUs_MailTo" );
			}
		}

		#endregion Contact Us

		#region AU Related Key

		public static string AuServer
		{
			get
			{
				return Data.Get( "AuServer" );
			}
		}

		public static string AuRealm
		{
			get
			{
				return Data.Get( "AuRealm" );
			}
		}

		public static string AuMemberId
		{
			get
			{
				return Data.Get( "AuMemberId" );
			}
		}

		public static string AuServiceIdContinuousCharge
		{
			get
			{
				return Data.Get( "AuServiceIdContinuousCharge" );
			}
		}

		public static string AuServiceIdCharge
		{
			get
			{
				return Data.Get( "AuServiceIdCharge" );
			}
		}

		public static string AuSecureKey
		{
			get
			{
				return Data.Get( "AuSecureKey" );
			}
		}

		public static string AuAccountTimingKbn
		{
			get
			{
				return Data.Get( "AuAccountTimingKbn" );
			}
		}

		public static string AuAccountTiming
		{
			get
			{
				return Data.Get( "AuAccountTiming" );
			}
		}

		public static string AuOKUrlRegistration
		{
			get
			{
				return Data.Get( "AuOKUrlRegistration" );
			}
		}

		public static string AuNGUrlRegistration
		{
			get
			{
				return Data.Get( "AuNGUrlRegistration" );
			}
		}

		public static string AuOKUrlPurchaseUnitPlan
		{
			get
			{
				return Data.Get( "AuOKUrlPurchaseUnitPlan" );
			}
		}

		public static string AuNGUrlPurchaseUnitPlan
		{
			get
			{
				return Data.Get( "AuNGUrlPurchaseUnitPlan" );
			}
		}

		public static string AuOKUrlPurchaseProduct
		{
			get
			{
				return Data.Get( "AuOKUrlPurchaseProduct" );
			}
		}

		public static string AuNGUrlPurchaseProductCancel
		{
			get
			{
				return Data.Get( "AuNGUrlPurchaseProductCancel" );
			}
		}

		public static string AuLogOnURL
		{
			get
			{
				return Data.Get( "AuLogOnURL" );
			}
		}

		public static string AuLogOnURLMobi
		{
			get
			{
				return Data.Get( "AuLogOnURLMobi" );
			}
		}

		public static string AuOKUrlPurchaseSubscriptionPlan
		{
			get
			{
				return Data.Get( "AuOKUrlPurchaseSubscriptionPlan" );
			}
		}

		public static string AuNGUrlPurchaseSubscriptionPlan
		{
			get
			{
				return Data.Get( "AuNGUrlPurchaseSubscriptionPlan" );
			}
		}

		#endregion AU Related Key

		#region Rakuten

		#region ShowRakuten

		public static bool ShowRakuten
		{
			get
			{
				return Data.GetBool( "ShowRakuten" );
			}
		}

		#endregion ShowRakuten

		#region RakutenReturnToURL

		public static string RakutenReturnToURL
		{
			get
			{
				return Data.Get( "RakutenReturnToURL" );
			}
		}

		#endregion RakutenReturnToURL

		#region RakutenLoginImage

		public static string RakutenLoginImage
		{
			get
			{
				return Data.Get( "RakutenLoginImage" );
			}
		}

		#endregion RakutenLoginImage

		#region RakutenLogoImage

		public static string RakutenLogoImage
		{
			get
			{
				return Data.Get( "RakutenLogoImage" );
			}
		}

		#endregion RakutenLogoImage

		#region RakutenServiceId

		public static string RakutenServiceId
		{
			get
			{
				return Data.Get( "RakutenServiceId" );
			}
		}

		#endregion RakutenServiceId

		#region RakutenServiceIdForContinuousCharge

		public static string RakutenServiceIdForContinuousCharge
		{
			get
			{
				return Data.Get( "RakutenServiceIdForContinuousCharge" );
			}
		}

		#endregion RakutenServiceIdForContinuousCharge

		#region RakutenAccessKey

		public static string RakutenAccessKey
		{
			get
			{
				return Data.Get( "RakutenAccessKey" );
			}
		}

		#endregion RakutenAccessKey

		#region RakutenAccessKeyForContinuousCharge

		public static string RakutenAccessKeyForContinuousCharge
		{
			get
			{
				return Data.Get( "RakutenAccessKeyForContinuousCharge" );
			}
		}

		#endregion RakutenAccessKeyForContinuousCharge

		#region RakutenTestMode

		public static string RakutenTestMode
		{
			get
			{
				return Data.Get( "RakutenTestMode" );
			}
		}

		#endregion RakutenTestMode

		#endregion Rakuten

		#region ShowAggregatorRental

		public static bool ShowAggregatorRental
		{
			get
			{
				return Data.GetBool( "ShowAggregatorRental" );
			}
		}

		#endregion ShowAggregatorRental

		#region ShowAUCRPlans

		public static bool ShowAUCRPlans
		{
			get
			{
				return Data.GetBool( "ShowAUCRPlans" );
			}
		}

		#endregion ShowAUCRPlans

		#region HidePriceDetails

		public static bool HidePriceDetails
		{
			get
			{
				return Data.GetBool( "HidePriceDetails" );
			}
		}

		#endregion HidePriceDetails

		#region HideReadingAllownceDetails

		public static bool HideReadingAllownceDetails
		{
			get
			{
				return Data.GetBool( "HideReadingAllownceDetails" );
			}
		}

		#endregion HideReadingAllownceDetails

		#region VIPPlanCode

		public static string VIPPlanCode
		{
			get
			{
				return Data.Get( "VIPPlanCode" );
			}
		}

		#endregion VIPPlanCode

		#region PremiumPlanCode

		public static string PremiumPlanCode
		{
			get
			{
				return Data.Get( "PremiumPlanCode" );
			}
		}

		#endregion PremiumPlanCode

		#region VIPPlanCode for IMobi-Book

		public static string IMobiVIPPlanCode
		{
			get
			{
				return Data.Get( "IMobiVIPPlanCode" );
			}
		}

		#endregion VIPPlanCode for IMobi-Book

		#region PremiumPlanCode for IMobi-Book

		public static string IMobiPremiumPlanCode
		{
			get
			{
				return Data.Get( "IMobiPremiumPlanCode" );
			}
		}

		#endregion PremiumPlanCode for IMobi-Book

		#region HideDeviceIcons

		public static bool HideDeviceIcons
		{
			get
			{
				return Data.GetBool( "HideDeviceIcons" );
			}
		}

		#endregion HideDeviceIcons

		#region HideServiceIcons

		public static bool HideServiceIcons
		{
			get
			{
				return Data.GetBool( "HideServiceIcons" );
			}
		}

		#endregion HideServiceIcons

		#region HidePointDetails

		public static bool HidePointDetails
		{
			get
			{
				return Data.GetBool( "HidePointDetails" );
			}
		}

		#endregion HidePointDetails

		#region ShowStoreApp

		public static bool ShowStoreApp
		{
			get
			{
				return Data.GetBool( "ShowStoreApp" );
			}
		}

		#endregion ShowStoreApp

		#region ShowNonMemberDownload

		public static bool ShowNonMemberDownload
		{
			get
			{
				return Data.GetBool( "ShowNonMemberDownload" );
			}
		}

		#endregion ShowNonMemberDownload

		#region XBOOK

		public static string XBOOKDownloadReserveURL
		{
			get
			{
				return Data.Get( "XBOOKDownloadReserveURL" );
			}
		}

		public static string XBOOKDownloadPurchaseURL
		{
			get
			{
				return Data.Get( "XBOOKDownloadPurchaseURL" );
			}
		}

		public static string XBOOKBookstoreID
		{
			get
			{
				return Data.Get( "XBOOKBookstoreID" );
			}
		}

		public static string XBOOKJPSiteIDOthers
		{
			get
			{
				return Data.Get( "XBOOKJPSiteIDOthers" );
			}
		}

		public static string XBOOKJPSiteIDDocomo
		{
			get
			{
				return Data.Get( "XBOOKJPSiteIDDocomo" );
			}
		}

		public static string XBOOKJPSiteIDAu
		{
			get
			{
				return Data.Get( "XBOOKJPSiteIDAu" );
			}
		}

		public static string XBOOKNoOfSeparateVolume
		{
			get
			{
				return Data.Get( "XBOOKNoOfSeparateVolume" );
			}
		}

		public static string XBOOKDownloadType
		{
			get
			{
				return Data.Get( "XBOOKDownloadType" );
			}
		}

		public static string XBOOKReDownloadType
		{
			get
			{
				return Data.Get( "XBOOKReDownloadType" );
			}
		}

		public static string XBOOKSampleDownloadType
		{
			get
			{
				return Data.Get( "XBOOKSampleDownloadType" );
			}
		}

		public static string XBOOKDownloadInfoURL
		{
			get
			{
				return Data.Get( "XBOOKDownloadInfoURL" );
			}
		}

		public static string XBOOKDownloadURL
		{
			get
			{
				return Data.Get( "XBOOKDownloadURL" );
			}
		}

		#endregion XBOOK

		#region FileUploadPhysical

		public static string FileUploadPhysical
		{
			get
			{
				return Data.Get( "FileuploadPhysical" );
			}
		}

		#endregion FileUploadPhysical

		#region Google Checkout

		/// <summary>
		/// Gets the settings if GoogleCheckout is enabled.
		/// </summary>
		public static bool IsGoogleCheckoutEnabled
		{
			get { return Data.GetBool( "EnableGoogleCheckout" ); }
		}

		/// <summary>
		/// Gets the continue shopping URL.
		/// </summary>
		public static string ContinueShoppingUrl
		{
			get
			{
				return Data.Get( "GCheckoutContinueShoppingUrl" );
			}
		}

		public static string GoogleMerchantID
		{
			get
			{
				return Data.Get( "GoogleMerchantID" );
			}
		}

		public static string GoogleMerchantKey
		{
			get
			{
				return Data.Get( "GoogleMerchantKey" );
			}
		}

		public static string GoogleEnvironment
		{
			get
			{
				return Data.Get( "GoogleEnvironment" );
			}
		}

		#endregion Google Checkout

		public static string GiftPostURL
		{
			get
			{
				return Data.Get( "GiftPostURL" );
			}
		}

		public static string SendPushMessageUrl
		{
			get
			{
				return Data.Get( "SendPushMessageUrl" );
			}
		}

		public static int MaxGiftDownloadCount
		{
			get
			{
				return Data.GetInt( "MaxGiftDownloadCount" );
			}
		}

		public static string MSContentRepositoryRootPath
		{
			get
			{
				return Data.Get( "MSContentRepositoryRootPath" );
			}
		}

		public static string MSNewFSDContent
		{
			get
			{
				return Data.Get( "MSNewFSDContent" );
			}
		}

		#region MappedNewProduct

		/// <summary>
		/// Default Country ID
		/// </summary>
		public static int MappedNewProduct
		{
			get
			{
				return Data.GetInt( "MappedNewProduct" );
			}
		}

		#endregion MappedNewProduct

		public static string FSDReleaseDate
		{
			get
			{
				return Data.Get( "FSDReleaseDate" );
			}
		}

		public static bool ShowDocomoSP
		{
			get
			{
				return Data.GetBool( "ShowDocomoSP" );
			}
		}

		public static bool ShowApplePayment
		{
			get
			{
				return Data.GetBool( "ShowApplePayment" );
			}
		}

		#region IMobibook Rental Book for Premium

		public static int IMobibookNoOfBookRent
		{
			get
			{
				return Data.GetInt( "IMobibookNoOfBookRent" );
			}
		}

		#endregion IMobibook Rental Book for Premium

		#region Docmo SP Payment Gateway

		public static string DocomoSPMonthlySiteID
		{
			get
			{
				return Data.Get( "DocomoSPMonthlySiteID" );
			}
		}

		public static string DocomoSPOneTimePaymentSiteID
		{
			get
			{
				return Data.Get( "DocomoSPOneTimePaymentSiteID" );
			}
		}

		public static string DcocomoSPPaymentURL
		{
			get
			{
				return Data.Get( "DcocomoSPPaymentURL" );
			}
		}

		public static string DcocomoSPPaymentSubscribeURL
		{
			get
			{
				return Data.Get( "DcocomoSPPaymentSubscribeURL" );
			}
		}

		public static string DcocomoSPPaymentChangeSubscriptionURL
		{
			get
			{
				return Data.Get( "DcocomoSPPaymentChangeSubscriptionURL" );
			}
		}

		public static string DcocomoSPPaymentCancelSubscriptionURL
		{
			get
			{
				return Data.Get( "DcocomoSPPaymentCancelSubscriptionURL" );
			}
		}

		public static string DocomoSPAuthURL
		{
			get
			{
				return Data.Get( "DocomoSPAuthURL" );
			}
		}

		public static string DocomoSPAuthReturnURL
		{
			get
			{
				return Data.Get( "DocomoSPAuthReturnURL" );
			}
		}

		public static bool EnableDeviceAccessLimitation
		{
			get
			{
				return Data.GetBool( "EnableDeviceAccessLimitation" );
			}
		}

		#endregion Docmo SP Payment Gateway

		#region EnableOpenIDLoginPopUp

		public static bool EnableOpenIDLoginPopUp
		{
			get
			{
				return Data.GetBool( "EnableOpenIDLoginPopUp" );
			}
		}

		#endregion EnableOpenIDLoginPopUp

		public static string WorldTimeEngineKey
		{
			get
			{
				return Data.Get( "WorldTimeEngineKey" );
			}
		}

		public static bool ShowNewUI
		{
			get
			{
				return Data.GetBool( "ShowNewUI" );
			}
		}

		/// <summary>
		/// Gets the true/false to use static point discount percentage.
		/// </summary>
		/// <value>
		/// true to use static point discount percentage value, false if
		/// not.
		/// </value>
		public static bool IsStaticPointDiscountPercentage
		{
			get { return Data.GetBool( "IsStaticPointDiscountPercentage" ); }
		}

		/// <summary>
		/// Gets the value for point discount percentage.
		/// </summary>
		/// <value>Point discount percentage.</value>
		public static string PointDiscountPercentage
		{
			get { return Data.Get( "PointDiscountPercentage" ); }
		}
	}
}