//---------------------------------------------------------------------------
// Version			: 001
// Designer			: DatNT
// Programmer		: HoangNH16
// Date				: Sep 08 2014
// Comment			: Revised, using log4net service.
//---------------------------------------------------------------------------

using Gulliver.DoCol.DataAccess;
using Gulliver.DoCol.Entities;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Gulliver.DoCol.BusinessServices.Common
{
	public class Log
	{
		private static log4net.ILog Logger = log4net.LogManager.GetLogger( typeof( Log ) );

		static Log()
		{
			log4net.Config.XmlConfigurator.ConfigureAndWatch( new FileInfo( System.Web.HttpContext.Current.Server.MapPath( "~/log4net.config" ) ) );
		}

		public enum LogLevel
		{
			ERROR, // E:Error
			WARN,  // W:Waring
			INFO   // I:Success
		}

		public static void SaveLog( LogLevel logLevel, string method, UserSessionInfo userSessionInfo, Exception exception )
		{
			if (userSessionInfo == null)
			{
				userSessionInfo = new UserSessionInfo();
			}

			if (exception == null)
			{
				exception = new Exception( "Exception be null!" );
			}

			SaveLogToFile( logLevel, method, userSessionInfo.UserID, userSessionInfo.StoreID, userSessionInfo.IPAddress, exception.Message, exception.StackTrace );
		}

		public static void SaveLogToDB( LogLevel logLevel, string method, UserSessionInfo userSessionInfo, Exception exception )
		{
			if (userSessionInfo == null)
			{
				userSessionInfo = new UserSessionInfo();
			}

			if (exception == null)
			{
				exception = new Exception( "Exception be null!" );
			}
			SaveLogToDB( logLevel, method, userSessionInfo.UserID, userSessionInfo.StoreID, userSessionInfo.IPAddress, exception.Message, exception.StackTrace );
		}

		public static void SaveLogToDB( LogLevel logLevel, string method, string loginUser, string loginStore, string ipAddress, string logMessage, string detail )
		{
			String logDiv = String.Empty;
			switch (logLevel)
			{
				case LogLevel.ERROR:
					logDiv = "E";
					break;

				case LogLevel.INFO:
					logDiv = "I";
					break;

				case LogLevel.WARN:
					logDiv = "W";
					break;
			}
			UtilityDa.LogSave( logDiv, method, loginUser, loginStore, ipAddress, logMessage, detail );
		}

		public static void SaveLogToFile( LogLevel logLevel, string method, string loginUser, string loginStore, string ipAddress, string logMessage, string detail )
		{
			{
				StringBuilder strMessage = new StringBuilder();
				strMessage.Append( "[" + DateTime.Now.ToString( "MM/dd/yyyy hh:mm:tt" ) + "]" );
				strMessage.Append( "[" + method + "]" );
				strMessage.Append( "[" + loginUser + "]" );
				strMessage.Append( "[" + loginStore + "]" );
				strMessage.Append( "[" + ipAddress + "]" );
				strMessage.Append( "[" + logMessage + "]" );
				strMessage.Append( "\r\n" );
				strMessage.Append( detail );

				switch (logLevel)
				{
					case LogLevel.ERROR:
						Logger.Error( strMessage );
						break;

					case LogLevel.INFO:
						Logger.Info( strMessage );
						break;

					case LogLevel.WARN:
						Logger.Warn( strMessage );
						break;
				}
			}
		}
	}
}