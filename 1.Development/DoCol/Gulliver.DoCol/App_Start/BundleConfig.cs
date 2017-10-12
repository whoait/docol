//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System.Web.Optimization;

namespace Gulliver.DoCol
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles( BundleCollection bundles )
		{
			bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
						"~/Scripts/base/jquery-{version}.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/jqueryui" ).Include(
						"~/Scripts/base/jquery-ui-{version}.js",
                        "~/Scripts/base/jquery.ui.datepicker-ja.js") );

			bundles.Add( new ScriptBundle( "~/bundles/TabIndexPopup" ).Include(
						"~/Scripts/base/TabIndexPopup.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/jqueryval" ).Include(
						"~/Scripts/base/jquery.unobtrusive*",
						"~/Scripts/base/jquery.validate*",
						"~/Scripts/base/jquery.numeric*",
						"~/Scripts/base/jquery.inputmask*" ) );

			bundles.Add( new ScriptBundle( "~/bundles/jqueryCommon" ).Include(
						"~/Scripts/base/jqueryCommon.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/jqueryValidaion" ).Include(
						"~/Scripts/base/jquery.validate.js",
						"~/Scripts/base/jquery.validate.unobtrusive.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/customjs" ).Include(
						"~/Scripts/base/bootstrap.js",
						"~/Scripts/base/GLV-data-validation.js",
						"~/Scripts/base/modal.js",
						"~/Scripts/base/Common.js",
						"~/Scripts/base/autolayout.js",
						"~/Scripts/base/scrollup.js",
						"~/Scripts/base/MultipleTab.js",
						"~/Scripts/base/jquery.serializejson.js",
						"~/Scripts/base/Grid.js" ) );

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add( new ScriptBundle( "~/bundles/modernizr" ).Include(
						"~/Scripts/base/modernizr-*" ) );

			bundles.Add( new StyleBundle( "~/bundles/style" ).Include(
                        "~/Content/themes/base/calendar.css",
						"~/Content/themes/base/bootstrap.css",
						"~/Content/themes/base/bootstrap_grid_custom.css",
						"~/Content/themes/base/jquery-ui.css",
						"~/Content/themes/base/jquery.ui.all.css",
						"~/Content/themes/base/controls.css",
						"~/Content/themes/base/color.css",
						"~/Content/themes/base/style.css"
						) );
		}
	}
}