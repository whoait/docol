//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Gulliver.DoCol.DataValidation
{
	public class ConditionalAttributeBase : ValidationAttribute
	{
		private const string DefaultErrorMessage = "The {0} field was invalid";

		public ConditionalAttributeBase()
			: this( DefaultErrorMessage )
		{
		}

		public ConditionalAttributeBase( string errorMessage )
			: base( errorMessage )
		{
		}

		protected bool ShouldRunValidation(
			object value,
			string dependentProperty,
			object targetValue,
			ValidationContext validationContext )
		{
			string[] dependentControls = dependentProperty.Split( '_' );
			bool shouldRunValid = false;

			foreach (var dependentControl in dependentControls)
			{
				var dependentValue = GetDependentFieldValue( dependentControl, validationContext );
				if ((dependentValue == null && targetValue == null) ||
					(dependentValue != null && dependentValue.Equals( targetValue )))
				{
					shouldRunValid = true;
					break;
				}
			}

			// compare the value against the target value
			return shouldRunValid;
		}

		protected bool ShouldRunValidationNotEmpty(
			object value,
			string dependentProperty,
			object targetValue,
			ValidationContext validationContext )
		{
			string[] dependentControls = dependentProperty.Split( '_' );
			bool isValid = true;

			foreach (var dependentControl in dependentControls)
			{
				var dependentValue = GetDependentFieldValue( dependentControl, validationContext );
				if ((dependentValue == null && targetValue == null) ||
					(dependentValue != null && !dependentValue.Equals( targetValue )))
				{
					isValid = false;
					break;
				}
			}

			// compare the value against the target value
			return isValid;
		}

		// TODO: Some of these could be removed from the base class. They also need dedicated unit tests.
		protected object GetDependentFieldValue( string dependentProperty, ValidationContext validationContext )
		{
			// get a reference to the property this validation depends upon
			var containerType = validationContext.ObjectInstance.GetType();
			var field = containerType.GetProperty( dependentProperty );

			if (field == null)
				throw new MissingMemberException( containerType.Name, dependentProperty );

			// get the value of the dependent property
			var dependentvalue = field.GetValue( validationContext.ObjectInstance, null );
			return dependentvalue;
		}

		protected string QualifyFieldId( ModelMetadata metadata, string fieldId, ViewContext viewContext )
		{
			// build the ID of the property
			string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId( fieldId );
			// unfortunately this will have the name of the current field appended to the beginning,
			// because the TemplateInfo's context has had this fieldname appended to it. Instead, we
			// want to get the context as though it was one level higher (i.e. outside the current property,
			// which is the containing object (our Person), and hence the same level as the dependent property.
			var thisField = metadata.PropertyName + "_";
			if (depProp.StartsWith( thisField ))
				// strip it off again
				depProp = depProp.Substring( thisField.Length );
			return depProp;
		}
	}
}