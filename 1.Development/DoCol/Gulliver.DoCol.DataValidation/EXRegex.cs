﻿//---------------------------------------------------------------------------
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
	///
	/// </summary>
	public class EXRegex : RegularExpressionAttribute, IClientValidatable
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

		/// <summary>
		/// Initializes a new instance of the <see cref="EXRegex"/> class.
		/// </summary>
		/// <param name="msgCd">The MSG cd.</param>
		/// <param name="msgReplaceStr">The MSG replace string.</param>
		/// <param name="regEx">The regular expression string</param>
		public EXRegex( string regEx, string msgCd, string msgReplaceStr = "" )
			: base( regEx )
		{
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
			this._regEx = regEx;
		}

		public EXRegex( string regEx, string msgCd, Type msgReplaceStrResourceType, string msgReplaceStrResourceName )
			: base( regEx )
		{
			ResourceManager resourceManager = new ResourceManager( msgReplaceStrResourceType );
			this._msgReplaceStr = resourceManager.GetString( msgReplaceStrResourceName );
			this._msgCd = msgCd;
			this._regEx = regEx;
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
			rule.ValidationParameters.Add( "regex", this._regEx );
			rule.ValidationType = "exregex";
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
			if (base.IsValid( value, validationContext ) != null)
			{
				new ValidUtility().SaveError( _PropertyName, ValidUtility.Type.EXRegex, FormatErrorMessage( validationContext.DisplayName ) );
			}
			return base.IsValid( value, validationContext );
		}
	}
}