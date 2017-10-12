//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using Gulliver.DoCol.DataAccess;

namespace Gulliver.DoCol.UtilityServices
{
	public static partial class Utility
	{
		public static void SequenceGetValue( string seqKey, out string seqValue )
		{
			UtilityDa.SequenceGetValue( seqKey, out seqValue );
		}
	}
}