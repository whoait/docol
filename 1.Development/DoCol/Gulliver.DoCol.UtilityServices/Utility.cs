//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------
//---------------------------------------------------------------------------
// Version			: 002
// Designer			: DungNH6-FPT
// Programmer		: DungNH6-FPT
// Date				: 2015/04/14
// Comment			: Update function ConvertHalfToFullsize and create function ConvertFullToHalfsize
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.UtilityServices
{
	using System;
	using System.Globalization;

	using Microsoft.VisualBasic;

	/// <summary>
	/// The utility.
	/// </summary>
	public static partial class Utility
	{
		/// <summary>
		/// The is content equal.
		/// </summary>
		/// <param name="listItems">
		/// The list items.
		/// </param>
		/// <param name="item">
		/// The item.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public static bool IsContentEqual( string listItems, string item )
		{
			var ci = CompareInfo.GetCompareInfo( "ja-JP" );
			return ci.Compare( listItems, item, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase ) == 0;
		}

		/// <summary>
		/// The is content match.
		/// </summary>
		/// <param name="listItems">
		/// The list items.
		/// </param>
		/// <param name="item">
		/// The item.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public static bool IsContentMatch( string listItems, string item )
		{
			int listItemsLength = listItems.Length;
			int itemLength = item.Length;
			CompareInfo ci = CompareInfo.GetCompareInfo( "ja-JP" );

			if (listItemsLength < itemLength)
			{
				return false;
			}

			for (int i = 0; i <= listItemsLength - itemLength; i++)
			{
				string index = listItems.Substring( i, itemLength );
				if (ci.Compare( index, item, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase ) == 0)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// The convert half to fullsize.
		/// </summary>
		/// <param name="halfsizeString">
		/// The half size string.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public static string ConvertHalfToFullsize( string halfsizeString )
		{
			return Strings.StrConv( halfsizeString, VbStrConv.Wide, 1041 );
		}

		/// <summary>
		/// The convert fullsize to halfsize.
		/// </summary>
		/// <param name="fullsizeString">
		/// The full size string.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public static string ConvertFullToHalfsize( string fullsizeString )
		{
			return Strings.StrConv( fullsizeString, VbStrConv.Narrow, 1041 );
		}

		public static string ConvertDateToStringWithFormat( DateTime? inputDateTime, string formatDateTime = "yyyy/MM/dd" )
		{
			return inputDateTime == null ? string.Empty : inputDateTime.Value.ToString( formatDateTime );
		}

		public static string JoinStringWithSuffix( string inputString, string suffix )
		{
			return inputString == null ? string.Empty : inputString + suffix;
		}

		public static string JoinStringWithSuffix( int? inputString, string suffix )
		{
			return inputString == null ? string.Empty : JoinStringWithSuffix( inputString.Value.ToString(), suffix );
		}

		public static string CompareObjectDecimalAndReturnString( string object1, string object2, string returnValueIfEqual = "" )
		{
			double objectDecimal1, objectDecimal2;
			if (double.TryParse( object1, out objectDecimal1 )
				&& double.TryParse( object2, out objectDecimal2 ))
			{
				return (objectDecimal1 > objectDecimal2) ? returnValueIfEqual : string.Empty;
			}

			return string.Empty;
		}

		public static string ConvertDecimalToStringWithFormat( decimal? inputValue, string suffixesUnit = "" )
		{
			return inputValue == null ? string.Empty : inputValue.Value.ToString( "#,#", CultureInfo.InvariantCulture.NumberFormat ) + suffixesUnit;
		}

		public static string ConvertDecimalToStringWithFormat( object inputValue, string suffixesUnit = "" )
		{
			if (inputValue == null)
			{
				return string.Empty;
			}
			else
			{
				decimal value = 0;
				bool fv = decimal.TryParse( inputValue.ToString(), out value );
				return value.ToString( "#,#", CultureInfo.InvariantCulture.NumberFormat ) + suffixesUnit;
			}
		}

		/// <summary>
		/// Function compare two object
		/// </summary>
		/// <param name="object1">object 1</param>
		/// <param name="object2">object 2</param>
		/// <returns>
		/// return 2: object1 > object 2
		/// return 1: object1 = object 2
		/// return 0: ...
		/// </returns>
		public static int CompareObjectTypeInt( string object1, string object2 )
		{
			int objectDecimal1, objectDecimal2;
			if (int.TryParse( object1, out objectDecimal1 )
				&& int.TryParse( object2, out objectDecimal2 ))
			{
				return (objectDecimal1 > objectDecimal2) ? 2 : (objectDecimal1 < objectDecimal2 ? 0 : 1);
			}

			return -1;
		}
	}
}