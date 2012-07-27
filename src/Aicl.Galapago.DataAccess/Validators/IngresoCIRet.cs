using ServiceStack.FluentValidation;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
    public class IngresoCIRet
    {
        public IngresoCIRet (){}

        public Ingreso Ingreso{ get; set;}
        public ComprobanteIngreso Ce { get; set;}
        public ComprobanteIngresoItem Cei { get; set;}
        public ComprobanteIngresoRetencion Ret { get; set;}
        public ComprobanteIngresoRetencion OldRet { get; set;}
        public PresupuestoItem Pi {get; set;}

    }

    public class IngresoCIRetValidator:AbstractValidator<IngresoCIRet>
    {
        public IngresoCIRetValidator()
        {
            RuleSet(Operaciones.InsertarRetencionEnCI, () =>{
                RuleFor(x=>x.Cei).Must((e,x)=> x.IdComprobanteIngreso==e.Ingreso.Id).
                    WithMessage("El Ingreso no pertenece al comprobante").
                        WithErrorCode("IngresoNoEnComprobante");

                RuleFor(x=>x.Ret).Must(x=>x.Valor!=0).
                    WithMessage("Valor de la Retencion debe ser diferente de cero").WithErrorCode("ValorRetencionCero");

                RuleFor(x=>x.Ingreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Ingreso sin Saldo").WithErrorCode("SinSaldo");

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

                RuleFor(x => x.Pi.Codigo).Must(r=> r.StartsWith(Definiciones.GrupoRetenciones) ).
                    WithMessage("Item No es del Grupo Retenciones").
                        WithErrorCode("NoRetencion");           

                RuleFor(x => x.Pi.Codigo).Must(r=> r.IndexOf(".")==Definiciones.PrspPosicionPunto).
                    WithMessage("Item de Presupuesto no es de Detalle").
                        WithErrorCode("NoDetalle");           

                RuleFor(x => x.Pi.Activo).Must(r=> true).
                    WithMessage("Item de Presupuesto  esta desactivado").
                        WithErrorCode("NoActivo");

            });

            RuleSet(Operaciones.BorrarRetencionEnCI, () =>{
                RuleFor(x=>x.Cei).Must((e,x)=> x.IdComprobanteIngreso==e.Ingreso.Id).
                    WithMessage("El Ingreso no pertenece al comprobante").
                        WithErrorCode("IngresoNoEnComprobante");

                RuleFor(x=>x.Ingreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Ingreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Ingreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Ingreso NO esta Asentado").WithErrorCode("IngresoNoAsentado");

                RuleFor(x=>x.Ingreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Ingreso esta Anulado").WithErrorCode("IngresoAnulado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAsentado.HasValue).
                    WithMessage("ComprobanteIngreso Ya esta Asentado.No se pueden borrar la retencion").
                        WithErrorCode("ComprobanteIngresoAsentado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAnulado.HasValue).
                    WithMessage("ComprobanteIngreso  esta Anulado.No se pueden borrar la retencion").
                        WithErrorCode("ComprobanteIngresoAnulado");

            });

        }
    
    }
}

