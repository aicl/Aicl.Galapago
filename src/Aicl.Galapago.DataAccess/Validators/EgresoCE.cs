using System;
using ServiceStack.Common;
using ServiceStack.FluentValidation;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
    public class EgresoCE
    {
        public EgresoCE ()
        {
            OldCei= new ComprobanteEgresoItem();
        }

        public Egreso Egreso{ get; set;}
        public ComprobanteEgreso Ce { get; set;}
        public ComprobanteEgresoItem Cei { get; set;}
        public ComprobanteEgresoItem OldCei { get; set;}

    }

    public class EgresoCEValidator:AbstractValidator<EgresoCE>
    {
        public EgresoCEValidator()
        {
            RuleSet(Operaciones.InsertarEgresoEnCE, () =>{

                RuleFor(x=>x.Cei).Must(x=>x.Abono!=0).
                    WithMessage("Valor a Pagar debe ser diferente de cero").WithErrorCode("ValorPagarCero");

                RuleFor(x=>x.Egreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Egreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Cei).Must((e,x)=> Math.Abs(x.Abono)<= Math.Abs(e.Egreso.Saldo)).
                    WithMessage("Valor a Pagar > Saldo del egreso").WithErrorCode("ValorMayorSaldo");

                RuleFor(x=>x.Egreso).Must((e,x)=>x.IdTercero==e.Ce.IdTercero).
                    WithMessage("Tercero del Egreso!= Tercero del Comprobante").WithErrorCode("ErrorTercero");

                RuleFor(x=>x.Egreso).Must((e,x)=>x.IdSucursal==e.Ce.IdSucursal).
                    WithMessage("Sucursal del Egreso!= Sucursal del Comprobante").WithErrorCode("ErrorSucursal");

                RuleFor(x=>x.Egreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Egreso NO esta Asentado").WithErrorCode("EgresoNoAsentado");

                RuleFor(x=>x.Egreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Egreso esta Anulado").WithErrorCode("EgresoAnulado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAsentado.HasValue).
                    WithMessage("ComprobanteEgreso Ya esta Asentado.No se pueden agregar mas egresos").
                        WithErrorCode("ComprobanteEgresoAsentado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAnulado.HasValue).
                    WithMessage("ComprobanteEgreso  esta Anulado.No se pueden agregar mas egresos").
                        WithErrorCode("ComprobanteEgresoAnulado");

                RuleFor(x=>x.Cei).Must((e,x)=> x.Abono>0).
                    When(e=>e.Egreso.CodigoDocumento==Definiciones.ProveedorFV ||
                         e.Egreso.CodigoDocumento==Definiciones.ProveedorCC).
                        WithMessage("El valor para Factura o Cuenta de Cobro debe ser Mayor a Cero").
                        WithErrorCode("ValorNegativo");



            });

            RuleSet(Operaciones.ActualizarEgresoEnCE, () =>{



                RuleFor(x=> x.Cei).Must((e,x)=> x.IdComprobanteEgreso==e.OldCei.IdComprobanteEgreso).
                    WithMessage("No se puede cambiar el ComprobanteEgreso").
                        WithErrorCode("IdComprobanteEgresoModificado");

                RuleFor(x=> x.Cei).Must((e,x)=> x.IdEgreso== e.OldCei.IdEgreso).
                    WithMessage("No se puede modificar el Egreso").
                        WithErrorCode("IdEgresoModificado");

                RuleFor(x=>x.Cei).Must(x=>x.Abono!=0).
                    WithMessage("Valor a Pagar debe ser diferente de cero").WithErrorCode("ValorPagarCero");

                RuleFor(x=>x.Egreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Egreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Egreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Egreso NO esta Asentado").WithErrorCode("EgresoNoAsentado");

                RuleFor(x=>x.Egreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Egreso esta Anulado").WithErrorCode("EgresoAnulado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAsentado.HasValue).
                    WithMessage("ComprobanteEgreso Ya esta Asentado.No se puede actualizar el egreso").
                        WithErrorCode("ComprobanteEgresoAsentado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAnulado.HasValue).
                    WithMessage("ComprobanteEgreso  esta Anulado.No se pueden actualizar el egreso").
                        WithErrorCode("ComprobanteEgresoAnulado");

                RuleFor(x=>x.Cei).Must((e,x)=> x.Abono>0).
                    When(e=>e.Egreso.CodigoDocumento==Definiciones.ProveedorFV ||
                         e.Egreso.CodigoDocumento==Definiciones.ProveedorCC).
                        WithMessage("El valor para Factura o Cuenta de Cobro debe ser Mayor a Cero").
                        WithErrorCode("ValorNegativo");
            });


            RuleSet(Operaciones.BorraregresoEnCE, () =>{

                RuleFor(x=>x.Egreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Egreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Egreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Egreso NO esta Asentado").WithErrorCode("EgresoNoAsentado");

                RuleFor(x=>x.Egreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Egreso esta Anulado").WithErrorCode("EgresoAnulado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAsentado.HasValue).
                    WithMessage("ComprobanteEgreso Ya esta Asentado.No se puede borrar el egreso").
                        WithErrorCode("ComprobanteEgresoAsentado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAnulado.HasValue).
                    WithMessage("ComprobanteEgreso  esta Anulado.No se pueden borrar el egreso").
                        WithErrorCode("ComprobanteEgresoAnulado");

            });


            RuleSet(Operaciones.ActualizarValorEgresoAlAsentarCE, () =>{

                RuleFor(x=>x.Egreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Egreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Egreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Egreso NO esta Asentado").WithErrorCode("EgresoNoAsentado");

                RuleFor(x=>x.Egreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Egreso esta Anulado").WithErrorCode("EgresoAnulado");

                RuleFor(x=>x.Cei).Must((e,x)=> Math.Abs(x.Abono)<=Math.Abs(e.Egreso.Saldo)).
                    WithMessage("El Valor  es mayor al Saldo Disponible").WithErrorCode("ValorMayorSaldo");

            });



        }

    }

}

