//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using Gulliver.DoCol.MessageUtility;

namespace Gulliver.DoCol.UtilityServices
{
	public static partial class Utility
	{
		public static string GetMessage( string messageID )
		{
			return MessageService.GetMessage( messageID );
		}

		public static string GetMessage( string messageID, string replaceString )
		{
			return MessageService.GetMessage( messageID, replaceString );
		}

		public static string GetMessage( string messageID, string replaceString1, string replaceString2 )
		{
			return MessageService.GetMessage( messageID, replaceString1 + "\t" + replaceString2 );
		}

		public static string GetMessage( string messageID, string replaceString1, string replaceString2, string replaceString3 )
		{
			return MessageService.GetMessage( messageID, replaceString1 + "\t" + replaceString2 + "\t" + replaceString3 );
		}

		public static string GetMessage( string messageID,
												string replaceString,
												out string messageType,
												out string messageContent )
		{
			messageContent = MessageService.GetMessage( messageID, replaceString, out messageType );
			return messageContent;
		}

		public static string GetMessage( string messageID,
												string replaceString1,
												string replaceString2,
												out string messageType,
												out string messageContent )
		{
			messageContent = MessageService.GetMessage( messageID, replaceString1 + "\t" + replaceString2, out messageType );
			return messageContent;
		}

		public static string GetMessage( string messageID,
												string replaceString1,
												string replaceString2,
												string replaceString3,
												out string messageType,
												out string messageContent )
		{
			messageContent = MessageService.GetMessage( messageID, replaceString1 + "\t" + replaceString2 + "\t" + replaceString3, out messageType );
			return messageContent;
		}
	}
}