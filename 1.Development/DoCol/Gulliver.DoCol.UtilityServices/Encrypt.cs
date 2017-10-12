//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Gulliver.DoCol.UtilityServices.security
{
	public class Encrypt
	{
		// This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
		// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
		// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
		private const string initVector = "GLV-RIKUSO@$#!32";

		// This constant is used to determine the keysize of the encryption algorithm.
		private const int keysize = 256;

		private const string passPhrase = "GLV-RIKUSO@$#!32";

		public static string GetEncryptStr( string strInput )
		{
			byte[] initVectorBytes = Encoding.UTF8.GetBytes( initVector );
			byte[] plainTextBytes = Encoding.UTF8.GetBytes( strInput );
			PasswordDeriveBytes password = new PasswordDeriveBytes( passPhrase, null );
			byte[] keyBytes = password.GetBytes( keysize / 8 );
			RijndaelManaged symmetricKey = new RijndaelManaged();
			symmetricKey.Mode = CipherMode.CBC;
			ICryptoTransform encryptor = symmetricKey.CreateEncryptor( keyBytes, initVectorBytes );
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream( memoryStream, encryptor, CryptoStreamMode.Write );
			cryptoStream.Write( plainTextBytes, 0, plainTextBytes.Length );
			cryptoStream.FlushFinalBlock();
			byte[] cipherTextBytes = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String( cipherTextBytes );
		}

		public static string GetUrlEncryptStr( string strInput )
		{
			return Uri.EscapeDataString( GetEncryptStr( strInput ) );
		}

		/// <summary>
		/// Get string encryption base on SHA1
		/// </summary>
		/// <param name="strInput">Input text</param>
		/// <returns>Data Encryption</returns>
		public static string GetEncryptionSHA1( string strInput )
		{
			// Covert input string to byte array
			byte[] data = Encoding.Default.GetBytes( strInput );

			// Encrypt byte data
			byte[] hashdata = SHA1.Create().ComputeHash( data );

			// Return ASCII data
			return Encoding.ASCII.GetString( hashdata, 0, hashdata.Length );
		}
	}
}