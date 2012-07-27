using ServiceStack.FluentValidation;
namespace Aicl.Galapago.Model.Types
{
    public class ComprobanteEgresoRetencionValidator:AbstractValidator<ComprobanteEgresoRetencion>
    {
        public ComprobanteEgresoRetencionValidator ()
        {
            RuleSet(Operaciones.Create, () => {

                RuleFor(x => x.Id).Must(x=> x==default(int)).
                    WithMessage("Se debe omitir el Id").
                        WithErrorCode("ConId");

                RuleFor(x => x.Valor).Must(x=>x!=default(decimal)).
                    WithMessage("Debe Indicar el valor de la Retencion").
                        WithErrorCode("SinValor");

                RuleFor(x=> x.IdComprobanteEgresoItem).Must(x=> x!=default(int)).
                    WithMessage("Debe inidicar el egreso").
                        WithErrorCode("SinIdComprobanteEgresoItem");

                RuleFor(x=> x.IdComprobanteEgreso).Must(x=> x!=default(int)).
                    WithMessage("Debe inidicar el Comprobante de egreso").
                        WithErrorCode("SinIdComprobanteEgreso");


            });
        }
    }
}

/*
 * RuleFor(x => x.TipoPartida).Must(r=> r==2).
                    WithMessage("Tipo de Partida debe ser 2=Credito").
                        WithErrorCode("ErrorTipoPartida");           

*/
