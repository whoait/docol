//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
namespace Gulliver.DoCol.MessageUtility
{
	public class MessageService
	{
		public static string GetMessage( string messageID, out string messageType )
		{
			string messageContent;
			string buttonOK = String.Empty;
			string buttonNOK = String.Empty;
			string buttonCancel = String.Empty;
			string defaultButton = String.Empty;
			MessageDa.GetMessage( messageID, string.Empty, out messageType, out messageContent
								, out buttonOK, out buttonNOK, out buttonCancel, out defaultButton );
			messageType = new JavaScriptSerializer().Serialize( new
			{
				messageType = messageType
				,
				buttonOK = buttonOK
				,
				buttonNOK = buttonNOK
				,
				buttonCancel = buttonCancel
				,
				defaultButton = defaultButton
			} );

			return messageContent.Replace( "\n", "" ).Replace( "\r", "" );
		}

		public static string GetMessage( string messageID, string replaceString, out string messageType )
		{
			string messageContent;
			string buttonOK = String.Empty;
			string buttonNOK = String.Empty;
			string buttonCancel = String.Empty;
			string defaultButton = String.Empty;
			MessageDa.GetMessage( messageID, replaceString, out messageType, out messageContent
								, out buttonOK, out buttonNOK, out buttonCancel, out defaultButton );
			messageType = new JavaScriptSerializer().Serialize( new
			{
				messageType = messageType
				,
				buttonOK = buttonOK
				,
				buttonNOK = buttonNOK
				,
				buttonCancel = buttonCancel
				,
				defaultButton = defaultButton
			} );
			return messageContent.Replace( "\n", "" ).Replace( "\r", "" );
		}

		public static string GetMessage( string messageID )
		{
			string messageType;
			string messageContent;
			string buttonOK = String.Empty;
			string buttonNOK = String.Empty;
			string buttonCancel = String.Empty;
			string defaultButton = String.Empty;
			MessageDa.GetMessage( messageID, string.Empty, out messageType, out messageContent
								, out buttonOK, out buttonNOK, out buttonCancel, out defaultButton );
			messageType = new JavaScriptSerializer().Serialize( new
			{
				messageType = messageType
				,
				buttonOK = buttonOK
				,
				buttonNOK = buttonNOK
				,
				buttonCancel = buttonCancel
				,
				defaultButton = defaultButton
			} );
			return messageContent.Replace( "\n", "" ).Replace( "\r", "" );
		}

		public static string GetMessage( string messageID, string replaceString )
		{
			string messageType;
			string messageContent;
			string buttonOK = String.Empty;
			string buttonNOK = String.Empty;
			string buttonCancel = String.Empty;
			string defaultButton = String.Empty;
			MessageDa.GetMessage( messageID, replaceString, out messageType, out messageContent
								, out buttonOK, out buttonNOK, out buttonCancel, out defaultButton );
			messageType = new JavaScriptSerializer().Serialize( new
			{
				messageType = messageType
				,
				buttonOK = buttonOK
				,
				buttonNOK = buttonNOK
				,
				buttonCancel = buttonCancel
				,
				defaultButton = defaultButton
			} );
			return messageContent.Replace( "\n", "" ).Replace( "\r", "" );
		}
	}
}