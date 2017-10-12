//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;

namespace Gulliver.DoCol.Entities
{
	public class UserModel
	{
		public int UserId { get; set; }

		public string Username { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public Nullable<bool> IsActive { get; set; }

		public Nullable<System.DateTime> CreateDate { get; set; }

		public Nullable<bool> IsAdmin { get; set; }
	}
}