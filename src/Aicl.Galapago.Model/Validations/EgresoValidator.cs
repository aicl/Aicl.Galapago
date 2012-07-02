using System;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;

namespace Aicl.Galapago.Model.Types
{
	public class EgresoValidator:AbstractValidator<Egreso>
	{
		// contemplar Operaciones.Exportar,
		//Egreso.ActualizarSaldo ( despues de asentado se puede agregar items en el comprobante de pago...)
        // o Operaciones.InsertarItem.....
		public EgresoValidator ()
		{
            List<string> codigos= new List<string>(){"CLCC","CLFV", "PRFV"};

			RuleSet(Operaciones.Create, () => {
				RuleFor(x => x.Id).Equal(0).WithMessage("Se debe omitir el Id").WithErrorCode("ConId");
                RuleFor(x => x.Numero).Equal(0).WithMessage("Se debe omitir el Numero").WithErrorCode("ConNumero");
				RuleFor(x => x.IdSucursal).NotEqual(0).WithMessage("Debe Indicar el IdSucursal").WithErrorCode("SinSucursal");
				RuleFor(x => x.IdTercero).NotEqual(0).WithMessage("Debe Indicar el IdTercero").WithErrorCode("SinTercero");
				
				RuleFor(x => x.Valor).Equal(0).WithMessage("Valor debe ser 0").WithErrorCode("ConValor");
				RuleFor(x => x.Saldo).Equal(0).WithMessage("Saldo debe ser 0").WithErrorCode("ConSaldo");
				
				RuleFor(x => x.DiasCredito).Must(r=> r>=0).WithMessage("Dias Credito debe ser >=0").WithErrorCode("DiasCreditoNegativo");
				
				RuleFor(x => x.Fecha).NotEqual(default(DateTime)).WithMessage("Debe Indicar la fecha del asiento").WithErrorCode("SinFecha");
				RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Se debe omitir la Fecha de Asentado");
				RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Se debe omitir la Fecha de Anulado");
				RuleFor(x => x.Periodo).Must(r=> string.IsNullOrEmpty(r) ).WithMessage("Se debe omitir el Periodo");
				RuleFor(x => x.Descripcion).Must(r=> !string.IsNullOrEmpty(r) ).WithMessage("Debe Indicar la Descripcion").WithErrorCode("SinDescripcion");
				RuleFor(x => x.CodigoDocumento).NotEmpty().WithMessage("Debe Indicar el codigo del documento").WithErrorCode("SinCodigoDocumento");
				RuleFor(x => x.Documento).NotEmpty().When( x=> codigos.Contains(x.CodigoDocumento)).WithMessage("Debe Indicar el del documento").WithErrorCode("SinDocumento");
				
				RuleFor(x => x.Externo).Equal(false).WithMessage("Externo debe ser falso").WithErrorCode("NoFalso");
				
			});
			
			RuleSet(Operaciones.Update, () => {
				RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Documento Asentado. No se puede actualizar").WithErrorCode("Asentado");
				RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento Anulado. No se puede actualizar").WithErrorCode("Anulado");				
			});
			
			RuleSet(Operaciones.Destroy, () => {

				RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Documento Asentado. No se puede borrar").WithErrorCode("Asentado");
				RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento Anulado. No se puede borrar").WithErrorCode("Anulado");
				
			});

            RuleSet(Operaciones.Asentar, () => {
                RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento Anulado. No se puede asentar").WithErrorCode("Anulado");               
                RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Documento ya esta Asentado").WithErrorCode("YaAsentado");
            });

            RuleSet(Operaciones.Reversar, () => {
                RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento Anulado. No se puede reversar").WithErrorCode("Anulado");
                RuleFor(x => x.FechaAsentado).Must(r=> r.HasValue).WithMessage("Documento NO esta Asentado. No se puede reversar").WithErrorCode("NoAsentado");
            });

            RuleSet(Operaciones.Anular, () => {
                RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento ya esta anulado").WithErrorCode("YaAnulado");               
                RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Documento asentado.No se puede Anular").WithErrorCode("Asentado");               
            });


            RuleSet(Definiciones.CheckRequestBeforeAsentar, () => {
                RuleFor(x => x.Id).NotEqual(0).WithMessage("Debe Indicar el Id del Documento a asentar").WithErrorCode("SinId");
                RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento enviado como Anulado. No se puede asentar").WithErrorCode("Anulado");               
            });

            RuleSet(Definiciones.CheckRequestBeforeReversar, () => {
                RuleFor(x => x.Id).NotEqual(0).WithMessage("Debe Indicar el Id del Documento a reversar").WithErrorCode("SinId");
                RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento enviado como Anulado. No se puede reversar").WithErrorCode("Anulado");               
            });

            RuleSet(Definiciones.CheckRequestBeforeAnular, () => {
                RuleFor(x => x.Id).NotEqual(0).WithMessage("Debe Indicar el Id del Documento a anular").WithErrorCode("SinId");
                RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Documento enviado como asentado. No se puede anular").WithErrorCode("Asentado");               
            });


            RuleSet(Definiciones.CheckRequestBeforeUpdate, () => {
                RuleFor(x => x.Id).NotEqual(0).WithMessage("Debe Indicar el Id del Documento a actualizar").WithErrorCode("SinId");
                RuleFor(x => x.FechaAsentado).Must(r=> !r.HasValue).WithMessage("Documento enviado como Asentado. No se puede actualizar").WithErrorCode("Asentado");
                RuleFor(x => x.FechaAnulado).Must(r=> !r.HasValue).WithMessage("Documento enviado como Anulado. No se puede actualizar").WithErrorCode("Anulado");
                
            });

		}
	}
}

