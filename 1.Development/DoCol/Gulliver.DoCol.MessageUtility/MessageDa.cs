//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.MessageUtility
{
	using Gulliver.DoCol.DataAccess.Framework;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Reflection;
	using System.Web;
	using System.Linq;

	public class MessageDa
	{
		public static void GetMessage( string messageID
										, string replaceString
										, out string messageClass
										, out string messageContent
										, out string buttonOK
										, out string buttonNOK
										, out string buttonCancel
										, out string defaultButton)
		{
			messageClass = String.Empty;
			messageContent = String.Empty;
			buttonOK = String.Empty;
			buttonNOK = String.Empty;
			buttonCancel = String.Empty;
			defaultButton = String.Empty;

			messageContent = GetMessageContent( messageID, replaceString, out messageClass, out buttonOK, out buttonNOK, out buttonCancel, out defaultButton );

			//using (DBManager dbManager = new DBManager( "stp_GetMessage" ))
			//{
			//	dbManager.Add( "@MessageID", messageID );
			//	dbManager.Add( "@MessageClass", messageClass, ParameterDirection.Output );
			//	dbManager.Add( "@MessageContent", SqlDbType.NVarChar, 200, ParameterDirection.Output );
			//	dbManager.ExecuteNonQuery();
			//	messageClass = dbManager.GetValueInString( "@MessageClass" );
			//	messageContent = dbManager.GetValueInString( "@MessageContent" );
			//	if (!String.IsNullOrEmpty( messageContent ) && !string.IsNullOrEmpty( replaceString ))
			//	{
			//		string[] ListReplaceString = replaceString.Split( '\t' );
			//		for (int index = 0; index < ListReplaceString.Length; index++)
			//		{
			//			messageContent = messageContent.Replace( "{" + index.ToString() + "}", ListReplaceString[index] );
			//		}
			//	}
			//}

			if (String.IsNullOrEmpty( messageContent ))
			{
				throw new Exception( "Not found messagecd = " + messageID );
			}
		}

		public static List<MessageModel> GetAllMessage()
		{
			List<MessageModel> results = new List<MessageModel>();

			// Create new DBManager object for check user login into system.
			using (DBManager manager = new DBManager( "stp_GetMessageAll" ))
			{
				// Get the user login information from database.
				DataTable tableResult = manager.GetDataTable();

				if (tableResult.Rows.Count == 0)
				{
					return new List<MessageModel>();
				}
				else
				{
					results = EntityHelper<MessageModel>.GetListObject( tableResult );
				}
			}
			return results;
		}

		public static string GetMessageContent( string messageID
												, string replaceString
												, out string messageClass
												, out string buttonOK
												, out string buttonNOK
												, out string buttonCancel
												, out string defaultButton)
		{
			string messageContent = String.Empty;
			messageClass = String.Empty;
			messageContent = String.Empty;
			buttonOK = String.Empty;
			buttonNOK = String.Empty;
			buttonCancel = String.Empty;
			defaultButton = String.Empty;

			List<MessageModel> listMessage;
			//if (HttpContext.Current.Application["ListAllMessage"] != null)
			//{
			//	listMessage = (List<MessageModel>)HttpContext.Current.Application["ListAllMessage"];
			//}
			//else
			//{
				// Get and set all message to session
				HttpContext.Current.Application["ListAllMessage"] = GetAllMessage();
				listMessage = (List<MessageModel>)HttpContext.Current.Application["ListAllMessage"];
			//}

			MessageModel message = listMessage.FirstOrDefault( x => x.MessageId == messageID );
			if (message == null)
			{
				return null;
			}

			messageContent = message.Message;
			if (!string.IsNullOrEmpty( messageContent ) && !string.IsNullOrEmpty( replaceString ))
			{
				string[] ListReplaceString = replaceString.Split( '\t' );
				for (int index = 0; index < ListReplaceString.Length; index++)
				{
					messageContent = messageContent.Replace( "{" + index.ToString() + "}", ListReplaceString[index] );
				}
			}

			messageClass = message.Class;
			buttonOK = message.ButtonOK;
			buttonNOK = message.ButtonNOK;
			buttonCancel = message.ButtonCancel;
			defaultButton = message.DefaultButton;

			return messageContent;
		}

		public static List<MessageModel> ConvertList( List<string> lstStr )
		{
			List<MessageModel> lstResult = new List<MessageModel>();
			foreach (var item in lstStr)
			{
				MessageModel model = new MessageModel();
				model = ConvertStringToObjiect( item );
				lstResult.Add( model );
			}
			return lstResult;
		}

		public static MessageModel ConvertStringToObjiect( string str )
		{
			if (string.IsNullOrEmpty( str ))
			{
				return null;
			}
			int index1 = str.IndexOf( "/" );
			int index2 = str.IndexOf( "//" );
			MessageModel model = new MessageModel();
			model.MessageId = str.Substring( 0, index1 );
			model.Class = str.Substring( index1 + 1, 1 );
			model.Message = str.Substring( index2 + 2 );

			return model;
		}
	}

	public class MessageModel
	{
		public string MessageId { get; set; }
		public string Class { get; set; }
		public string Message { get; set; }
		public string ButtonOK { get; set; }
		public string ButtonNOK { get; set; }
		public string ButtonCancel { get; set; }
		public string DefaultButton { get; set; }
	}


	public class EntityHelper<T> where T : new()
	{
		public static List<T> GetListObject( DataTable dt )
		{
			List<T> lstObject = new List<T>();

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				T obj = new T();
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					int index = IndexOfField( dt.Columns[j].ColumnName );
					if (index != -1)
					{
						PropertyInfo pi = obj.GetType().GetProperties()[index];
						Type propType = pi.PropertyType;

						if ((propType.IsGenericType) && (propType.GetGenericTypeDefinition() == typeof( Nullable<> )))
						{
							propType = propType.GetGenericArguments()[0];
						}

						if (dt.Columns[j].DataType.Equals( propType ) &&
							dt.Rows[i][j] != DBNull.Value)
						{
							pi.SetValue( obj, dt.Rows[i][j], null );
						}
					}
				}
				lstObject.Add( obj );
			}
			return lstObject;
		}

		public static DataTable ConvertToDataTable( IList<T> list )
		{
			if (list == null || list.Count == 0)
			{
				return null;
			}

			DataTable table = new DataTable();
			foreach (var prop in list[0].GetType().GetProperties())
			{
				Type colType = prop.PropertyType;

				if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof( Nullable<> )))
				{
					colType = colType.GetGenericArguments()[0];
				}

				table.Columns.Add( new DataColumn( prop.Name, colType ) );
			}

			foreach (T item in list)
			{
				DataRow row = table.NewRow();

				foreach (DataColumn column in table.Columns)
				{
					int index = IndexOfField( column.ColumnName );
					if (index != -1)
					{
						PropertyInfo info = item.GetType().GetProperties()[index];
						Type propType = info.PropertyType;

						if ((propType.IsGenericType) && (propType.GetGenericTypeDefinition() == typeof( Nullable<> )))
						{
							propType = propType.GetGenericArguments()[0];
						}

						if (propType.Equals( column.DataType ))
						{
							var colValue = info.GetValue( item, null );
							if (colValue != null)
							{
								row[column.ColumnName] = colValue;
							}
							else
							{
								row[column.ColumnName] = DBNull.Value;
							}
						}
					}
				}
				table.Rows.Add( row );
			}
			return table;
		}

		private static int IndexOfField( string colName )
		{
			T o = new T();
			PropertyInfo[] pi = o.GetType().GetProperties();
			for (int i = 0; i < pi.Length; i++)
				if (pi[i].Name == colName)
					return i;
			return -1;
		}
	}
}