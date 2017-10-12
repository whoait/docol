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
	public class WebData : IDisposable
	{
		public string Data
		{
			get;
			set;
		}

		public bool Load( string pURL )
		{
			bool retVal = false;
			HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create( pURL );
			hwr.KeepAlive = true;
			hwr.Method = "get";
			hwr.Timeout = 3600000;
			hwr.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";

			HttpWebResponse response = (HttpWebResponse)hwr.GetResponse();

			using (StreamReader sr = new StreamReader( response.GetResponseStream() ))
			{
				this.Data = sr.ReadToEnd();
				sr.Close();
				response.Close();
			}
			retVal = true;

			return retVal;
		}

		public bool LoadAndSave( string pURL, string pSaveFileName )
		{
			bool retVal = false;

			HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create( pURL );
			hwr.KeepAlive = true;
			hwr.Method = "get";
            hwr.Timeout = 3600000;// 60000;
			hwr.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";

			HttpWebResponse response = (HttpWebResponse)hwr.GetResponse();
			if (File.Exists( pSaveFileName ))
			{
				File.Delete( pSaveFileName );
			}
			using (Stream dataStream = response.GetResponseStream())
			{
				FileStream outputStream = new FileStream( pSaveFileName, FileMode.Create );

				int bufferSize = 102400;
				int readCount;
				byte[] buffer = new byte[bufferSize];
				while ((readCount = dataStream.Read( buffer, 0, bufferSize )) > 0)
				{
					outputStream.Write( buffer, 0, readCount );
					//readCount = ftpStream.Read(buffer, 0, bufferSize);
				}

				dataStream.Close();
				outputStream.Close();
				response.Close();
			}

			retVal = true;

			return retVal;
		}

		/// <summary>
		/// Save Data as File
		/// </summary>
		/// <param name="pTargetName"></param>
		/// <returns></returns>
		public bool Save( string pTargetName )
		{
			bool retVal = false;

			if (!string.IsNullOrEmpty( this.Data ))
			{
				if (File.Exists( pTargetName ))
				{
					File.Delete( pTargetName );
				}

				File.WriteAllText( pTargetName, this.Data );
				retVal = true;
			}

			return retVal;
		}

		#region IDisposable Members

		public void Dispose()
		{
			this.Data = null;
		}

		#endregion IDisposable Members
	}
}