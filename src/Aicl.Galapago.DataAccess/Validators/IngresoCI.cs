using System;
using ServiceStack.FluentValidation;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
    public class IngresoCI
    {
        public IngresoCI ()
        {
            OldCei= new ComprobanteIngresoItem();
        }

        public Ingreso Ingreso{ get; set;}
        public ComprobanteIngreso Ce { get; set;}
        public ComprobanteIngresoItem Cei { get; set;}
        public ComprobanteIngresoItem OldCei { get; set;}

    }

    public class IngresoCIValidator:AbstractValidator<IngresoCI>
    {
        public IngresoCIValidator()
        {
            RuleSet(Operaciones.InsertarIngresoEnCI, () =>{

                RuleFor(x=>x.Cei).Must(x=>x.Abono!=0).
                    WithMessage("Valor a Pagar debe ser diferente de cero").WithErrorCode("ValorPagarCero");

                RuleFor(x=>x.Ingreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Ingreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Cei).Must((e,x)=> Math.Abs(x.Abono)<= Math.Abs(e.Ingreso.Saldo)).
                    WithMessage("Valor a Pagar > Saldo del egreso").WithErrorCode("ValorMayorSaldo");

                RuleFor(x=>x.Ingreso).Must((e,x)=>x.IdTercero==e.Ce.IdTercero).
                    WithMessage("Tercero del Ingreso!= Tercero del Comprobante").WithErrorCode("ErrorTercero");

                RuleFor(x=>x.Ingreso).Must((e,x)=>x.IdSucursal==e.Ce.IdSucursal).
                    WithMessage("Sucursal del Ingreso!= Sucursal del Comprobante").WithErrorCode("ErrorSucursal");

                RuleFor(x=>x.Ingreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Ingreso NO esta Asentado").WithErrorCode("IngresoNoAsentado");

                RuleFor(x=>x.Ingreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Ingreso esta Anulado").WithErrorCode("IngresoAnulado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAsentado.HasValue).
                    WithMessage("ComprobanteIngreso Ya esta Asentado.No se pueden agregar mas egresos").
                        WithErrorCode("ComprobanteIngresoAsentado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAnulado.HasValue).
                    WithMessage("ComprobanteIngreso  esta Anulado.No se pueden agregar mas egresos").
                        WithErrorCode("ComprobanteIngresoAnulado");

                RuleFor(x=>x.Cei).Must((e,x)=> x.Abono>0).
                    When(e=>e.Ingreso.CodigoDocumento==Definiciones.ProveedorFV ||
                         e.Ingreso.CodigoDocumento==Definiciones.ProveedorCC).
                        WithMessage("El valor para Factura o Cuenta de Cobro debe ser Mayor a Cero").
                        WithErrorCode("ValorNegativo");



            });

            RuleSet(Operaciones.ActualizarIngresoEnCI, () =>{



                RuleFor(x=> x.Cei).Must((e,x)=> x.IdComprobanteIngreso==e.OldCei.IdComprobanteIngreso).
                    WithMessage("No se puede cambiar el ComprobanteIngreso").
                        WithErrorCode("IdComprobanteIngresoModificado");

                RuleFor(x=> x.Cei).Must((e,x)=> x.IdIngreso== e.OldCei.IdIngreso).
                    WithMessage("No se puede modificar el Ingreso").
                        WithErrorCode("IdIngresoModificado");

                RuleFor(x=>x.Cei).Must(x=>x.Abono!=0).
                    WithMessage("Valor a Pagar debe ser diferente de cero").WithErrorCode("ValorPagarCero");

                RuleFor(x=>x.Ingreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Ingreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Ingreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Ingreso NO esta Asentado").WithErrorCode("IngresoNoAsentado");

                RuleFor(x=>x.Ingreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Ingreso esta Anulado").WithErrorCode("IngresoAnulado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAsentado.HasValue).
                    WithMessage("ComprobanteIngreso Ya esta Asentado.No se puede actualizar el egreso").
                        WithErrorCode("ComprobanteIngresoAsentado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAnulado.HasValue).
                    WithMessage("ComprobanteIngreso  esta Anulado.No se pueden actualizar el egreso").
                        WithErrorCode("ComprobanteIngresoAnulado");

                RuleFor(x=>x.Cei).Must((e,x)=> x.Abono>0).
                    When(e=>e.Ingreso.CodigoDocumento==Definiciones.ProveedorFV ||
                         e.Ingreso.CodigoDocumento==Definiciones.ProveedorCC).
                        WithMessage("El valor para Factura o Cuenta de Cobro debe ser Mayor a Cero").
                        WithErrorCode("ValorNegativo");
            });


            RuleSet(Operaciones.BorrarIngresoEnCI, () =>{

                RuleFor(x=>x.Ingreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Ingreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Ingreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Ingreso NO esta Asentado").WithErrorCode("IngresoNoAsentado");

                RuleFor(x=>x.Ingreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Ingreso esta Anulado").WithErrorCode("IngresoAnulado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAsentado.HasValue).
                    WithMessage("ComprobanteIngreso Ya esta Asentado.No se puede borrar el egreso").
                        WithErrorCode("ComprobanteIngresoAsentado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAnulado.HasValue).
                    WithMessage("ComprobanteIngreso  esta Anulado.No se pueden borrar el egreso").
                        WithErrorCode("ComprobanteIngresoAnulado");
            });

            RuleSet(Operaciones.ActualizarValorIngresoAlAsentarCI, () =>{

                RuleFor(x=>x.Ingreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Ingreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Ingreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Ingreso NO esta Asentado").WithErrorCode("IngresoNoAsentado");

                RuleFor(x=>x.Ingreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Ingreso esta Anulado").WithErrorCode("IngresoAnulado");

                RuleFor(x=>x.Cei).Must((e,x)=> Math.Abs(x.Abono)<=Math.Abs(e.Ingreso.Saldo)).
                    WithMessage("El Valor  es mayor al Saldo Disponible").WithErrorCode("ValorMayorSaldo");
            });
        }
    }
}

