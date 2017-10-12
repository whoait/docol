//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.Text;

namespace Gulliver.DoCol.UtilityServices
{
	public static partial class Utility
	{
		public static string Encode( string value )
		{
			return Convert.ToBase64String( Encoding.UTF8.GetBytes( value ) );
		}

		public static string Decode( string value )
		{
			return Encoding.UTF8.GetString( Convert.FromBase64String( value ) );
		}

		//public static string GetDolphinetLink(string dn_no)
		//{
		//    //http://dolphinet-i.in.glv.co.jp/direct_login2.aspx?DN_NO={dn_no}&MODE={mode}&SHOP={shop}&PASS={pass}
		//    return Dolphinet.LinkPattern.Replace("{dn_no}", dn_no).Replace("{mode}", Decode(Dolphinet.Mode)).Replace("{shop}", Decode(Dolphinet.Shop)).Replace("{pass}", Decode(Dolphinet.Pass));
		//}
	}
}