using System;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;
namespace Aicl.Galapago.Model.Types
{
 
    public class EgresoItemValidator : AbstractValidator<EgresoItem>
    {
        public EgresoItemValidator()
        {
            RuleSet(Operaciones.Create, () => {
                RuleFor(x=>x.Id).Equal(0).WithMessage("Se debe omitir el Id del item").WithErrorCode("ConIdItem");
                RuleFor(x => x.IdPresupuestoItem).NotEqual(0).
                    WithMessage("Debe Indicar el IdPresupuestoItem").WithErrorCode("NoIdPresupuestoItem");
                RuleFor(x => x.IdCentro).NotEqual(0).WithMessage("Debe Indicar el IdCentro").
                    WithErrorCode("SinIdCentro");;
                RuleFor(x => x.IdEgreso).NotEqual(0).WithMessage("Debe Indicar el IdEgreso").
                   WithErrorCode("SinIdEgreso");
                RuleFor(x => x.Valor).NotEqual(0).WithMessage("Debe Indicar el valor del Item").
                    WithErrorCode("SinValor");;
                RuleFor(x => x.TipoPartida).Must(r=>r==1 || r==2).
                    WithMessage("Tipo de Partida debe ser 1=Debito 2=Credito").
                        WithErrorCode("ErrorTipoPartida");           
            });
        }
    }

    public class EgresoItemAlCrear
    {
        public EgresoItem NewItem {get;set;}
        public Centro CentroItem { get; set;}
        public Tercero TerceroItem { get; set;}
        public Presupuesto Prs { get; set;}
        public PresupuestoItem Pi { get; set;}
        public Egreso Parent { get; set;}
        public static readonly string Regla1= "Regla1";
    }


    public class EgresoItemAlCrearValidador: AbstractValidator<EgresoItemAlCrear>
    {
        public EgresoItemAlCrearValidador()
        {
            RuleSet(EgresoItemAlCrear.Regla1, ()=>{
                RuleFor(x=>x.Prs.Activo).Must(r=>true).
                    WithMessage("Presupuesto No Activo").
                    WithErrorCode("PrespuestoNoActivo");

                RuleFor(x=>x.CentroItem.Activo).Must(r=>true).
                    WithMessage("Centro No Activo").
                    WithErrorCode("CentroNoActivo");

                RuleFor(x=>x.TerceroItem.Activo).Must(r=>true).
                    When(x=>x.Pi.UsaTercero && x.TerceroItem!=default(Tercero)).
                    WithMessage("Tercero No Activo").
                    WithErrorCode("TerceroNoActivo");

                RuleFor(x=>x.NewItem.IdCentro).Must((e,idCentro)=>e.Prs.IdCentro==idCentro).
                    WithMessage(string.Format("Centro de Costo no Coincide (Egresoitem-presupuesto)")).
                    WithErrorCode("CentroErroneo");

                RuleFor(x=>x.NewItem.IdTercero).Must(idTercero=>idTercero.HasValue && idTercero.Value!=default(int)).
                    When(x=>x.Pi.UsaTercero).
                    WithMessage("Se debe Indicar el Tercero para este item)").
                    WithErrorCode("FaltaTercero");

                //RuleFor(x => x.Pi).SetValidator(new PresupuestoItemDetalleActivoValidador()); does not work!

                RuleFor(x => x.Pi.Codigo).Must(r=> r.IndexOf(".")==Definiciones.PrspPosicionPunto).WithMessage("Item de Presupuesto no es de Detalle").WithErrorCode("NoDetalle");           
                RuleFor(x => x.Pi.Activo).Must(r=> true).WithMessage("Item de Presupuesto  esta desactivado").WithErrorCode("NoActivo");
                //TODO: poner la regla aqui.... para que sean de detalle y activo...

            });

        }
    }
}

