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
	public class Decrypt
	{
		// This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
		// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
		// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
		private const string initVector = "GLV-RIKUSO@$#!32";

		// This constant is used to determine the keysize of the encryption algorithm.
		private const int keysize = 256;

		private const string passPhrase = "GLV-RIKUSO@$#!32";

		public static string GetDecryptStr( string strInput )
		{
			strInput = Uri.UnescapeDataString( strInput );
			byte[] initVectorBytes = Encoding.ASCII.GetBytes( initVector );
			byte[] cipherTextBytes = Convert.FromBase64String( strInput );
			PasswordDeriveBytes password = new PasswordDeriveBytes( passPhrase, null );
			byte[] keyBytes = password.GetBytes( keysize / 8 );
			RijndaelManaged symmetricKey = new RijndaelManaged();
			symmetricKey.Mode = CipherMode.CBC;
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor( keyBytes, initVectorBytes );
			MemoryStream memoryStream = new MemoryStream( cipherTextBytes );
			CryptoStream cryptoStream = new CryptoStream( memoryStream, decryptor, CryptoStreamMode.Read );
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];
			int decryptedByteCount = cryptoStream.Read( plainTextBytes, 0, plainTextBytes.Length );
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString( plainTextBytes, 0, decryptedByteCount );
		}
	}
}