using ServiceStack.FluentValidation;
namespace Aicl.Galapago.Model.Types
{
    public class ComprobanteIngresoRetencionValidator:AbstractValidator<ComprobanteIngresoRetencion>
    {
        public ComprobanteIngresoRetencionValidator ()
        {
            RuleSet(Operaciones.Create, () => {

                RuleFor(x => x.Id).Must(x=> x==default(int)).
                    WithMessage("Se debe omitir el Id").
                        WithErrorCode("ConId");

                RuleFor(x => x.Valor).Must(x=>x!=default(decimal)).
                    WithMessage("Debe Indicar el valor de la Retencion").
                        WithErrorCode("SinValor");

                RuleFor(x=> x.IdComprobanteIngresoItem).Must(x=> x!=default(int)).
                    WithMessage("Debe inidicar el ingreso").
                        WithErrorCode("SinIdComprobanteIngresoItem");

                RuleFor(x=> x.IdComprobanteIngreso).Must(x=> x!=default(int)).
                    WithMessage("Debe inidicar el Comprobante de ingreso").
                        WithErrorCode("SinIdComprobanteIngreso");


            });
        }
    }
}
