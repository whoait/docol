//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;

namespace Gulliver.DoCol.UtilityServices
{
	public class FTPDownloader
	{
		private bool isDeleteSource = false;

		//public string URL { get; set; }
		public NetworkCredential LoginCredentials { get; set; }

		//public string SavePath { get; set; }
		public long FileLength { get; set; }

		public bool IsDeleteSource
		{
			set
			{
				isDeleteSource = value;
			}
			get
			{
				return isDeleteSource;
			}
		}

		public bool Start( string pFTPUrl, string pSavePath )
		{
			this.FileLength = 0;

			FtpWebRequest reqFTP;

			//download file
			reqFTP = (FtpWebRequest)FtpWebRequest.Create( new Uri( pFTPUrl ) );
			reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
			reqFTP.UseBinary = true;
			reqFTP.Credentials = LoginCredentials;
			FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
			Stream ftpStream = response.GetResponseStream();

			using (FileStream outputStream = new FileStream( pSavePath, FileMode.Create ))
			{
				int bufferSize = 204800;
				int readCount;
				byte[] buffer = new byte[bufferSize];

				readCount = ftpStream.Read( buffer, 0, bufferSize );
				while (readCount > 0)
				{
					outputStream.Write( buffer, 0, readCount );
					readCount = ftpStream.Read( buffer, 0, bufferSize );
				}

				this.FileLength = outputStream.Length;

				outputStream.Close();
				ftpStream.Close();
				response.Close();
			}

			if (this.FileLength <= 0)
			{
				FileInfo ff = new FileInfo( pSavePath );
				this.FileLength = ff.Length;
				ff = null;
			}

			/////Assign the file size here
			//reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(URL));
			//reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
			//reqFTP.UseBinary = true;
			//reqFTP.Credentials = LoginCredentials;
			//FtpWebResponse newresponse = (FtpWebResponse)reqFTP.GetResponse();
			//this.FileLength = newresponse.ContentLength;
			//newresponse.Close();
			///// End

			//if (this.IsDeleteSource)
			//{
			//    /// Delete the Downloaded File from FTP server.
			//    reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(URL));
			//    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
			//    reqFTP.UseBinary = true;
			//    reqFTP.Credentials = LoginCredentials;
			//    FtpWebResponse newResponse = (FtpWebResponse)reqFTP.GetResponse();
			//    newResponse.Close();
			//}

			/// END
			return true;
		}

		public bool DeleteFile( string pFTPUrl )
		{
			/// Delete the Downloaded File from FTP server.
			FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create( new Uri( pFTPUrl ) );
			reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
			reqFTP.UseBinary = true;
			reqFTP.Credentials = LoginCredentials;
			FtpWebResponse newResponse = (FtpWebResponse)reqFTP.GetResponse();
			newResponse.Close();

			return true;
		}

		public string GetFileListInfo( string pFTPUrl )
		{
			string datastring = string.Empty;

			FtpWebRequest ftpclientRequest = (FtpWebRequest)WebRequest.Create( pFTPUrl );
			ftpclientRequest.Method = WebRequestMethods.Ftp.ListDirectory;//.ListDirectoryDetails;
			ftpclientRequest.Proxy = null;
			ftpclientRequest.Credentials = this.LoginCredentials;

			using (FtpWebResponse response = (FtpWebResponse)ftpclientRequest.GetResponse())
			{
				using (StreamReader sr = new StreamReader( response.GetResponseStream(), System.Text.Encoding.UTF8 ))
				{
					datastring = sr.ReadToEnd().Replace( "\r\n", "|" );
					sr.Close();
				}
				response.Close();
			}

			return datastring;
		}
	}
}