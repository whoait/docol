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
using System.Text;
using System.Web.Mvc;

namespace Gulliver.DoCol.DataValidation
{
	public class EXHalfSize : ValidationAttribute, IClientValidatable
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
		/// Initializes a new instance of the <see cref="EXRequired" /> class.
		/// </summary>
		/// <param name="msgCd">The MSG cd.</param>
		/// <param name="msgReplaceStr">The MSG replace string.</param>
		public EXHalfSize( string msgCd, string msgReplaceStr = "" )
		{
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EXFullSize"/> class.
		/// </summary>
		/// <param name="msgCd">The MSG cd.</param>
		/// <param name="msgReplaceStrResourceType">Type of the MSG replace string resource.</param>
		/// <param name="msgReplaceStrResourceName">Name of the MSG replace string resource.</param>
		public EXHalfSize( string msgCd, Type msgReplaceStrResourceType, string msgReplaceStrResourceName )
			: base()
		{
			ResourceManager resourceManager = new ResourceManager( msgReplaceStrResourceType );
			this._msgReplaceStr = resourceManager.GetString( msgReplaceStrResourceName );
			this._msgCd = msgCd;
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
			rule.ValidationParameters.Add( "other", "" );
			rule.ValidationType = "exhalfsize";
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
			if (string.IsNullOrEmpty( Convert.ToString( value ) ))
			{
				return null;
			}
			if (Encoding.GetEncoding( "shift-jis" ).GetBytes( Convert.ToString( value ) ).Length > Convert.ToString( value ).Length)
			{
				new ValidUtility().SaveError( _PropertyName, ValidUtility.Type.EXHalfSize, FormatErrorMessage( validationContext.DisplayName ) );
				return new ValidationResult( FormatErrorMessage( validationContext.DisplayName ) );
			}
			return null;
		}
	}
}