//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.Entities.Validation
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public sealed class CompareDateForm : ValidationAttribute
	{
		private string _dateToCompare = "DateFrom";

		public CompareDateForm()
			: base()
		{
			// Do nothing.
		}

		public CompareDateForm( string dateToCompare )
			: base()
		{
			_dateToCompare = dateToCompare;
		}

		protected override ValidationResult IsValid( object value, ValidationContext validationContext )
		{
			var dateToCompare = validationContext.ObjectType.GetProperty( _dateToCompare );
			var dateToCompareValue = dateToCompare.GetValue( validationContext.ObjectInstance, null );

			if (dateToCompareValue == null || value == null)
			{
				return null;
			}
			int dateTo = 0;

			if (Convert.ToString( value.GetType() ).ToLower().Equals( "system.string" ) || Convert.ToString( value.GetType() ).ToLower().Contains( "system.int" ))
			{
				dateTo = Convert.ToInt32( Convert.ToString( value ).Replace( "/", "" ) );
			}
			else if (Convert.ToString( value.GetType() ).ToLower().Equals( "system.datetime" ))
			{
				dateTo = Convert.ToInt32( Convert.ToDateTime( value ).ToString( "yyyyMMdd" ) );
			}

			int dateForm = 0;
			if (Convert.ToString( dateToCompareValue.GetType() ).ToLower().Equals( "system.string" ) || Convert.ToString( dateToCompareValue.GetType() ).ToLower().Contains( "system.int" ))
			{
				dateForm = Convert.ToInt32( Convert.ToString( dateToCompareValue ).Replace( "/", "" ) );
			}
			else if (Convert.ToString( dateToCompareValue.GetType() ).ToLower().Equals( "system.datetime" ))
			{
				dateForm = Convert.ToInt32( Convert.ToDateTime( dateToCompareValue ).ToString( "yyyyMMdd" ) );
			}

			if (dateTo < dateForm)
			{
				return new ValidationResult( FormatErrorMessage( validationContext.DisplayName ) );
			}
			return null;
		}
	}
}