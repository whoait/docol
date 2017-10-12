//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------
//---------------------------------------------------------------------------
// Version			: 002
// Designer			: DungNH6-FPT
// Programmer		: DungNH6-FPT
// Date				: 2015/04/24
// Comment			: Insert enum ConnectDatabase
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.Constants
{
	public enum Modules
	{
		// TODO

		//Group = 1,
		//UserAdmin = 2,
		//ModulePermission = 3,
		//Menu = 4,
		//Client = 5,
		//Project = 6,
		//ResourceAllocation = 7,
		//UserLog = 8,
		//Issue = 9,
		//Risk = 10,
		//MultipleTabs = 13
	}

	public enum Permissions
	{
		// TODO

		//Read = 1,
		//Insert = 2,
		//Update = 3,
		//Delete = 4,
		//Export = 5,
		//Assign = 6,
		//ForceEdit = 7,
		//Confirm = 8,
		//UnConfirm = 9,
		//ViewAll = 10,
		//FullControls = 11
	}

	public enum PortalRole
	{
		// TODO
	}

	/// <summary>
	/// The connect database.
	/// </summary>
	public enum ConnectDatabase
	{
		/// <summary>
		/// The local.
		/// </summary>
		Local = 0,

		/// <summary>
		/// The external db futai.
		/// </summary>
		ExternalDBFutai = 1,

		/// <summary>
		/// The external car db.
		/// </summary>
		ExternalCarDB = 2,

		/// <summary>
		/// the external taps DB
		/// </summary>
		ExternalTapsDB = 3,

		/// <summary>
		/// the external Lilliput DB
		/// </summary>
		ExternalLilliputDB = 4
	}

	/// <summary>
	/// The mode login.
	/// </summary>
	public enum ModeLogin
	{
		/// <summary>
		/// Login with mode HQ
		/// </summary>
		HQ = 1,

		/// <summary>
		/// Login mode management
		/// </summary>
		Management = 2,

		/// <summary>
		/// Login mode employee
		/// </summary>
		Employee = 3
	}

	/// <summary>
	/// Order stage
	/// </summary>
	public enum Stage
	{
		StoreReceipt = 1,
		WorkInput = 2,
		RequestEstimation = 3,
		Order = 4,
		FactoryWarehousing = 5,
		GetEstimation = 6,
		InputEstimation = 7,
		InputOrderDetail = 8,
		InputDelivery = 9,
		ManagementApproveDelivery = 10,
		HQApproveDelivery = 11,
		InputPaymentRequest = 12,
		ApprovePayment = 13
	}

	public enum CategoryContentSeqNo
	{
		DeliveryPreviousUpgrading = 10,
		CostCarNew = 20,
		CostCarContinued = 30,
		InspectAppointment = 40
	}

	public enum FAXKbn
	{
		REQUEST_ESTIMATION = 1,
		CANCEL_REQUEST_ESTIMATION = 2,
		REQUEST_ESTIMATION_ORDER = 3,
		CANCEL_REQUEST_ESTIMATION_ORDER = 4,
		ORDER = 5,
		CANCEL_ORDER = 6
	}
}