using ServiceStack.FluentValidation;
namespace Aicl.Galapago.Model.Types
{
 
    public class IngresoItemValidator : AbstractValidator<IngresoItem>
    {
        public IngresoItemValidator()
        {
            RuleSet(Operaciones.Create, () => {
                RuleFor(x=>x.Id).Equal(0).WithMessage("Se debe omitir el Id del item").WithErrorCode("ConIdItem");
                RuleFor(x => x.IdPresupuestoItem).NotEqual(0).
                    WithMessage("Debe Indicar el IdPresupuestoItem").WithErrorCode("NoIdPresupuestoItem");
                RuleFor(x => x.IdCentro).NotEqual(0).WithMessage("Debe Indicar el IdCentro").
                    WithErrorCode("SinIdCentro");;
                RuleFor(x => x.IdIngreso).NotEqual(0).WithMessage("Debe Indicar el IdIngreso").
                   WithErrorCode("SinIdIngreso");
                RuleFor(x => x.Valor).NotEqual(0).WithMessage("Debe Indicar el valor del Item").
                    WithErrorCode("SinValor");;
                RuleFor(x => x.TipoPartida).Must(r=>r==1 || r==2).
                    WithMessage("Tipo de Partida debe ser 1=Debito 2=Credito").
                        WithErrorCode("ErrorTipoPartida");           
            });
        }
    }

    public class IngresoItemAlCrear
    {
        public IngresoItem NewItem {get;set;}
        public Centro CentroItem { get; set;}
        public Presupuesto Prs { get; set;}
        public PresupuestoItem Pi { get; set;}
        public Ingreso Parent { get; set;}
        public static readonly string Regla1= "Regla1";
    }


    public class IngresoItemAlCrearValidador: AbstractValidator<IngresoItemAlCrear>
    {
        public IngresoItemAlCrearValidador()
        {
            RuleSet(IngresoItemAlCrear.Regla1, ()=>{
                RuleFor(x=>x.Prs.Activo).Must(r=>true).
                    WithMessage("Presupuesto No Activo").
                    WithErrorCode("PrespuestoNoActivo");

                RuleFor(x=>x.CentroItem.Activo).Must(r=>true).
                    WithMessage("Centro No Activo").
                    WithErrorCode("CentroNoActivo");

                RuleFor(x=>x.NewItem.IdCentro).Must((e,idCentro)=>e.Prs.IdCentro==idCentro).
                    WithMessage(string.Format("Centro de Costo no Coincide (Ingresoitem-presupuesto)")).
                    WithErrorCode("CentroErroneo");
				                
                //RuleFor(x => x.Pi).SetValidator(new PresupuestoItemDetalleActivoValidador()); does not work!

                RuleFor(x => x.Pi.Codigo).Must(r=> r.IndexOf(".")==Definiciones.PrspPosicionPunto).WithMessage("Item de Presupuesto no es de Detalle").WithErrorCode("NoDetalle");           
                RuleFor(x => x.Pi.Activo).Must(r=> true).WithMessage("Item de Presupuesto  esta desactivado").WithErrorCode("NoActivo");
                //TODO: poner la regla aqui.... para que sean de detalle y activo...
            });
        }
    }
}

