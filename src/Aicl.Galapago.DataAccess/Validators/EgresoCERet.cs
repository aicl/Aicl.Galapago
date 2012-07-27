using ServiceStack.FluentValidation;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
    public class EgresoCERet
    {
        public EgresoCERet (){}

        public Egreso Egreso{ get; set;}
        public ComprobanteEgreso Ce { get; set;}
        public ComprobanteEgresoItem Cei { get; set;}
        public ComprobanteEgresoRetencion Ret { get; set;}
        public ComprobanteEgresoRetencion OldRet { get; set;}
        public PresupuestoItem Pi {get; set;}

    }

    public class EgresoCERetValidator:AbstractValidator<EgresoCERet>
    {
        public EgresoCERetValidator()
        {
            RuleSet(Operaciones.InsertarRetencionEnCE, () =>{
                RuleFor(x=>x.Cei).Must((e,x)=> x.IdComprobanteEgreso==e.Egreso.Id).
                    WithMessage("El Egreso no pertenece al comprobante").
                        WithErrorCode("EgresoNoEnComprobante");

                RuleFor(x=>x.Ret).Must(x=>x.Valor!=0).
                    WithMessage("Valor de la Retencion debe ser diferente de cero").WithErrorCode("ValorRetencionCero");

                RuleFor(x=>x.Egreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Egreso sin Saldo").WithErrorCode("SinSaldo");

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

            RuleSet(Operaciones.BorrarRetencionEnCE, () =>{
                RuleFor(x=>x.Cei).Must((e,x)=> x.IdComprobanteEgreso==e.Egreso.Id).
                    WithMessage("El Egreso no pertenece al comprobante").
                        WithErrorCode("EgresoNoEnComprobante");

                RuleFor(x=>x.Egreso).Must((e,x)=>x.Saldo!=0).
                    WithMessage("Egreso sin Saldo").WithErrorCode("SinSaldo");

                RuleFor(x=>x.Egreso).Must( x=> x.FechaAsentado.HasValue ).
                    WithMessage("Egreso NO esta Asentado").WithErrorCode("EgresoNoAsentado");

                RuleFor(x=>x.Egreso).Must( x=> !x.FechaAnulado.HasValue ).
                    WithMessage("Egreso esta Anulado").WithErrorCode("EgresoAnulado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAsentado.HasValue).
                    WithMessage("ComprobanteEgreso Ya esta Asentado.No se pueden borrar la retencion").
                        WithErrorCode("ComprobanteEgresoAsentado");

                RuleFor(x=>x.Ce).Must(x=>!x.FechaAnulado.HasValue).
                    WithMessage("ComprobanteEgreso  esta Anulado.No se pueden borrar la retencion").
                        WithErrorCode("ComprobanteEgresoAnulado");

            });

        }
    
    }
}

