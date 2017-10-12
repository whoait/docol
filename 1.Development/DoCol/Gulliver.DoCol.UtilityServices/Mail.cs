//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Gulliver.DoCol.UtilityServices
{
	/// <summary>
	/// Summary description for Mail
	/// </summary>
	public class Mail
	{
		public Mail()
		{
			if (string.IsNullOrEmpty( serverName ))
			{
				serverName = SettingsCommon.MailServer;
			}
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// The name of the SMTP server
		/// </summary>
		public static string serverName;

		/// <summary>
		/// Sends a mail
		/// </summary>
		/// <param name="toAdresses"></param>
		/// <param name="fromAddress"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public static void SendMail( string toAdresses, string fromAddress, string subject, string body )
		{
			Mail.SendMailWithoutAttachment( Mail.serverName, toAdresses, fromAddress, String.Empty, String.Empty, subject, body );
		}

		/// <summary>
		/// Sends a mail
		/// </summary>
		/// <param name="toAdresses"></param>
		/// <param name="fromAddress"></param>
		/// <param name="ccAdresses"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public static void SendMail( string toAdresses, string fromAddress, string ccAdresses, string subject, string body )
		{
			Mail.SendMailWithoutAttachment( Mail.serverName, toAdresses, fromAddress, ccAdresses, String.Empty, subject, body );
		}

		/// <summary>
		/// Sends a mail
		/// </summary>
		/// <param name="toAdresses"></param>
		/// <param name="fromAddress"></param>
		/// <param name="ccAdresses"></param>
		/// <param name="bccAddresses"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public static void SendMail( string toAdresses, string fromAddress, string ccAdresses, string bccAddresses, string subject, string body )
		{
			Mail.SendMailWithoutAttachment( Mail.serverName, toAdresses, fromAddress, ccAdresses, bccAddresses, subject, body );
		}

		/// <summary>
		/// Sends a mail
		/// </summary>
		/// <param name="server"></param>
		/// <param name="toAdresses"></param>
		/// <param name="fromAddress"></param>
		/// <param name="ccAdresses"></param>
		/// <param name="bccAddresses"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public static void SendMail( string server, string toAdresses, string fromAddress, string ccAdresses, string bccAddresses, string subject, string body )
		{
			Mail.SendMailWithoutAttachment( server, toAdresses, fromAddress, ccAdresses, bccAddresses, subject, body );
		}

		/// <summary>
		/// Sends a mail
		/// </summary>
		/// <param name="server"></param>
		/// <param name="toAdresses"></param>
		/// <param name="fromAddress"></param>
		/// <param name="ccAdresses"></param>
		/// <param name="bccAddresses"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public static void SendMailWithoutAttachment( string server, string toAdresses, string fromAddress, string ccAdresses, string bccAddresses, string subject, string body )
		{
			// Create a message and set up the recipients.
			MailMessage message = new MailMessage( fromAddress, toAdresses, subject, body );
			message.IsBodyHtml = true;
			if (!String.IsNullOrEmpty( ccAdresses ))
			{
				message.CC.Add( ccAdresses );
			}
			if (!String.IsNullOrEmpty( bccAddresses ))
			{
				message.Bcc.Add( bccAddresses );
			}

			/* FPT TuNM3 add config Mail Server Start. */
			string userName = SettingsCommon.MailUserName;
			string password = SettingsCommon.MailPassword;
			bool isProxyRequired = SettingsCommon.MailIsProxyRequired;
			int port = SettingsCommon.MailPort;
			//Send the message.
			SmtpClient client = new SmtpClient( server, port );
			if (isProxyRequired)
			{
				client.Credentials = new System.Net.NetworkCredential( userName, password );
			}
			else
			{
				// Add credentials if the SMTP server requires them.
				client.Credentials = CredentialCache.DefaultNetworkCredentials;
			}
			/* FPT TuNM3 add config Mail Server End. */

			try
			{
				client.Send( message );
			}
			catch (Exception ex)
			{
				Console.WriteLine( "Exception caught in CreateMessageWithAttachment(): {0}",
					  ex.ToString() );
			}
		}

		/// <summary>
		/// Sends a mail
		/// </summary>
		/// <param name="server"></param>
		/// <param name="toAdresses"></param>
		/// <param name="fromAddress"></param>
		/// <param name="ccAdresses"></param>
		/// <param name="bccAddresses"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <param name="fileName"></param>
		public static void SendMailWithSingleAttachment( string server, string toAdresses, string fromAddress, string ccAdresses, string bccAddresses, string subject, string body, string fileName )
		{
			//for multiple email ids
			toAdresses = toAdresses.Replace( ";", "," );

			// Create a message and set up the recipients.
			MailMessage message = new MailMessage( fromAddress, toAdresses, subject, body );

			message.IsBodyHtml = true;

			if (!string.IsNullOrEmpty( ccAdresses ))
			{
				message.CC.Add( ccAdresses );
			}

			if (!string.IsNullOrEmpty( bccAddresses ))
			{
				message.Bcc.Add( bccAddresses );
			}

			// Create  the file attachment for this e-mail message.
			Attachment data = new Attachment( fileName, MediaTypeNames.Application.Octet );
			// Add time stamp information for the file.
			ContentDisposition disposition = data.ContentDisposition;
			disposition.CreationDate = System.IO.File.GetCreationTime( fileName );
			disposition.ModificationDate = System.IO.File.GetLastWriteTime( fileName );
			disposition.ReadDate = System.IO.File.GetLastAccessTime( fileName );
			// Add the file attachment to this e-mail message.
			message.Attachments.Add( data );

			//Send the message.
			SmtpClient client = new SmtpClient( server );
			/* FPT TuNM3 add config Mail Server Start. */
			string userName = SettingsCommon.MailUserName;
			string password = SettingsCommon.MailPassword;
			bool isProxyRequired = SettingsCommon.MailIsProxyRequired;
			client.Port = SettingsCommon.MailPort;
			// Add credentials if the SMTP server requires them.
			if (isProxyRequired)
			{
				client.Credentials = new System.Net.NetworkCredential( userName, password );
			}
			else
			{
				client.Credentials = CredentialCache.DefaultNetworkCredentials;
			}

			/* FPT TuNM3 add  config Mail Server End. */
			try
			{
				client.Send( message );
			}
			catch (Exception ex)
			{
				Console.WriteLine( "Exception caught in CreateMessageWithAttachment(): {0}",
					  ex.ToString() );
			}
			// Display the values in the ContentDisposition for the attachment.
			//ContentDisposition cd = data.ContentDisposition;
			//Console.WriteLine("Content disposition");
			//Console.WriteLine(cd.ToString());
			//Console.WriteLine("File {0}", cd.FileName);
			//Console.WriteLine("Size {0}", cd.Size);
			//Console.WriteLine("Creation {0}", cd.CreationDate);
			//Console.WriteLine("Modification {0}", cd.ModificationDate);
			//Console.WriteLine("Read {0}", cd.ReadDate);
			//Console.WriteLine("Inline {0}", cd.Inline);
			//Console.WriteLine("Parameters: {0}", cd.Parameters.Count);
			//foreach (System.Collections.DictionaryEntry d in cd.Parameters)
			//{
			//    Console.WriteLine("{0} = {1}", d.Key, d.Value);
			//}
			data.Dispose();
		}

		/// <summary>
		/// Sends a mail
		/// </summary>
		/// <param name="server"></param>
		/// <param name="toAdresses"></param>
		/// <param name="fromAddress"></param>
		/// <param name="ccAdresses"></param>
		/// <param name="bccAddresses"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <param name="fileName"></param>
		public static void SendMailWithAttachments( string server, string toAdresses, string fromAddress, string ccAdresses, string bccAddresses, string subject, string body, string[] fileName )
		{
			// Create a message and set up the recipients.
			MailMessage message = new MailMessage( fromAddress, toAdresses, subject, body );
			message.CC.Add( ccAdresses );
			message.Bcc.Add( bccAddresses );

			ContentDisposition[] disposition = new ContentDisposition[fileName.Count()];
			Attachment[] data = new Attachment[fileName.Count()];
			int fileCount = 0;
			foreach (string item in fileName)
			{
				// Create  the file attachment for this e-mail message.
				data[fileCount] = new Attachment( item, MediaTypeNames.Application.Octet );
				// Add time stamp information for the file.
				disposition[fileCount] = data[fileCount].ContentDisposition;
				disposition[fileCount].CreationDate = System.IO.File.GetCreationTime( item );
				disposition[fileCount].ModificationDate = System.IO.File.GetLastWriteTime( item );
				disposition[fileCount].ReadDate = System.IO.File.GetLastAccessTime( item );
				// Add the file attachment to this e-mail message.
				message.Attachments.Add( data[fileCount] );
				fileCount++;
			}

			//Send the message.
			SmtpClient client = new SmtpClient( server );
			// Add credentials if the SMTP server requires them.
			client.Credentials = CredentialCache.DefaultNetworkCredentials;

			try
			{
				client.Send( message );
			}
			catch (Exception ex)
			{
				Console.WriteLine( "Exception caught in CreateMessageWithAttachment(): {0}",
					  ex.ToString() );
			}
			for (int i = 0; i < fileCount - 1; i++)
			{
				//// Display the values in the ContentDisposition for the attachment.
				//ContentDisposition cd = data[i].ContentDisposition;
				//Console.WriteLine("Content disposition");
				//Console.WriteLine(cd.ToString());
				//Console.WriteLine("File {0}", cd.FileName);
				//Console.WriteLine("Size {0}", cd.Size);
				//Console.WriteLine("Creation {0}", cd.CreationDate);
				//Console.WriteLine("Modification {0}", cd.ModificationDate);
				//Console.WriteLine("Read {0}", cd.ReadDate);
				//Console.WriteLine("Inline {0}", cd.Inline);
				//Console.WriteLine("Parameters: {0}", cd.Parameters.Count);
				//foreach (System.Collections.DictionaryEntry d in cd.Parameters)
				//{
				//    Console.WriteLine("{0} = {1}", d.Key, d.Value);
				//}
				data[i].Dispose();
			}
		}
	}
}