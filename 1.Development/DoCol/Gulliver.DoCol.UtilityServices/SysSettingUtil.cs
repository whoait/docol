//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System.Configuration;
using System.Web.Configuration;

namespace Gulliver.DoCol.UtilityServices
{
	public static class SysSettingUtil
	{
		private static double timeout = 0;

		public static double GetTimeoutMinute()
		{
			if (timeout == 0)
			{
				SessionStateSection sessionState = (SessionStateSection)ConfigurationManager.GetSection( "system.web/sessionState" );
				if (sessionState != null)
				{
					timeout = sessionState.Timeout.TotalMinutes;
				}
			}
			return timeout;
		}
	}
}