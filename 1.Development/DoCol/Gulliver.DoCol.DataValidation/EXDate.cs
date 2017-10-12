// --------------------------------------------------------------------------------------------------------------------
// Version		: 001
// Designer		:
// Programmer	: TungTNS
// Date			: 2013/10/07
// Comment		: Create new
// --------------------------------------------------------------------------------------------------------------------

namespace Gulliver.DoCol.DataValidation
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Globalization;
	using System.Resources;
	using System.Web.Mvc;
	using Gulliver.DoCol.MessageUtility;

	/// <summary>
	/// Regex numeric
	/// </summary>
	public class EXDate : ValidationAttribute, IClientValidatable
	{
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
		/// The property name
		/// </summary>
		private string _formatString = string.Empty;

		/// <summary>
		/// Initializes a new instance of the <see cref="EXDate"/> class. 
		/// Initializes a new instance of the <see cref="EXRegex"/> class.
		/// </summary>
		/// <param name="msgCd">
		/// The MSG cd.
		/// </param>
		/// <param name="msgReplaceStr">
		/// The MSG replace string.
		/// </param>
		public EXDate( string msgCd, string msgReplaceStr = "" )
			: base()
		{
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
			this._formatString = "yyyy/MM/dd H:mm:ss";
		}

		public EXDate( string msgCd, Type msgReplaceStrResourceType, string msgReplaceStrResourceName )
			: base()
		{
			ResourceManager resourceManager = new ResourceManager( msgReplaceStrResourceType );
			this._msgReplaceStr = resourceManager.GetString( msgReplaceStrResourceName );
			this._msgCd = msgCd;
			this._formatString = "yyyy/MM/dd H:mm:ss";
		}

		public EXDate( string msgCd, string formatString, string msgReplaceStr = "" )
			: base()
		{
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
			this._formatString = formatString ?? "yyyy/MM/dd H:mm:ss";
		}

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
			rule.ValidationParameters.Add( "other", "111" );
			rule.ValidationType = "exdate";
			yield return rule;
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

		/// <summary>
		/// Validates the specified value with respect to the current validation attribute.
		/// </summary>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context information about the validation operation.</param>
		/// <returns>
		/// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
		/// </returns>
		protected override ValidationResult IsValid( object value, ValidationContext validationContext )
		{
			DateTime date;
			
			if (value == null)
				return ValidationResult.Success;

			bool parsed = DateTime.TryParseExact( Convert.ToString( value ), this._formatString, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out date );
			if (!parsed)
			{
				new ValidUtility().SaveError( _PropertyName, ValidUtility.Type.EXSDate, FormatErrorMessage( validationContext.DisplayName ) );
				return new ValidationResult( FormatErrorMessage( validationContext.DisplayName ), new[] { validationContext.MemberName } );
			}
			return ValidationResult.Success;
		}
	}
}