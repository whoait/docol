namespace Gulliver.DoCol
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Http;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;
	using System.Web.Script.Serialization;
	using Gulliver.DoCol.Areas.Common.Controllers;
	using Gulliver.DoCol.Constants;
	using Gulliver.DoCol.BusinessServices.Common;

	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		private Dictionary<string, Dictionary<string, object>> shortLiveCache
											= new Dictionary<string, Dictionary<string, object>>();

		protected void Application_Start()
		{
			Log.SaveLogToFile( Log.LogLevel.INFO,
									"Application_Start",
									"",
									"",
									"",
									"START",
									"" );
			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register( GlobalConfiguration.Configuration );
			FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );

			RegisterGlobalFilters( GlobalFilters.Filters );

			RouteConfig.RegisterRoutes( RouteTable.Routes );
			BundleTable.EnableOptimizations = false;
			BundleConfig.RegisterBundles( BundleTable.Bundles );
			AuthConfig.RegisterAuth();
			DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
			/* Load custom Razor engines */
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add( new Gulliver.DoCol.RouteConfig.RazorViewFactory() );
			Application["shortLiveCache"] = shortLiveCache;
			var clientDataTypeProvider = ModelValidatorProviders.Providers.FirstOrDefault( p => p.GetType().Equals( typeof( ClientDataTypeModelValidatorProvider ) ) );
			ModelValidatorProviders.Providers.Remove( clientDataTypeProvider );
			Log.SaveLogToFile( Log.LogLevel.INFO,
									"Application_Start",
									"",
									"",
									"",
									"END",
									"" );
		}

		public static void RegisterGlobalFilters( GlobalFilterCollection filters )
		{
			Log.SaveLogToFile( Log.LogLevel.INFO,
									"RegisterGlobalFilters",
									"",
									"",
									"",
									"START",
									"" );
			filters.Add( new HandleErrorAttribute() );
			Log.SaveLogToFile( Log.LogLevel.INFO,
									"RegisterGlobalFilters",
									"",
									"",
									"",
									"END",
									"" );
		}

		protected void Application_Error( object sender, EventArgs e )
		{
			Log.SaveLogToFile( Log.LogLevel.INFO,
									"Application_Error",
									"",
									"",
									"",
									"START",
									"" );
			var httpContext = ((MvcApplication)sender).Context;
			var ex = Server.GetLastError();
			Controller controller = new ErrorController();
			var routeData = new RouteData();
			var action = "SqlException";
			HttpException httpEx = new HttpException();

			httpContext.ClearError();
			httpContext.Response.Clear();
			if (ex.GetType() == typeof( HttpException ))
			{
				httpEx = ex as HttpException;

				switch (httpEx.GetHttpCode())
				{
					case 404:
						action = "SqlException";
						break;

					case 401:
						action = "SqlException";
						break;
				}
			}

			if (ex.Message.Contains( ExceptionKey.GLV_CMN_DBException ))
			{
				action = "SqlException";
			}

			if (ex.Message.Contains( ExceptionKey.GLV_CMN_NotAuthenticated ))
			{
				action = "NotAuthenticated";
			}

			if (ex.Message.Contains( ExceptionKey.GLV_CMN_InvalidAccessException ))
			{
				action = "NotPermission";
			}

			if (ex.Message.Contains( ExceptionKey.GLV_CMN_NotFoundException ))
			{
				action = "NotFound";
			}

			if (ex.Message.Contains( ExceptionKey.GLV_CMN_LoginException ))
			{
				action = "Login";
			}

			httpContext.Response.StatusCode = ex is HttpException ? httpEx.GetHttpCode() : 500;
			httpContext.Response.TrySkipIisCustomErrors = true;

			string Message = ex.Message;
			string ErrorCode = httpContext.Response.StatusCode.ToString();

			routeData.Values["controller"] = "Error";
			routeData.Values["action"] = action;
			routeData.Values["message"] = Message;
			routeData.Values["referer"] = httpContext.Request.Url.LocalPath;

			bool isAjaxCall = string.Equals( "XMLHttpRequest", Context.Request.Headers["x-requested-with"], StringComparison.OrdinalIgnoreCase );
			Context.ClearError();
			if (isAjaxCall)
			{
				Context.Response.ContentType = "application/json";
				Context.Response.StatusCode = 200;
				Context.Response.Write(
					new JavaScriptSerializer().Serialize(
						new { CommonError = "CommonError", url = "/Common/Error/" + action + "?referer=" + routeData.Values["referer"] + "&message=" + routeData.Values["message"] }
					)
				);
			}
			else
			{
				((IController)controller).Execute( new RequestContext( new HttpContextWrapper( httpContext ), routeData ) );
			}
			controller.Dispose();

			Log.SaveLogToFile( Log.LogLevel.INFO,
									"Application_Error",
									"",
									"",
									"",
									"END",
									"" );
		}
	}
}