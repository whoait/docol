using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;

namespace Gulliver.DoCol.UtilityServices
{
	public static class JavascriptExtension
	{
		public static MvcHtmlString IncludeVersionedJs( this HtmlHelper helper, string filename )
		{
			string version = GetVersion( helper, filename );
			return MvcHtmlString.Create( "<script src='" + filename + version + "'></script>" );
		}

		private static string GetVersion( this HtmlHelper helper, string filename )
		{
			var context = helper.ViewContext.RequestContext.HttpContext;

			if (context.Cache[filename] == null)
			{
				var physicalPath = context.Server.MapPath( filename );
				var version = "?v=" +
				  new System.IO.FileInfo( physicalPath ).LastWriteTime
					.ToString( "yyyyMMddHHmmss" );
				context.Cache.Add( physicalPath, version, null,
				  DateTime.Now.AddMinutes( 1 ), TimeSpan.Zero,
				  CacheItemPriority.Normal, null );
				context.Cache[filename] = version;
				return version;
			}
			else
			{
				return context.Cache[filename] as string;
			}
		}
	}
}
