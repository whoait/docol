//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using Gulliver.DoCol.Constants;
using Gulliver.DoCol.Entities;
using System.Web;

namespace Gulliver.DoCol.DataAccess
{
	public class BaseDa
	{
		private CmnEntityModel cmnEntityModel = null;

		public CmnEntityModel CmnEntityModel
		{
			get
			{
				if (cmnEntityModel == null)
				{
					cmnEntityModel = (CmnEntityModel)HttpContext.Current.Items[CacheKeys.CmnEntityModel];
				}
				return cmnEntityModel;
			}
		}
	}
}