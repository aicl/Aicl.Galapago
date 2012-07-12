using System;
using ServiceStack.Common;
using ServiceStack.FluentValidation;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
    public class Egresos
    {
        public Egreso Viejo{get; set;}
        public Egreso Nuevo{get; set;}

        public Egresos ()
        {
        }

    }

    public class EgresosValidator:AbstractValidator<Egresos>
    {
        public EgresosValidator()
        {
            Action common=()=>{
            RuleFor(e => e.Nuevo).Must((e,nuevo)=> e.Viejo.CodigoDocumento==nuevo.CodigoDocumento ).
                    When(e=> !e.Nuevo.CodigoDocumento.IsNullOrEmpty()).
                    WithMessage("CodigoDocumento modificado").WithErrorCode("CodigoDocumentoModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Descripcion==nuevo.Descripcion).
                    When(e=> !e.Nuevo.Descripcion.IsNullOrEmpty()).
                    WithMessage("Descripcion modificada").WithErrorCode("DescripcionModificada");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.DiasCredito==nuevo.DiasCredito).
                    When(e=> e.Nuevo.DiasCredito!=default(short)).
                    WithMessage("DiasCredito modificado").WithErrorCode("DiasModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Documento==nuevo.Documento).
                    When(e=> !e.Nuevo.Documento.IsNullOrEmpty()).
                    WithMessage("Documento modificado").WithErrorCode("DocumentoModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Fecha==nuevo.Fecha).
                    When(e=> e.Nuevo.Fecha!=default(DateTime)).
                    WithMessage("Fecha modificada").WithErrorCode("FechaModificada");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.IdSucursal==nuevo.IdSucursal).
                    When(e=> e.Nuevo.IdSucursal!=default(int)).
                    WithMessage("Sucursal modificada").WithErrorCode("SucursalModificada");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.IdTercero==nuevo.IdTercero).
                    When(e=> e.Nuevo.IdTercero!=default(int)).
                    WithMessage("Tercero modificado").WithErrorCode("TerceroModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Numero==nuevo.Numero).
                    When(e=> e.Nuevo.Numero!=default(int)).
                    WithMessage("Numero modificado").WithErrorCode("NumeroModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Periodo==nuevo.Periodo).
                    When(e=> !e.Nuevo.Periodo.IsNullOrEmpty()).
                    WithMessage("Periodo modificado").WithErrorCode("PeriodoModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Saldo==nuevo.Saldo).
                    When(e=> e.Nuevo.Saldo!=default(decimal)).
                    WithMessage("Saldo modificado").WithErrorCode("SaldoModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.Valor==nuevo.Valor).
                    When(e=> e.Nuevo.Valor!=default(decimal)).
                    WithMessage("Valor modificado").WithErrorCode("ValorModificado");

            RuleFor(e=>e.Nuevo).Must((e,nuevo)=>e.Viejo.IdTerceroReceptor==nuevo.IdTerceroReceptor).
                When(e=> e.Nuevo.IdTerceroReceptor.HasValue).
                WithMessage("Tercero Receptor Modificado").WithErrorCode("TerceroModificado");

            };

            RuleSet(Operaciones.Asentar, () => common() );
            RuleSet(Operaciones.Reversar, () => common() );
            RuleSet(Operaciones.Anular, () => common() );


            RuleSet(Operaciones.Update, () =>{
                RuleFor(x => x.Nuevo).Must((x, nuevo)=> x.Viejo.IdSucursal==nuevo.IdSucursal).
                    When(x=> x.Nuevo.IdSucursal!=default(int)).
                    WithMessage("No se debe modificar la Sucursal").WithErrorCode("SucursalModificada");

                RuleFor(x=>x.Nuevo).Must((x,nuevo)=> x.Viejo.Saldo==nuevo.Saldo).
                When(x=> x.Nuevo.Saldo!=default(decimal)).
                    WithMessage("Saldo modificado").WithErrorCode("SaldoModificado");

                 RuleFor(x => x.Nuevo).Must((x, nuevo)=> x.Viejo.Valor==nuevo.Valor).
                    When(x=> x.Nuevo.Valor!=default(decimal)).
                        WithMessage("Valor modificado").WithErrorCode("ValorModificado");

                RuleFor(x=>x.Nuevo).Must((x,nuevo)=>x.Viejo.Numero==nuevo.Numero).
                    When(x=> x.Nuevo.Numero!=default(int)).
                    WithMessage("Numero modificado").WithErrorCode("NumeroModificado");


                RuleFor(x=>x.Nuevo).Must((x,nuevo)=>x.Viejo.CodigoDocumento==nuevo.CodigoDocumento).
                    When(x=> !x.Nuevo.CodigoDocumento.IsNullOrEmpty()).
                    WithMessage("Tipo Documento Modificado").WithErrorCode("TipoDocumentoModificado");

            });

        }
    }

}


//When(e => !e.Nuevo.CodigoDocumento.IsNullOrEmpty(), () => {
//    RuleFor( r => new{ Nuevo=r.Nuevo.CodigoDocumento, Viejo=r.Viejo.CodigoDocumento}).Must(x=>x.Nuevo==x.Viejo);
//});