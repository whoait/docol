//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using Gulliver.DoCol.MessageUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Web.Mvc;

namespace Gulliver.DoCol.DataValidation
{
	public class EXCompareDateForm : ValidationAttribute, IClientValidatable
	{
		private string _dateToCompare = "DateFrom";

		/// <summary>
		/// The _MSG cd
		/// </summary>
		private string _msgCd = string.Empty;

		/// <summary>
		/// The _MSG replace string
		/// </summary>
		private string _msgReplaceStr = string.Empty;

		/// <summary>
		/// The property name
		/// </summary>
		private string _PropertyName = string.Empty;

		/// <summary>
		/// When implemented in a class, returns client validation rules for that class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The controller context.</param>
		/// <returns>
		/// The client validation rules for this validator.
		/// </returns>
		public IEnumerable<ModelClientValidationRule> GetClientValidationRules( ModelMetadata metadata, ControllerContext context )
		{
			_PropertyName = metadata.PropertyName;
			var rule = new ModelClientValidationRule();
			rule.ErrorMessage = FormatErrorMessage( metadata.GetDisplayName() );
			rule.ValidationParameters.Add( "fromdatename", _dateToCompare );
			rule.ValidationType = "excomparedateform";
			yield return rule;
		}

		public EXCompareDateForm( string msgCd, string msgReplaceStr = "" )
			: base()
		{
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
		}

		public EXCompareDateForm( string dateToCompare, string msgCd, string msgReplaceStr = "" )
			: base()
		{
			this._dateToCompare = dateToCompare;
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
		}

		public EXCompareDateForm( string dateToCompare, string msgCd, Type msgReplaceStrResourceType, string msgReplaceStrResourceName )
			: base()
		{
			ResourceManager resourceManager = new ResourceManager( msgReplaceStrResourceType );
			this._msgReplaceStr = resourceManager.GetString( msgReplaceStrResourceName );
			this._msgCd = msgCd;
			this._dateToCompare = dateToCompare;
		}

		/// <summary>
		/// Applies formatting to an error message, based on the data field where the error occurred.
		/// </summary>
		/// <param name="name">The name to include in the formatted message.</param>
		/// <returns>
		/// An instance of the formatted error message.
		/// </returns>
		public override string FormatErrorMessage( string name )
		{
			return MessageService.GetMessage( this._msgCd, this._msgReplaceStr );
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
				new ValidUtility().SaveError( _PropertyName, ValidUtility.Type.EXCompareDateForm, FormatErrorMessage( validationContext.DisplayName ) );
				return new ValidationResult( FormatErrorMessage( validationContext.DisplayName ) );
			}
			return null;
		}
	}
}