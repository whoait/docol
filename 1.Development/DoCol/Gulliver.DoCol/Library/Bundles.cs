namespace Gulliver.DoCol.Library.Html
{
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Optimization;

	public static class Bundles
	{
		public static IHtmlString RenderStylesIe( string ie, params string[] paths )
		{
			var tag = string.Format( "<!--[if {0}]>{1}<![endif]-->", ie, Styles.Render( paths ) );
			return new MvcHtmlString( tag );
		}
	}
}