using System;
using System.Globalization;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	public class IdValidator<T>:AbstractValidator<T> 
		where T:IHasId<System.Int32>, new()
	{
		public IdValidator ()
		{
			
			RuleSet(Operaciones.Create, () => {

				RuleFor(x => x.Id).Equal(0).WithMessage("Se Debe Omitir el Id del Documento").WithErrorCode("ConId");	
				
			});
			
			RuleSet(Operaciones.Destroy, () => {

				RuleFor(x => x.Id).NotEqual(0).WithMessage("Debe Indicar el Id del Documento que desea borrar").WithErrorCode("SinId");	
				
			});
			
			RuleSet(Operaciones.Update, () => {

				RuleFor(x => x.Id).NotEqual(0).WithMessage("Debe Indicar el Id del Documento que desea actualizar").WithErrorCode("SinId");
								
			});
			
			
			RuleSet(Operaciones.Asentar, () => {

				RuleFor(x => x.Id).NotEqual(0).WithMessage("Debe Indicar el Id del Documento que desea asentar").WithErrorCode("SinId");
								
			});
			
			RuleSet(Operaciones.Reversar, () => {

				RuleFor(x => x.Id).NotEqual(0).WithMessage("Debe Indicar el Id del Documento que desea reversar").WithErrorCode("SinId");
								
			});
		}
	}
}

