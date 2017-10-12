//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.Entities
{
	public class LoginUserAuthorityModel
	{
		public LoginUserAuthorityModel()
		{
			this.EstimationAndDeliveryView = false;
			this.EstimationAndDeliveryUpdate = false;
			this.EstimationAndDeliveryDelete = false;
			this.BillingView = false;
			this.BillingUpdate = false;
			this.BillingDelete = false;
			this.Master = 0;
		}

		public bool EstimationAndDeliveryView { get; set; }

		public bool EstimationAndDeliveryUpdate { get; set; }

		public bool EstimationAndDeliveryDelete { get; set; }

		public bool BillingView { get; set; }

		public bool BillingUpdate { get; set; }

		public bool BillingDelete { get; set; }

		public int Master { get; set; }

		public bool IsGLVUser { get; set; }
	}
}