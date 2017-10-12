using System.Web.Mvc;

namespace Gulliver.DoCol.Areas.DCW
{
	public class DCWAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "DCW";
			}
		}

		public override void RegisterArea( AreaRegistrationContext context )
		{
			context.MapRoute(
				"DCW_default",
				"DCW/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}