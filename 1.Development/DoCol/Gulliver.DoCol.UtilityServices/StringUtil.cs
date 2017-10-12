//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.UtilityServices
{
	using System;

	public static partial class StringUtil
	{
		public static string RTrim( string stringValue )
		{
			if (String.IsNullOrEmpty( stringValue ))
			{
				return stringValue;
			}
			else
			{
				return stringValue.TrimEnd();
			}
		}

		public static string TrimString( string inputString, int lenghRequire )
		{
			if (inputString.Length > lenghRequire)
				return inputString.Remove( lenghRequire ) + "...";
			else
				return inputString;
		}

		/// <summary>
		/// Cut string with left lenght.
		/// </summary>
		/// <param name="str">
		/// The str.
		/// </param>
		/// <param name="length">
		/// The length.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public static string Left(this string str, int length)
        {
            str = str ?? string.Empty;
            return str.Substring(0, Math.Min(length, str.Length));
        }

		/// <summary>
		/// Cut string with right lenght.
		/// </summary>
		/// <param name="str">
		/// The str.
		/// </param>
		/// <param name="length">
		/// The length.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public static string Right(this string str, int length)
        {
            str = str ?? string.Empty;
            return (str.Length >= length)
                ? str.Substring(str.Length - length, length)
                : str;
        }
	}
}