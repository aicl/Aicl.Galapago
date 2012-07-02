using System;
using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;
namespace Aicl.Galapago.Model.Types
{



    public class PresupuestoItemValidador:AbstractValidator<PresupuestoItem>
    {
        public PresupuestoItemValidador ()
        {

            RuleSet(Definiciones.PrspItemActivo, () => {
                RuleFor(x => x.Codigo).Must(r=> r.IndexOf(".")==Definiciones.PrspPosicionPunto).WithMessage("Item de Presupuesto no es de Detalle").WithErrorCode("NoDetalle");           
                RuleFor(x => x.Activo).Must(r=> true).WithMessage("Item de Presupuesto  esta desactivado").WithErrorCode("NoActivo");
            });

        }
    }

/*
    public class PresupuestoItemDetalleActivoValidador:AbstractValidator<PresupuestoItem>
    {
        public PresupuestoItemDetalleActivoValidador ()
        {
            RuleFor(x => x.Codigo).Must(r=> r.IndexOf(".")==Definiciones.PrspPosicionPunto).WithMessage("Item de Presupuesto no es de Detalle").WithErrorCode("NoDetalle");           
            RuleFor(x => x.Activo).Must(r=> true).WithMessage("Item de Presupuesto  esta desactivado").WithErrorCode("NoActivo");

        }
    }

*/

}



