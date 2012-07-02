using System;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;

namespace Aicl.Galapago.Model.Types
{
	public class AsientoItemValidator : AbstractValidator<AsientoItem>
	{
		public AsientoItemValidator()
		{
			RuleSet(Operaciones.Create, () => {
				RuleFor(x => x.Id).Equal(0).WithMessage("Se Debe Indicar el Id").WithErrorCode("ConId");
				RuleFor(x => x.IdCuenta).NotEqual(0).WithMessage("Debe Indicar el IdCuenta").WithErrorCode("SinIdCuenta");
				RuleFor(x => x.IdCentro).NotEqual(0).WithMessage("Debe Indicar el IdCentro").WithErrorCode("SinIdCentro");
				RuleFor(x => x.IdAsiento).NotEqual(0).WithMessage("Debe Indicar el IdAsiento").WithErrorCode("SinIdAsiento");
				RuleFor(x => x.TipoPartida).Must(r=>r==1 || r==2).WithMessage("Tipo de Partida debe ser 1=Debito 2=Credito").WithErrorCode("TipoPartidaErroneo");
				RuleFor(x => x.Valor).GreaterThan(0).WithMessage("Valor debe ser mayor a cero").WithErrorCode("ValorCero");
			});
			
			RuleSet(Definiciones.UsaTercero, () => {
				RuleFor(x => x.IdTercero).Must(r=> r.HasValue && r.Value!=default(int)).WithMessage("Debe Indicar el IdTercero").WithErrorCode("SinIdTercero");
			});
			
			RuleSet(Operaciones.Update, () => {
				RuleFor(x => x.TipoPartida).Must(r=>r==1 || r==2).WithMessage("Tipo de Partida debe ser 1=Debito 2=Credito").WithErrorCode("TipoPartidaErroneo");
				RuleFor(x => x.Valor).GreaterThan(0).WithMessage("Valor debe ser mayor a cero").WithErrorCode("ValorCero");
			});
			
			
		}
	}
}

