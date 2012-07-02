using System;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;

namespace Aicl.Galapago.Model.Types
{
	public class CuentaValidator : AbstractValidator<Cuenta>
	{
		public CuentaValidator ()
		{
			
			RuleSet(Definiciones.CuentaDetalleActiva, () => {
				RuleFor(x => x.Codigo).Must(r=> r.IndexOf(".")==Definiciones.CntbPosicionPunto).WithMessage("La cuenta no es de Detalle").WithErrorCode("NoDetalle");			
				RuleFor(x => x.Activa).Must(r=> true).WithMessage("La cuenta esta desactivada").WithErrorCode("NoActiva");
			});
			
			
		}
	}
}

// El punto debe estar en una posicion determinada en Definiciones.CntbPosicionPunto: 1 1 11 05 . 
// Desde Cero un numero par..
// la primera cuenta es de long = 1,
// la segunda cuenta es de long = 2,
// despues cada cuenta aumenta de a 2 en dos 
// la cuentas detalle tienen punto en posicion == Definiciones.CntbPosicionPunto

// Cuando contiene punto : index == PuntoPosicion........ y 
// cuando no contiene punto: long < PuntoPosicion-1 y ( igual a 1, 2, o cualquier numero par 


/*
RuleSet(Definiciones.CuentaDetalle, () => {
				RuleFor(x => x.Codigo).Must(r=> r.Contains(".")).WithMessage("La cuenta no es de Detalle").WithErrorCode("NoDetalle");			
			});
			
			RuleSet(Definiciones.Activo, () => {
				RuleFor(x => x.Activa).Must(r=> true).WithMessage("La cuenta esta desactivada").WithErrorCode("NoActiva");
			});
			
			*/