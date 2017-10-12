//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.UtilityServices.security
{
	using Gulliver.DoCol.Constants.Resources;
	using Gulliver.DoCol.Constants.Security;
	using Gulliver.DoCol.Entities;
	using System.Resources;
	using System.Web;

	/// <summary>
	///
	/// </summary>
	public class Permision
	{
		/// <summary>
		/// Gets the CMN entity model.
		/// </summary>
		/// <value>
		/// The CMN entity model.
		/// </value>
		private static CmnEntityModel CmnEntityModel
		{
			get
			{
				if (HttpContext.Current.Session["CmnEntityModel"] == null)
				{
					HttpContext.Current.Session["CmnEntityModel"] = new CmnEntityModel();
				}
				return (CmnEntityModel)HttpContext.Current.Session["CmnEntityModel"];
			}
		}

		/// <summary>
		/// Gets the permision.
		/// </summary>
		/// <returns></returns>
		public static string GetPermision()
		{
			string screenID = CmnEntityModel.CurrentScreenID;

			return GetPermision( screenID );
		}

		public static string GetPermision( string screenID )
		{
			// TODO
			
			//string authority = string.Empty;
			//string screenType = string.Empty;
			//ResourceManager managerResource = new ResourceManager( typeof( ScreenList ) );

			//if (string.IsNullOrEmpty( screenID ))
			//{
			//	return null;
			//}

			//screenType = managerResource.GetString( screenID );
			//if (string.IsNullOrEmpty( screenType ))
			//{
			//	return null;
			//}

			//if (screenType.Contains( "Common" ))
			//{
			//	authority += PermisionType.ACCESS;
			//}

			//if (screenType.Contains( "Master" ))
			//{
			//	authority += CmnEntityModel.UserAuthority.Master == 1
			//		|| CmnEntityModel.UserAuthority.Master == 2 ? PermisionType.ACCESS : string.Empty;
			//}

			//if (screenType.Contains( "Estimation" ) || screenType.Contains( "Delivery" ))
			//{
			//	authority += CmnEntityModel.UserAuthority.EstimationAndDeliveryView
			//		|| CmnEntityModel.UserAuthority.EstimationAndDeliveryUpdate
			//		|| CmnEntityModel.UserAuthority.EstimationAndDeliveryDelete ? PermisionType.ACCESS : string.Empty;
			//}

			//if (screenType.Contains( "Billing" ))
			//{
			//	authority += CmnEntityModel.UserAuthority.BillingView
			//		|| CmnEntityModel.UserAuthority.BillingUpdate
			//		|| CmnEntityModel.UserAuthority.BillingDelete ? PermisionType.ACCESS : string.Empty;
			//}

			//if (screenType.Contains( "GLV" ))
			//{
			//	authority += CmnEntityModel.UserAuthority.IsGLVUser ? PermisionType.GLV_USER : string.Empty;
			//}

			//authority += CmnEntityModel.UserAuthority.BillingView ? PermisionType.BILLING_VIEW : string.Empty;
			//authority += CmnEntityModel.UserAuthority.BillingUpdate ? PermisionType.BILLING_UPDATE + PermisionType.BILLING_VIEW : string.Empty;
			//authority += CmnEntityModel.UserAuthority.BillingDelete ? PermisionType.BILLING_DELETE + PermisionType.BILLING_VIEW : string.Empty;
			//authority += CmnEntityModel.UserAuthority.EstimationAndDeliveryView ? PermisionType.ESTIMATION_AND_DELIVERY_VIEW : string.Empty;
			//authority += CmnEntityModel.UserAuthority.EstimationAndDeliveryUpdate ? PermisionType.ESTIMATION_AND_DELIVERY_UPDATE + PermisionType.ESTIMATION_AND_DELIVERY_VIEW : string.Empty;
			//authority += CmnEntityModel.UserAuthority.EstimationAndDeliveryDelete ? PermisionType.ESTIMATION_AND_DELIVERY_DELETE + PermisionType.ESTIMATION_AND_DELIVERY_VIEW : string.Empty;

			//switch (CmnEntityModel.UserAuthority.Master)
			//{
			//	case 1:
			//		authority += PermisionType.MASTER;
			//		break;

			//	case 2:
			//		authority += PermisionType.USER_MASTER;
			//		break;

			//	default:
			//		break;
			//}

			//return authority;

			return null;
		}
	}
}