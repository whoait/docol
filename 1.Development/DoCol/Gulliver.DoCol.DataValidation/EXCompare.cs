//---------------------------------------------------------------------------
// Version			: 001
// Designer			: DungNH6-FPT
// Programmer		: DungNH6-FPT
// Date				: 2015/05/27
// Comment			: Create new
//---------------------------------------------------------------------------
namespace Gulliver.DoCol.DataValidation
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Resources;
	using System.Web.Mvc;
	using Gulliver.DoCol.MessageUtility;

	public class EXCompare : ValidationAttribute, IClientValidatable
	{
		private readonly string _objectToCompare = "MileageKmStart";

		private readonly string _msgCd = string.Empty;

		private readonly string _msgReplaceStr = string.Empty;

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
			this._PropertyName = metadata.PropertyName;
			var rule = new ModelClientValidationRule { ErrorMessage = this.FormatErrorMessage( metadata.GetDisplayName() ) };
			rule.ValidationParameters.Add( "fromobjectname", this._objectToCompare );
			rule.ValidationType = "excompare";
			yield return rule;
		}

		public EXCompare( string msgCd, string msgReplaceStr = "" )
			: base()
		{
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
		}

		public EXCompare( string objectToCompare, string msgCd, string msgReplaceStr = "" )
			: base()
		{
			this._objectToCompare = objectToCompare;
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
		}

		public EXCompare( string objectToCompare, string msgCd, Type msgReplaceStrResourceType, string msgReplaceStrResourceName )
			: base()
		{
			ResourceManager resourceManager = new ResourceManager( msgReplaceStrResourceType );
			this._msgReplaceStr = resourceManager.GetString( msgReplaceStrResourceName );
			this._msgCd = msgCd;
			this._objectToCompare = objectToCompare;
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
			var objectToCompare = validationContext.ObjectType.GetProperty( this._objectToCompare );
			var objectToCompareValue = objectToCompare.GetValue( validationContext.ObjectInstance, null );

			if (objectToCompareValue == null || value == null)
			{
				return null;
			}

			int objectRootValue = 0, objectToValue = 0;
			bool checkConvert = true;
			string typeOfObjectRoot = Convert.ToString( value.GetType() ).ToLower();
			string typeOfObjectTo = Convert.ToString( objectToCompareValue.GetType() ).ToLower();

			if (typeOfObjectRoot.Equals( "system.int" ) || typeOfObjectRoot.Equals( "system.int32" ))
			{
				checkConvert = Int32.TryParse( Convert.ToString( value ), out objectToValue );
			}
			else if (Convert.ToString( value.GetType() ).ToLower().Equals( "system.datetime" ))
			{
				checkConvert = Int32.TryParse( Convert.ToDateTime( value ).ToString( "yyyyMMdd" ), out objectToValue );
			}

			if (typeOfObjectTo.Equals( "system.int" ) || typeOfObjectTo.Equals( "system.int32" ))
			{
				checkConvert = Int32.TryParse( Convert.ToString( objectToCompareValue ), out objectRootValue );
			}
			else if (Convert.ToString( value.GetType() ).ToLower().Equals( "system.datetime" ))
			{
				checkConvert = Int32.TryParse( Convert.ToDateTime( objectToCompareValue ).ToString( "yyyyMMdd" ), out objectRootValue );
			}

			if (!checkConvert)
			{
				return null;
			}

			if (objectToValue < objectRootValue)
			{
				new ValidUtility().SaveError( this._PropertyName, ValidUtility.Type.EXCompare, this.FormatErrorMessage( validationContext.DisplayName ) );
				return new ValidationResult( this.FormatErrorMessage( validationContext.DisplayName ) );
			}

			return null;
		}
	}
}