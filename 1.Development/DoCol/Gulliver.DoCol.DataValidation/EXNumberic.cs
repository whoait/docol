//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.DataValidation
{
	using Gulliver.DoCol.MessageUtility;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Resources;
	using System.Web.Mvc;

	/// <summary>
	/// Regex numeric
	/// </summary>
	public class EXNumberic : RegularExpressionAttribute, IClientValidatable
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
		/// The Regex string
		/// </summary>
		private string _regEx = string.Empty;

		/// <summary>
		/// The property name
		/// </summary>
		private string _PropertyName = string.Empty;

		//public static List<MessageModel> lstAllMessageNumberic;

		/// <summary>
		/// Initializes a new instance of the <see cref="EXRegex"/> class.
		/// </summary>
		/// <param name="msgCd">The MSG cd.</param>
		/// <param name="msgReplaceStr">The MSG replace string.</param>
		/// <param name="regEx">The regular expression string</param>
		public EXNumberic( string msgCd, string msgReplaceStr = "" )
			: base( "^[0-9]*$" )
		{
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
			this._regEx = "^[0-9]*$";
		}

		public EXNumberic( string msgCd, Type msgReplaceStrResourceType, string msgReplaceStrResourceName )
			: base( "^[0-9]*$" )
		{
			ResourceManager resourceManager = new ResourceManager( msgReplaceStrResourceType );
			this._msgReplaceStr = resourceManager.GetString( msgReplaceStrResourceName );
			this._msgCd = msgCd;
			this._regEx = "^[0-9]*$";
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
			rule.ValidationParameters.Add( "regexnumberic", this._regEx );
			rule.ValidationType = "exnumberic";
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

			//if (lstAllMessageNumberic == null)
			//{
			//	lstAllMessageNumberic = MessageService.GetAllMessage();
			//}

			//string messageContent = MessageService.GetMessageContentByID( lstAllMessageNumberic, this._msgCd );

			//return MessageService.GetMessageContent( this._msgCd, this._msgReplaceStr, messageContent );
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
			if (base.IsValid( value, validationContext ) != null)
			{
				new ValidUtility().SaveError( _PropertyName, ValidUtility.Type.EXNumberic, FormatErrorMessage( validationContext.DisplayName ) );
			}
			return base.IsValid( value, validationContext );
		}
	}
}