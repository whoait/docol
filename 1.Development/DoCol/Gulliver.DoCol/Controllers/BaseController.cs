namespace Gulliver.DoCol.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;
	using System.Web.SessionState;

	using Gulliver.DoCol.BusinessServices.Common;
	using Gulliver.DoCol.Constants;
	using Gulliver.DoCol.Constants.Security;
	using Gulliver.DoCol.DataAccess.Common;
	using Gulliver.DoCol.DataValidation;
	using Gulliver.DoCol.Entities;
	using Gulliver.DoCol.Entities.Common;
	using Gulliver.DoCol.Library.Sercurity;
	using Gulliver.DoCol.UtilityServices;

	/// <summary>
	/// Controller base
	/// </summary>
	public class BaseController : Controller
	{
		#region Varibles

		// Common entity model
		private CmnEntityModel cmnEntityModel = null;

		// Common tab entity model
		private CmnTabEntityModel cmnTabEntityModel = null;

		// </summary>
		private string tabId = null;

		#endregion Varibles

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseController"/> class.
		/// </summary>
		public BaseController()
			: base()
		{
			ViewBag.CoreCenter_ScreenID = this.GetType().Name.Replace( "Controller", string.Empty ).ToUpper();
		}

		#endregion Constructor

		#region Properties

		/// <summary>
		/// Gets common entity model.
		/// </summary>
		/// <value>
		/// The common entity model.
		/// </value>
		public CmnEntityModel CmnEntityModel
		{
			get
			{
				if (cmnEntityModel == null)
				{
					if (CacheUtil.GetCache<CmnEntityModel>( "CmnEntityModel" ) == null)
					{
						CacheUtil.SaveCache( "CmnEntityModel", new CmnEntityModel() );
					}
					cmnEntityModel = (CmnEntityModel)CacheUtil.GetCache<CmnEntityModel>( "CmnEntityModel" );
				}
				HttpContext.Items[CacheKeys.CmnEntityModel] = cmnEntityModel;
				return cmnEntityModel;
			}
		}

		/// <summary>
		/// Gets common tab entity model.
		/// </summary>
		/// <value>
		/// The common tab entity model.
		/// </value>
		public CmnTabEntityModel CmnTabEntityModel
		{
			get
			{
				if (cmnTabEntityModel != null && !string.IsNullOrEmpty( cmnTabEntityModel.TabID ))
				{
					if (this.GetCache<CmnTabEntityModel>( "CmnTabEntityModel" ) == null)
					{
						this.SaveCache( "CmnTabEntityModel", new CmnTabEntityModel() );
					}
					cmnTabEntityModel = (CmnTabEntityModel)this.GetCache<CmnTabEntityModel>( "CmnTabEntityModel" );
				}
				return cmnTabEntityModel;
			}
		}

		#endregion Properties

		#region Override method

		/// <summary>
		/// Called before the action method is invoked.
		/// </summary>
		/// <param name="filterContext">Information about the current request and action.</param>
		/// <exception cref="System.Exception">
		/// </exception>
		protected override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			// get SessionId from queryString
			string sessionId = filterContext.RequestContext.HttpContext.Request["SessionId"];
			if (!string.IsNullOrWhiteSpace( sessionId ))
			{
				var manager = new SessionIDManager();
				bool redirected, isAdded;
				manager.SaveSessionID( System.Web.HttpContext.Current, sessionId, out redirected, out isAdded );
			}

			string currentScreenId = (string)ViewBag.CoreCenter_ScreenID;
			string screenType = string.Empty;

			this.CmnEntityModel.CurrentScreenID = currentScreenId;

            if (string.IsNullOrEmpty(this.cmnEntityModel.UserName) && (currentScreenId != "DCW001" && currentScreenId != "DCW002"))
            {
                throw new Exception("GLV_SYS_LoginException");
            }

			this._ShowModelStateError();

			// Reset error
			this.CmnEntityModel.ErrorMsgCd = string.Empty;
			this.CmnEntityModel.ErrorMsgReplaceString = string.Empty;

			// Get TabId POST/GET
			if (filterContext.HttpContext.Request.HttpMethod == HttpMethod.Post.Method)
			{
				this.tabId = filterContext.HttpContext.Request.Form["hfldUniqueTabSession"];
			}
			else
			{
				this.tabId = filterContext.HttpContext.Request.QueryString["tabId"];
			}

			// Get TabId in AJAX Request
			if ((filterContext.HttpContext.Request.ContentType ?? string.Empty).Contains( "application/json" ))
			{
				string jsonPost = string.Empty;
				filterContext.HttpContext.Request.InputStream.Position = 0;
				using (var reader = new StreamReader( filterContext.HttpContext.Request.InputStream ))
				{
					jsonPost = reader.ReadToEnd();
				}

				if (!string.IsNullOrEmpty( jsonPost ))
				{
					var jsonPostData = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, object>>( jsonPost );
					this.tabId = (jsonPostData != null && jsonPostData.ContainsKey( "hfldUniqueTabSession" ) && jsonPostData["hfldUniqueTabSession"] != null)
									 ? Convert.ToString( jsonPostData["hfldUniqueTabSession"] )
									 : "";
				}
			}

			//if (string.IsNullOrEmpty( this.tabId ))
			//{
			//	return;
			//}

			// Save TabId and Screen Route
			this.cmnTabEntityModel = this.GetCmnTabEntityModel( this.tabId );
			this.cmnTabEntityModel.TabID = this.tabId;
			this.cmnTabEntityModel.CurrentScreenID = currentScreenId;

			if (this.cmnTabEntityModel.CurrentScreenID.Equals( "DCW001" ))
			{
				this.cmnTabEntityModel.ScreenRoute = string.Empty;
			}
			else if (this.cmnTabEntityModel.CurrentScreenID.Equals( "DCW002" ))
			{
				this.cmnTabEntityModel.ScreenRoute = "DCW002";
			}
			else if (!this.cmnTabEntityModel.ScreenRoute.Contains( currentScreenId ))
			{
				if (string.IsNullOrEmpty( this.cmnTabEntityModel.ScreenRoute ))
				{
					this.cmnTabEntityModel.ScreenRoute = currentScreenId;
				}
				else
				{
					this.cmnTabEntityModel.ScreenRoute += "," + currentScreenId;
				}
			}

			string[] screenList = this.cmnTabEntityModel.ScreenRoute.Split( ',' );
			if (screenList.Length > 1)
			{
				this.cmnTabEntityModel.ParrentScreenID = screenList[screenList.Length - 2];
			}

			this.SaveCache( "CmnTabEntityModel", this.cmnTabEntityModel );

			#region "Back"
			if (Request.UrlReferrer != null)
			{
				string currentScreenID = UriUtility.GetScreenIDFromURL( Request.Url.AbsoluteUri );
				string referrerScreenID = UriUtility.GetScreenIDFromURL( Request.UrlReferrer.AbsoluteUri );

				if (currentScreenID != referrerScreenID && !Request.Url.AbsoluteUri.Contains( "IsBack" ))
				{
					this.SaveCache( currentScreenID + ".BackURL", Request.UrlReferrer.AbsoluteUri );
				}
			}
			#endregion

			#region Detecting Refresh
			var cookie = this.GetCache<string>( "UrlCheckRefresh" );
			this.cmnTabEntityModel.IsRefreshed = filterContext.HttpContext.Request.Url != null && (cookie != null && cookie == filterContext.HttpContext.Request.Url.ToString());
			#endregion

			#region Current screen Id for common
			CacheUtil.SaveCache( "_CommonCurrentScreenId", currentScreenId );

			#endregion

            CacheUtil.SaveCache(CacheKeys.CmnEntityModel, cmnEntityModel);
		}

		protected override void OnActionExecuted( ActionExecutedContext actionExecutedContext )
		{
			if (!string.IsNullOrEmpty( this.CmnEntityModel.ErrorMsgCd ))
			{
				// Set error message for popup
				string typeMsg;
				string contentMsg;
				Utility.GetMessage(
					CmnEntityModel.ErrorMsgCd,
					CmnEntityModel.ErrorMsgReplaceString,
					out typeMsg,
					out contentMsg );

				this.ViewBag.TypeMessage = typeMsg;
				this.ViewBag.ErrorMessage = contentMsg;
			}

			#region Detecting refresh

			if (actionExecutedContext.HttpContext.Request.Url != null && actionExecutedContext.HttpContext.Request.IsAjaxRequest() == false)
			{
				this.SaveCache( "UrlCheckRefresh", actionExecutedContext.HttpContext.Request.Url.ToString() );
			}

			#endregion
		}

		/// <summary>
		/// Called when [exception].
		/// </summary>
		/// <param name="exceptionContext">The exception context.</param>
		protected override void OnException( ExceptionContext exceptionContext )
		{
			// TODO
			Log.SaveLogToFile( Log.LogLevel.ERROR,
									exceptionContext.Controller.ToString() + "." + exceptionContext.RouteData.Values["action"],
									this.CmnEntityModel.UserID,
									"",
									Request.UserHostAddress,
									exceptionContext.Exception.Message,
									exceptionContext.Exception.StackTrace );
			HttpCookie runTimeErrorMsg;
			if (exceptionContext.Exception.Message.Contains( "GLV_SYS_DBException" ))
			{
				CacheUtil.SaveCache( "GLV_SYS_Cache_RunTimeErrorMsg", Utility.GetMessage( MessageCd.E0002 ) );
			}
			else if (exceptionContext.Exception.Message.Contains( TextKey.PRE_EXCEPTION_MSG ))
			{
				CacheUtil.SaveCache( "GLV_SYS_Cache_RunTimeErrorMsg", exceptionContext.Exception.Message.Replace( TextKey.PRE_EXCEPTION_MSG, "" ) );
			}
			else
			{
				CacheUtil.SaveCache( "GLV_SYS_Cache_RunTimeErrorMsg", Utility.GetMessage( MessageCd.E0002 ) );
			}

			if (exceptionContext.Exception.Message.Contains( "GLV_SYS_RunTimeErrorMsg" ))
			{
				runTimeErrorMsg = new HttpCookie( "GLV_SYS_RunTimeErrorMsg", this.CmnEntityModel.CurrentScreenID );
				runTimeErrorMsg.Expires = DateTime.Now.AddMinutes( 120d );
				Response.Cookies.Add( runTimeErrorMsg );
			}

			base.OnException( exceptionContext );
		}

		#endregion Override method

		/// <summary>
		/// Back to Previous Screen
		/// </summary>
		/// <returns></returns>
		public ActionResult BackPreviousScreen()
		{
			string currentScreenID = UriUtility.GetScreenIDFromURL( Request.Url.AbsoluteUri );
			string returnURL = string.Empty;

			if (!string.IsNullOrEmpty( currentScreenID ))
			{
				returnURL = this.GetCache<string>( currentScreenID + ".BackURL" );

				if (!string.IsNullOrEmpty( returnURL ))
				{
					string referrerTabID = UriUtility.GetParamValueFromURL( returnURL, "tabId" );

					if (string.IsNullOrEmpty( referrerTabID ) && !string.IsNullOrEmpty( this.tabId ))
					{
						returnURL += returnURL.Contains( "?" ) ? "&tabId=" + this.tabId : "?tabId=" + this.tabId;
					}
					else
					{
						returnURL = !string.IsNullOrEmpty( this.tabId ) && referrerTabID != this.tabId ? returnURL.Replace( referrerTabID, this.tabId ) : returnURL;
					}

					returnURL = returnURL.Contains( "?" ) ? returnURL : returnURL + "?IsBack=1";
					returnURL = returnURL.Contains( "IsBack" ) ? returnURL : returnURL + "&IsBack=1";

					this.SaveCache( currentScreenID + ".BackURL", null );
					return base.Redirect( returnURL );
				}
			}

			this.SaveCache( currentScreenID + ".BackURL", null );
            return base.Redirect(string.IsNullOrEmpty(returnURL) ? Url.Action("DCW002Menu", "DCW002", new { Area = "DCW" }) : returnURL);
        }

		#region Cache

		/// <summary>
		/// Saves the cache.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void SaveCache( string key, object value )
		{
			CacheUtil.SaveCache( GetCacheKey( key ), value );
		}

		/// <summary>
		/// Gets the cache.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public T GetCache<T>( string key, object defaultValue = null )
		{
			return CacheUtil.GetCache<T>( GetCacheKey( key ), defaultValue );
		}

		/// <summary>
		/// Removes the cache.
		/// </summary>
		/// <param name="key">The key.</param>
		public void RemoveCache( string key )
		{
			CacheUtil.RemoveCache( GetCacheKey( key ) );
		}

		/// <summary>
		/// Removes the cache in screen route.
		/// </summary>
		public void RemoveCacheInScreenRoute()
		{
			CacheUtil.RemoveCacheInScreenRoute( this.cmnTabEntityModel.TabID, this.cmnTabEntityModel.ScreenRoute );
		}

		/// <summary>
		/// Removes the cache in tab.
		/// </summary>
		public void RemoveCacheInTab()
		{
            //if (this.cmnTabEntityModel != null && !string.IsNullOrWhiteSpace( this.cmnTabEntityModel.TabID ))
            //{
            //    CacheUtil.RemoveCacheInTab( this.cmnTabEntityModel.TabID );
            //}
            if (this.cmnTabEntityModel != null)
            {
                CacheUtil.RemoveCacheInTab(this.cmnTabEntityModel.TabID);
            }
		}

		#endregion Cache

		#region Validate

		private bool isModelStateValid()
		{
			if (ModelState.IsValid)
			{
				return true;
			}

			string error = ValidUtility.ErrorInfoString;
			ValidUtility.RemoveErrorInfo();

			HttpCookie errorMsgCookie = new HttpCookie( "GLV_SYS_isModelStateValid_error", error );
			errorMsgCookie.Expires = DateTime.Now.AddMinutes( 100d );
			Response.Cookies.Add( errorMsgCookie );
			return false;
		}

		/// <summary>
		/// Get list error validate by ModelState
		/// </summary>
		private void _ShowModelStateError()
		{
			// Default ViewBag.ErrorValidate is empty
			ViewBag.ErrorValidate = string.Empty;

			// If ModelState is not valid
			if (!ModelState.IsValid)
			{
				// Set all messenger error to ViewBag
				ViewBag.ErrorValidate = string.Join( "||", ModelState.Values
								.SelectMany( x => x.Errors )
								.Select( x => x.ErrorMessage ) );
			}
		}

		#endregion Validate

		public ActionResult GetMessage( string messageId, string replaceString )
		{
			string content = string.Empty;
			if (!String.IsNullOrEmpty( replaceString ))
			{
				content = Gulliver.DoCol.UtilityServices.Utility.GetMessage( messageId, replaceString );
			}
			else
			{
				content = Gulliver.DoCol.UtilityServices.Utility.GetMessage( messageId );
			}

			return Json( new { message = content }, JsonRequestBehavior.AllowGet );
		}

		#region Multiple Tab

		public ActionResult GetTabId( string oldTabId )
		{
			string newTabId = UriUtility.ConvertStringtoMD5( DateTime.Now.ToString() );
			if (!string.IsNullOrEmpty( oldTabId ) && !newTabId.Equals( oldTabId ))
			{
				// Clone old session
				CacheUtil.CloneTabCache( oldTabId, newTabId );
			}

			return Json( new { tabId = newTabId }, JsonRequestBehavior.AllowGet );
		}

		#region 20150509
		public static RouteValueDictionary ToRouteValueDictionary( NameValueCollection collection )
		{
			RouteValueDictionary dic = new RouteValueDictionary();
			foreach (string key in collection.Keys)
			{
				dic.Add( key, collection[key] );
			}

			return dic;
		}

		public static RouteValueDictionary AddOrUpdate( RouteValueDictionary dictionary, string key, object value )
		{
			dictionary[key] = value;
			return dictionary;
		}

		public static RouteValueDictionary RemoveKeys( RouteValueDictionary dictionary, params string[] keys )
		{
			foreach (string key in keys)
			{
				dictionary.Remove( key );
			}

			return dictionary;
		}
		#endregion

		public RedirectToRouteResult Redirect( string actionName, string controllerName, object routeValues )
		{
			string newTabId = UriUtility.ConvertStringtoMD5( DateTime.Now.ToString() );
			if (routeValues != null)
			{
				RouteValueDictionary newRoute = new RouteValueDictionary( routeValues );
				newRoute = AddOrUpdate( newRoute, "tabId", this.CmnTabEntityModel != null ? this.CmnTabEntityModel.TabID ?? newTabId : newTabId );
				return base.RedirectToAction( actionName, controllerName, newRoute );
			}
			routeValues = new { tabId = this.CmnTabEntityModel != null ? this.CmnTabEntityModel.TabID ?? newTabId : newTabId };
			return base.RedirectToAction( actionName, controllerName, routeValues );
		}

		public RedirectToRouteResult Redirect( string actionName, string controllerName )
		{
			return this.Redirect( actionName, controllerName, null );
		}

		#endregion Multiple Tab

		#region Paging

		/// <summary>
		/// Render Paging Control
		/// </summary>
		/// <returns></returns>
		public ActionResult Paging( CmnPagingModel pagingModel )
		{
			//CmnPagingModel pagingModel = this.GetCache<CmnPagingModel>( CacheKeys.GLV_CMN_PAGING );

			var pageIndex = pagingModel.PageIndex;
			var pageSize = pagingModel.PageSize;
			var totalRow = pagingModel.TotalRow;
			int pageBegin = pagingModel.PageBegin;
			int pageEnd = pagingModel.PageEnd;
			int startOffset;
			int endOffset;
			int numberOfPage = this.NumberOfPage( totalRow, pagingModel.PageSize );

			if (pageIndex == 1 || (pageBegin == 0 && pageEnd == 0))
			{
				pageBegin = 1;
				pageEnd = 5;
			}
			else if ((pageIndex == pageEnd && pageIndex < numberOfPage && numberOfPage < pageEnd + 2))
			{
				pageBegin = pageIndex - 3;
				pageEnd = pageIndex + 1;
			}
			else if ((pageIndex == pageEnd && pageIndex < numberOfPage) || (pageIndex == pageBegin && pageIndex > 2))
			{
				pageBegin = pageIndex - 2;
				pageEnd = pageIndex + 2;
			}
			else if ((pageIndex == pageBegin && pageIndex <= 2))
			{
				pageBegin = 1;
				pageEnd = 5;
			}

			if (numberOfPage <= 5)
			{
				pageBegin = 1;
				pageEnd = numberOfPage;
			}

			pagingModel.PageBegin = pageBegin;
			pagingModel.PageEnd = pageEnd;

			startOffset = (pageIndex - 1) * pageSize + 1;

			// check current page is the last page ?
			if (pageIndex != numberOfPage && numberOfPage > 1)
			{
				// case : current page is not the last page
				endOffset = startOffset + pageSize - 1;
			}
			else
			{
				// case : current page is the last page
				endOffset = totalRow;
			}

			ViewBag.PageSize = pageSize;
			ViewBag.PageIndex = pageIndex;
			ViewBag.Total = totalRow;
			ViewBag.NumberOfPage = numberOfPage;
			ViewBag.PageBegin = pageBegin;
			ViewBag.PageEnd = pageEnd;
			ViewBag.SortItem = pagingModel.SortItem;
			ViewBag.SortDirection = pagingModel.SortDirection;
			ViewBag.StartOffset = startOffset;
			ViewBag.EndOffset = endOffset;

			//this.SaveCache( CacheKeys.GLV_CMN_PAGING, pagingModel );

			return PartialView( "_Paging" );
		}

		#endregion Paging

		#region Declare private method

		/// <summary>
		/// Get number of page function
		/// </summary>
		/// <param name="totalRow">otal row of the result</param>
		/// <param name="pageSize">The page size</param>
		/// <returns>The number of page</returns>
		private int NumberOfPage( int totalRow, int pageSize )
		{
			int numberOfPage = 0;

			if (pageSize == 0)
			{
				return numberOfPage;
			}

			if (totalRow / pageSize > 0)
			{
				numberOfPage = Convert.ToInt32( Math.Ceiling( (double)totalRow / pageSize ) );
			}

			return numberOfPage;
		}

		private CmnTabEntityModel GetCmnTabEntityModel( string TabId )
		{
			return this.GetCache<CmnTabEntityModel>( TabId + "CmnTabEntityModel", new CmnTabEntityModel() );
		}

		private string GetCacheKey( string key )
		{
			if (this.cmnTabEntityModel != null && !string.IsNullOrEmpty( this.cmnTabEntityModel.TabID ))
			{
				return "_" + this.cmnTabEntityModel.TabID + "_" + key;
			}
			return key;
		}

		#endregion Declare private method
	}
}