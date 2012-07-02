using System;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;

namespace Aicl.Galapago.Model.Types
{
	public class TerceroValidator:AbstractValidator<Tercero>
	{
		public TerceroValidator ()
		{
			RuleSet(Definiciones.RegistroActivo, () => {
				RuleFor(x => x.Activo).Must(r=> true).WithMessage("El tercero esta desactivado").WithErrorCode("NoActivo");
			});
			
		}
	}
}

