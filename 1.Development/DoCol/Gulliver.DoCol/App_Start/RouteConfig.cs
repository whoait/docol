//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System.Web.Mvc;
using System.Web.Routing;

namespace Gulliver.DoCol
{
	public class RouteConfig
	{
		public static void RegisterRoutes( RouteCollection routes )
		{
			routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );
			routes.IgnoreRoute( "*/{*path}" );
			routes.MapRoute(
				name: "Default",
				url: "{area}/{controller}/{action}/{id}",
                defaults: new { area = "DCW", controller = "DCW001", action = "DCW001Index", id = UrlParameter.Optional }
			);

           // routes.MapRoute(
           //    name: "getMessage",
           //    url: "base/getmessage",
           //    defaults: new { controller = "BaseController", action = "GetMessage" }
           //);
		}

		/// <summary>
		/// Custom view razor engine for custom view files.
		/// </summary>
		public class RazorViewFactory : RazorViewEngine
		{
			public RazorViewFactory()
			{
				MasterLocationFormats = new[] { "~/Views/Shared/{0}.cshtml" };

				ViewLocationFormats = new[]{
                    "~/Areas/Common/Views/{1}/{0}.cshtml",
                    "~/Areas/DCW/Views/{1}/{0}.cshtml",
                    "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml"
                    };
			}
		}
	}
}