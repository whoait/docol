//---------------------------------------------------------------------------
// System			: Gulliver
// Designer			: DatNT
// Programmer		: DatNT
// Created Date		: 2013/08/01
// Comment			:

#region ----------< History >------------------------------------------------

// ID				: 00x
// Designer			:
// Programmer		:
// Updated Date		:
// Comment			:

#endregion ----------< History >------------------------------------------------

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Gulliver.DoCol.UtilityServices
{
	/// <summary>
	/// Structure containing the files uploaded in the ftp
	/// </summary>
	public struct FileStruct
	{
		public string Flags;
		public string Owner;
		public string Group;
		public bool IsDirectory;
		public DateTime CreateTime;
		public long Size;
		public string Name;
		public string FullPathName;
		public string err;
	}

	public class FileManager
	{
		public long FileLength { get; set; }

		public bool Copy( string pSourceFileWithName, string pSaveFileWithName )
		{
			pSourceFileWithName = pSourceFileWithName.Replace( "/", "\\" );
			pSaveFileWithName = pSaveFileWithName.Replace( "/", "\\" );

			this.FileLength = 0;

			if (File.Exists( pSourceFileWithName ))
			{
				File.Copy( pSourceFileWithName, pSaveFileWithName, true );
				FileInfo fi = new FileInfo( pSourceFileWithName );
				this.FileLength = fi.Length;

				if (this.FileLength <= 0)
				{
					fi = new FileInfo( pSaveFileWithName );
					this.FileLength = fi.Length;
				}
				return true;
			}

			return false;
		}

		public bool Move( string pSourceFileWithName, string pSaveFileWithName )
		{
			pSourceFileWithName = pSourceFileWithName.Replace( "/", "\\" );
			pSaveFileWithName = pSaveFileWithName.Replace( "/", "\\" );

			this.FileLength = 0;

			if (File.Exists( pSourceFileWithName ))
			{
				File.Move( pSourceFileWithName, pSaveFileWithName );
				FileInfo fi = new FileInfo( pSourceFileWithName );
				this.FileLength = fi.Length;

				if (this.FileLength <= 0)
				{
					fi = new FileInfo( pSaveFileWithName );
					this.FileLength = fi.Length;
				}
				return true;
			}

			return false;
		}

		public bool DeleteFile( string pFileName )
		{
			pFileName = pFileName.Replace( "/", "\\" );
			if (File.Exists( pFileName ))
			{
				File.Delete( pFileName );
			}
			return true;
		}

		public static bool FileExists( string fileFullName )
		{
			fileFullName = fileFullName.Replace( "/", "\\" );
			return File.Exists( fileFullName );
		}

		public static string ReadAllText( string fileFullName )
		{
			fileFullName = fileFullName.Replace( "/", "\\" );
			if (FileExists( fileFullName ))
			{
				return File.ReadAllText( fileFullName );
			}
			else
			{
				return string.Empty;
			}
		}

		public static void CreateDirectory( string pDirectory )
		{
			pDirectory = pDirectory.Replace( "/", "\\" );
			if (!Directory.Exists( pDirectory ))
			{
				Directory.CreateDirectory( pDirectory );
			}
		}

		public static List<FileStruct> GetDirectoryFiles( string pRootDirectory )
		{
			pRootDirectory = pRootDirectory.Replace( "/", "\\" );

			List<FileStruct> myListArray = new List<FileStruct>();
			if (Directory.Exists( pRootDirectory ))
			{
				string[] dataRecords = Directory.GetFiles( pRootDirectory.Replace( "/", "\\" ) );
				//FileListStyle _directoryListStyle = GuessFileListStyle(dataRecords);
				foreach (string s in dataRecords)
				{
					if (!String.IsNullOrEmpty( s ))
					{
						string fileNameFullPath = s.ToLower();
						FileStruct f = new FileStruct();
						f.FullPathName = fileNameFullPath;
						f.Name = Path.GetFileName( fileNameFullPath );
						myListArray.Add( f );
					}
				}
			}
			else
			{
				Console.WriteLine( "Dir nt exist:" + pRootDirectory );
			}
			return myListArray;
		}

		public static string UploadFile( HttpPostedFile file, string path )
		{
			if (file == null)
			{
				return string.Empty;
			}

			string bucketName = "media.cloud.glv.co.jp";

			try
			{
				using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client( RegionEndpoint.APNortheast1 ))
				{
					PutObjectRequest request = new PutObjectRequest()
					{
						BucketName = bucketName,
						Key = path,
						InputStream = file.InputStream
					};

					PutObjectResponse response = client.PutObject( request );

					return path;
				}
			}
			catch (AmazonS3Exception)
			{
				return null;
			}
		}

		public static bool Exists( string path, string bucketName, IAmazonS3 client )
		{
			try
			{
				//client.GetObjectMetadata( bucketName, path );

				return true;
			}
			catch (AmazonS3Exception ex)
			{
				if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
					return false;

				//status wasn't not found, so throw the exception
				throw;
			}
		}

		public static string CreateFolder( string subPath )
		{
			try
			{
				string result = string.Empty;
				string folderPath = string.Empty;
				string bucketName = "media.cloud.glv.co.jp";

				// Get list string folder
				string[] listFolder = System.IO.Path.GetDirectoryName( subPath ).Split( System.IO.Path.DirectorySeparatorChar );

				foreach (string folder in listFolder)
					if (!string.IsNullOrWhiteSpace( folder ))
					{
						// Create path
						folderPath += folder + "/";

						// Create client S3
						IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client( RegionEndpoint.APNortheast1 );

						if (!Exists( folderPath, bucketName, client ))
						{
							PutObjectRequest request = new PutObjectRequest()
							{
								BucketName = bucketName,
								Key = folderPath,
								ContentBody = ""
							};

							// Set request
							PutObjectResponse response = client.PutObject( request );

							// Get result
							result = response.HttpStatusCode.ToString();
						}
					}

				return result;
			}
			catch (AmazonS3Exception ex)
			{
				return "Error S3 :" + ex.Message;
			}
		}

		public static string CopyingObject( string sourceKey, string destinationKey )
		{
			string bucketName = "media.cloud.glv.co.jp";

			try
			{
				using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client( RegionEndpoint.APNortheast1 ))
				{
					CopyObjectRequest request = new CopyObjectRequest
					{
						SourceBucket = bucketName,
						SourceKey = sourceKey,
						DestinationBucket = bucketName,
						DestinationKey = destinationKey
					};
					CopyObjectResponse response = client.CopyObject( request );
					return destinationKey;
				}
			}
			catch (AmazonS3Exception)
			{
				return null;
			}
		}

		public static string UploadImageTemp( HttpPostedFile file, string subPath )
		{
			CreateFolder( subPath );

			return UploadFile( file, string.Format( "{0}/{1}", "seibi/org/img_temp", subPath ) );
		}

		public static string UploadImage( string sourceKey )
		{
			string destinationKey = sourceKey.Replace( "img_temp", "img" );

			CreateFolder( destinationKey );

			return CopyingObject( sourceKey, destinationKey );
		}

		public static string UploadAttachFile( HttpPostedFile file, string subPath )
		{
			CreateFolder( subPath );

			return UploadFile( file, string.Format( "{0}/{1}", "seibi/org/etc", subPath ) );
		}

		public static string ReadFile( string key, string subPath )
		{
			string mediaPath = "http://media.221616.com/" + subPath;
			return mediaPath;
		}

		public static void DownloadFileFromMedia( string filePath, string fileName )
		{
			////Check extention
			//fileName = fileName.IndexOf( "." ) != -1 ? fileName : fileName + filePath.Substring( filePath.LastIndexOf( "." ) );

			//Create a stream for the file
			Stream stream = null;

			//This controls how many bytes to read at a time and send to the client
			int bytesToRead = 10000;

			// Buffer to read bytes in chunk size specified above
			byte[] buffer = new Byte[bytesToRead];

			// The number of bytes read
			try
			{
				string bucketName = "media.cloud.glv.co.jp";

				try
				{
					using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client( RegionEndpoint.APNortheast1 ))
					{
						GetObjectRequest request = new GetObjectRequest()
						{
							BucketName = bucketName,
							Key = filePath,
						};

						GetObjectResponse response = client.GetObject( request );

						if (response != null)
						{
							// Get the Stream returned from the response
							stream = response.ResponseStream;

							// Prepare the response to the client. resp is the client Response
							var resp = HttpContext.Current.Response;

							// Get file type of path
							string fileType = Path.GetExtension( filePath );

							// If pdf add header inline
							if (fileType.Equals( ".pdf" ))
							{
								// Indicate the type of data being sent
								resp.ContentType = "application/pdf";

								// Name the file
								resp.AddHeader( "Content-Disposition", "inline; filename=\"" + fileName + "\"" );
							}
							else
							{
								// Indicate the type of data being sent
								resp.ContentType = "application/octet-stream";

								// Name the file
								resp.AddHeader( "Content-Disposition", "attachment; filename=\"" + fileName + "\"" );
							}

							resp.AddHeader( "Content-Length", stream.Length.ToString() );

							int length;
							do
							{
								// Verify that the client is connected.
								if (resp.IsClientConnected)
								{
									// Read data into the buffer.
									length = stream.Read( buffer, 0, bytesToRead );

									// and write it out to the response's output stream
									resp.OutputStream.Write( buffer, 0, length );

									// Flush the data
									resp.Flush();

									// Clear the buffer
									buffer = new Byte[bytesToRead];
								}
								else
								{
									// Cancel the download if client has disconnected
									length = -1;
								}
							} while (length > 0); // Repeat until no data is read
						}
					}
				}
				catch (AmazonS3Exception ex)
				{
					throw ex;
				}
			}
			finally
			{
				if (stream != null)
				{
					// Close the input stream
					stream.Close();
				}
			}
		}

		public static MemoryStream ExportFileZip( List<string> nameFile, List<Stream> listStreamFile, string fileZipName )
		{
			// create new zip instance
			ZipFile zip = new ZipFile();
			zip.UseUnicodeAsNecessary = true;

			// create new stream
			MemoryStream stream = new MemoryStream();
			int i = 0;
			if (listStreamFile != null)
			{
				foreach (Stream item in listStreamFile)
				{
					string fileName = nameFile[i].ToString();
					zip.AddEntry( fileName, fileZipName, item );
					zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
					i++;
				}
			}

			zip.Save( stream );
			return stream;
		}

		public static void DownLoadFileZip( List<string> listfilePath, List<string> listfileName, string fileZipName )
		{
			// Create a stream for the file
			Stream stream = null;

			// Create list stream for the file
			List<Stream> listStream = new List<Stream>();

			//This controls how many bytes to read at a time and send to the client
			int bytesToRead = 10000;

			// The number of bytes read
			try
			{
				// Buffer to read bytes in chunk size specified above
				byte[] buffer = new Byte[bytesToRead];

				string bucketName = "media.cloud.glv.co.jp";

				try
				{
					for (int i = 0; i < listfilePath.Count; i++)
					{
						using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client( RegionEndpoint.APNortheast1 ))
						{
							GetObjectRequest request = new GetObjectRequest()
							{
								BucketName = bucketName,
								Key =  listfilePath[i],
							};

							GetObjectResponse response = client.GetObject( request );

							if (response != null)
							{
								// Get the Stream returned from the response
								stream = response.ResponseStream;
							}
						}
						listStream.Add( stream );
					}

					MemoryStream ms = ExportFileZip( listfileName, listStream, fileZipName );

					// Prepare the response to the client. resp is the client Response
					var resp = HttpContext.Current.Response;

					//// Indicate the type of data being sent
					//resp.ContentType = "application/octet-stream";

					// Name the file
					resp.AddHeader( "Content-Disposition", "attachment; filename=\"" + fileZipName + "\"" );
					resp.ContentType = "application/x-zip-compressed";
					//resp.AddHeader( "Content-Length", ms.Length.ToString() );

					if (resp.IsClientConnected)
					{
						resp.BinaryWrite( ms.ToArray() );
					}

					//int length;
					//do
					//{
					//	// Verify that the client is connected.
					//	if (resp.IsClientConnected)
					//	{
					//		// Read data into the buffer.
					//		length = ms.Read( buffer, 0, bytesToRead );

					//		// and write it out to the response's output stream
					//		resp.OutputStream.Write( buffer, 0, length );

					//		// Flush the data
					//		resp.Flush();

					//		// Clear the buffer
					//		buffer = new Byte[bytesToRead];
					//	}
					//	else
					//	{
					//		// Cancel the download if client has disconnected
					//		length = -1;
					//	}
					//} while (length > 0); // Repeat until no data is read

				}
				catch (AmazonS3Exception ex)
				{
					throw ex;
				}
			}
			finally
			{
				if (stream != null)
				{
					// Close the input stream
					stream.Close();
				}
			}
		}

	}
}