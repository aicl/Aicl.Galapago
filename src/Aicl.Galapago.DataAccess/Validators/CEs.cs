using System;
using ServiceStack.Common;
using ServiceStack.FluentValidation;
using Aicl.Galapago.Model.Types;
namespace Aicl.Galapago.DataAccess
{
    public class CEs
    {
        public ComprobanteEgreso Nuevo {get; set;}

        public ComprobanteEgreso Viejo {get; set;}

        public CEs ()
        {
        }
    }


     public class CEsValidator:AbstractValidator<CEs>
    {
        public CEsValidator()
        {
            Action common=()=>{
           
            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Descripcion==nuevo.Descripcion).
                    When(e=> !e.Nuevo.Descripcion.IsNullOrEmpty()).
                    WithMessage("Descripcion modificada").WithErrorCode("DescripcionModificada");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Fecha==nuevo.Fecha).
                    When(e=> e.Nuevo.Fecha!=default(DateTime)).
                    WithMessage("Fecha modificada").WithErrorCode("FechaModificada");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.IdSucursal==nuevo.IdSucursal).
                    When(e=> e.Nuevo.IdSucursal!=default(int)).
                    WithMessage("Sucursal modificada").WithErrorCode("SucursalModificada");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.IdTercero==nuevo.IdTercero).
                    When(e=> e.Nuevo.IdTercero!=default(int)).
                    WithMessage("Tercero modificado").WithErrorCode("TerceroModificado");

            
            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Periodo==nuevo.Periodo).
                    When(e=> !e.Nuevo.Periodo.IsNullOrEmpty()).
                    WithMessage("Periodo modificado").WithErrorCode("PeriodoModificado");

            
            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Valor==nuevo.Valor).
                    When(e=> e.Nuevo.Valor!=default(decimal)).
                    WithMessage("Valor modificado").WithErrorCode("ValorModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.IdTerceroReceptor==nuevo.IdTerceroReceptor).
                When(e=> e.Nuevo.IdTerceroReceptor !=default(int)).
                WithMessage("Tercero Receptor Modificado").WithErrorCode("TerceroModificado");

      
            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.IdCuentaGiradora==nuevo.IdCuentaGiradora).
                When(e=> e.Nuevo.IdTerceroReceptor !=default(int)).
                WithMessage("Cuenta Giradora Modificada").WithErrorCode("CuentaGiradoraModificada");

            };
      

            RuleSet(Operaciones.Asentar, () => common() );
            RuleSet(Operaciones.Reversar, () => common() );
            RuleSet(Operaciones.Anular, () => common() );


            RuleSet(Operaciones.Update, () =>{
                RuleFor(x => x.Nuevo).Must((x, nuevo)=> x.Viejo.IdSucursal==nuevo.IdSucursal).
                    When(x=> x.Nuevo.IdSucursal!=default(int)).
                    WithMessage("No se debe modificar la Sucursal").WithErrorCode("SucursalModificada");
              
                 RuleFor(x => x.Nuevo).Must((x, nuevo)=> x.Viejo.Valor==nuevo.Valor).
                    When(x=> x.Nuevo.Valor!=default(decimal)).
                        WithMessage("Valor modificado").WithErrorCode("ValorModificado");

                  RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.IdTercero==nuevo.IdTercero).
                    When(e=> e.Nuevo.IdTercero!=default(int) && e.Viejo.Valor!=0).
                    WithMessage("Tercero modificado").WithErrorCode("TerceroModificado");


            });

        }
    }

}

