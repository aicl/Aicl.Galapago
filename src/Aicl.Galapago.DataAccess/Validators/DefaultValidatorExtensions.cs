using System;
using System.Text;
using ServiceStack.Text;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Results;
using ServiceStack.FluentValidation.Internal;

namespace Aicl.Galapago.DataAccess
{
	public static class DefaultValidatorExtensions
	{
		public static void ValidateAndThrowHttpError<T>(this IValidator<T> validator, T instance) {
			var result = validator.Validate(instance);
			
			
			if(! result.IsValid)
			{	
				throw new HttpError( result.BuildErrorMessage());
			}
			
		}
		
		public static void ValidateAndThrowHttpError<T>(this IValidator<T> validator, T instance, string ruleSet)
		{
			var result = validator.Validate(instance, (IValidatorSelector)null, ruleSet);

			if (!result.IsValid)
			{
				throw new HttpError( result.BuildErrorMessage());
			}
		}
		
		
		public static string BuildErrorMessage(this ValidationResult validationResult)
		{
			return validationResult.Errors.SerializeToString();	
		}
	}
}

