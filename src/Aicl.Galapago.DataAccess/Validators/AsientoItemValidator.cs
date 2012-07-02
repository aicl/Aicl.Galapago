/*
using System;
using System.Globalization;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;

using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{

	
	public class AsientoItemValidator : AbstractValidator<AsientoItem>
	{
		public AsientoItemValidator()
		{
			RuleSet("create", () => {
				RuleFor(x => x.IdCuenta).NotEqual(0).WithMessage("Debe Indicar el IdCuenta");
				RuleFor(x => x.IdCentro).NotEqual(0).WithMessage("Debe Indicar el IdCentro");
				RuleFor(x => x.IdAsiento).NotEqual(0).WithMessage("Debe Indicar el IdAsiento");
				RuleFor(x => x.TipoPartida).Must(r=>r==1 || r==2).WithMessage("Tipo de Partida debe ser 1=Debito 2=Credito");			
			});
		}
	}
}

*/