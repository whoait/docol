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
	using System.Reflection;
	using System.Resources;
	using System.Web.Mvc;
	using System.Linq;

	/// <summary>
	///
	/// </summary>
	public class EXRequiredGroup : ValidationAttribute, IClientValidatable
	{
		public string GroupName { get; set; }

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
		/// Initializes a new instance of the <see cref="EXRequired"/> class.
		/// </summary>
		/// <param name="msgCd">The MSG cd.</param>
		/// <param name="msgReplaceStr">The MSG replace string.</param>
		public EXRequiredGroup( string groupName, string msgCd, string msgReplaceStr = "" )
		{
			this.GroupName = groupName;
			this._msgCd = msgCd;
			this._msgReplaceStr = msgReplaceStr;
		}

		public EXRequiredGroup( string groupName, string msgCd, Type msgReplaceStrResourceType, string msgReplaceStrResourceName )
			: base()
		{
			this.GroupName = groupName;
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
			var rule = new ModelClientValidationRule()
			{
				ErrorMessage = FormatErrorMessage( metadata.GetDisplayName() ),
			};

			var groupProperties = GetGroupProperties( metadata.ContainerType ).Select( p => p.Name );

			rule.ValidationType = string.Format( "exrequiredgroup", GroupName.ToLower() );
			rule.ValidationParameters["propertynames"] = string.Join( ",", groupProperties );
			yield return rule;
		}

		private IEnumerable<PropertyInfo> GetGroupProperties( Type type )
		{
			return
				from property in type.GetProperties()
				let attributes = property.GetCustomAttributes( typeof( EXRequiredGroup ), false ).OfType<EXRequiredGroup>()
				where attributes.Count() > 0
				from attribute in attributes
				where attribute.GroupName == this.GroupName
				select property;
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
		//protected override ValidationResult IsValid( object value, ValidationContext validationContext )
		//{
		//	if (base.IsValid( value, validationContext ) != null)
		//	{
		//		new ValidUtility().SaveError( _PropertyName, ValidUtility.Type.EXRequired, FormatErrorMessage( validationContext.DisplayName ) );
		//	}
		//	return base.IsValid( value, validationContext );
		//}
		protected override ValidationResult IsValid( object value, ValidationContext validationContext )
		{
			//bool isValid = true;
			//foreach (var property in GetGroupProperties( validationContext.ObjectType ))
			//{
			//	var propertyValue = property.GetValue( validationContext.ObjectInstance);
			//	if (propertyValue == null && Convert.ToBoolean(propertyValue) == false)
			//	{
			//		isValid = false;
			//		break;
			//	}
			//}

			//if (isValid)
			//{
			//	new ValidUtility().SaveError( _PropertyName, ValidUtility.Type.EXRequired, FormatErrorMessage( validationContext.DisplayName ) );
			//}
			//return base.IsValid( value, validationContext );
			return ValidationResult.Success;
		}
	}
}