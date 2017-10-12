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
	public class EXRequiredIfConstraint : ConditionalAttributeBase, IClientValidatable
	{
		private RequiredAttribute _innerAttribute = new RequiredAttribute();

		public string DependentProperty { get; set; }

		public object TargetValue { get; set; }

		// The Massage cd
		private string msgCd = string.Empty;

		// The Massage replace string
		private string msgReplaceStr = string.Empty;

		// The property name
		private string propertyName = string.Empty;

		// The property hasRadioButton
		private bool hasRadioButton = false;

		public EXRequiredIfConstraint( string dependentProperty, object targetValue )
			: this( dependentProperty, targetValue, null )
		{
		}

		public EXRequiredIfConstraint( string dependentProperty, object targetValue, string errorMessage )
			: base( errorMessage )
		{
			this.DependentProperty = dependentProperty;
			this.TargetValue = targetValue;
		}

		public EXRequiredIfConstraint( string dependentProperty, object targetValue, string msgCd, Type msgReplaceStrResourceType, string msgReplaceStrResourceName )
			: this( dependentProperty, targetValue, null )
		{
			ResourceManager resourceManager = new ResourceManager( msgReplaceStrResourceType );
			this.msgReplaceStr = resourceManager.GetString( msgReplaceStrResourceName );
			this.msgCd = msgCd;
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules( ModelMetadata metadata, ControllerContext context )
		{
			this.propertyName = metadata.PropertyName;
			var rule = new ModelClientValidationRule()
			{
				ErrorMessage = FormatErrorMessage( metadata.GetDisplayName() ),
				ValidationType = "exrequiredifconstraint",
			};

			string depProp = BuildDependentPropertyId( metadata, context as ViewContext );

			// find the value on the control we depend on;
			// if it's a bool, format it javascript style
			// (the default is True or False!)
			string targetValue = (this.TargetValue ?? "").ToString();
			if (this.TargetValue != null && this.TargetValue.GetType() == typeof( bool ))
				targetValue = targetValue.ToLower();

			rule.ValidationParameters.Add( "dependentproperty", depProp );
			rule.ValidationParameters.Add( "targetvalue", targetValue );

			yield return rule;
		}

		private string BuildDependentPropertyId( ModelMetadata metadata, ViewContext viewContext )
		{
			return QualifyFieldId( metadata, this.DependentProperty, viewContext );
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
			return MessageService.GetMessage( this.msgCd, this.msgReplaceStr );
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
			// check if the current value matches the target value
			if (ShouldRunValidation( value, this.DependentProperty, this.TargetValue, validationContext ))
			{
				//TODO: Check Dropdownlist, Textbox has value 0
				int dropdownValue;
				if (Int32.TryParse( Convert.ToString( value ), out dropdownValue ))
				{
					value = dropdownValue == 0 ? string.Empty : value;
				}

				// match => means we should try validating this field
				if (!_innerAttribute.IsValid( value ))
				{
					// validation failed - return an error
					new ValidUtility().SaveError( this.propertyName, ValidUtility.Type.EXRequired, FormatErrorMessage( validationContext.DisplayName ) );
					return new ValidationResult( FormatErrorMessage( validationContext.DisplayName ), new[] { validationContext.MemberName } );
				}
			}

			return ValidationResult.Success;
		}
	}
}