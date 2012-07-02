using System;
using System.Globalization;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;

namespace Aicl.Galapago.Model.Types
{
	public class AsientoValidator:AbstractValidator<Asiento>
	{
		public AsientoValidator ()
		{
			
			RuleSet(Operaciones.Create, () => {
				RuleFor(x => x.Id).Equal(0).WithMessage("Se debe omitir el Id").WithErrorCode("ConId");;
				RuleFor(x => x.IdSucursal).NotEqual(0).WithMessage("Debe Indicar el IdSucursal").WithErrorCode("SinSucursal");
				RuleFor(x => x.Fecha).NotEqual(default(DateTime)).WithMessage("Debe Indicar la fecha del asiento").WithErrorCode("SinFecha");
				RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Se debe omitir la Fecha de Asentado");
				RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Se debe omitir la Fecha de Anulado");
				RuleFor(x => x.Debitos).Equal(0).WithMessage("Se debe omitir el valor de los Debitos");
				RuleFor(x => x.Creditos).Equal(0).WithMessage("Se debe omitir el valor de los Creditos");
				RuleFor(x => x.Periodo).Must(r=> string.IsNullOrEmpty(r) ).WithMessage("Se debe omitir el Periodo");
				RuleFor(x => x.Descripcion).Must(r=> !string.IsNullOrEmpty(r) ).WithMessage("Debe Indicar la Descripcion").WithErrorCode("SinDescripcion");
				RuleFor(x => x.CodigoDocumento).NotEmpty().WithMessage("Debe Indicar el codigo del documento").WithErrorCode("SinCodigoDocumento");
				RuleFor(x => x.Documento).NotEmpty().Unless(x=>x.CodigoDocumento==Definiciones.ComprobranteContable).WithMessage("Debe Indicar el del documento").WithErrorCode("SinDocumento");
				
			});
			
			RuleSet(Operaciones.Update, () => {

				RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Documento Asentado. No se puede actualizar").WithErrorCode("Asentado");
				RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento Anulado. No se puede actualizar").WithErrorCode("Anulado");
				
			});
			
			RuleSet(Operaciones.Destroy, () => {

				RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Documento Asentado. No se puede borrar").WithErrorCode("Asentado");
				RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento Anulado. No se puede borrar").WithErrorCode("Anulado");
				
			});
			
		}
	}
}

