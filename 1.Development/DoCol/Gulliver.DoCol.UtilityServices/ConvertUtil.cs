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
	using System.Text.RegularExpressions;

	public static class ConvertUtil
	{
		public static int? ConvertDateStringToDateInt( string dateString )
		{
			int value;
			if (String.IsNullOrEmpty( dateString ))
			{
				return null;
			}

			if (Int32.TryParse( dateString.Replace( "/", String.Empty ), out value ))
			{
				return value;
			}
			else
			{
				return null;
			}
		}

		public static string ConvertDateIntToDateString( int dateInt )
		{
			string dateString = Convert.ToString( dateInt );
			DateTime dateValue;
			if (dateInt < 10000000)
			{
				return null;
			}

			dateString = Convert.ToString( dateInt / 10000 )
							+ "/" + Convert.ToString( dateInt / 100 % 100 )
							+ "/" + Convert.ToString( dateInt % 100 );

			if (DateTime.TryParse( dateString, out dateValue ))
			{
				return dateString;
			}
			else
			{
				return null;
			}
		}

		public static int? ToNullableInt32( this string s )
		{
			int i;
			if (Int32.TryParse( s, out i )) return i;
			return null;
		}

		public static string Sanitize( string stringValue )
		{
			return stringValue == null ? null : stringValue
						.RegexReplace( "-{2,}", "-" )                 // transforms multiple --- in - use to comment in sql scripts
						.RegexReplace( @"[*/]+", string.Empty )      // removes / and * used also to comment in sql scripts
						.RegexReplace( @"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase )
						.RegexReplace( "\'", "" ).Trim();
		}

		private static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith)
        {
            return Regex.Replace(stringValue, matchPattern, toReplaceWith);
        }

        private static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith, RegexOptions regexOptions)
        {
            return Regex.Replace(stringValue, matchPattern, toReplaceWith, regexOptions);
        }

	}
}