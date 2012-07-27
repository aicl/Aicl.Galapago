using ServiceStack.FluentValidation;
namespace Aicl.Galapago.Model.Types
{
    public class ComprobanteEgresoItemValidator:AbstractValidator<ComprobanteEgresoItem>
    {
        public ComprobanteEgresoItemValidator ()
        {
            RuleSet(Operaciones.Create, () => {
                RuleFor(x => x.Id).Must(x=> x==default(int)).
                    WithMessage("Se debe omitir el Id").WithErrorCode("ConId");

                RuleFor(x => x.IdEgreso ).Must(x=> x!=default(int)).
                    WithMessage("Debe Indicar el IdEgreso").WithErrorCode("SinIdEgreso");

                RuleFor(x => x.IdComprobanteEgreso).Must(x=>x!=default(int)).
                    WithMessage("Debe Indicar el IdComprobanteEgreso").WithErrorCode("SinIdComprobanteEgreso");

                RuleFor(x => x.Abono).Must(x=>x!=0).
                    WithMessage("Debe Indicar Valor a Pagar").WithErrorCode("SinValor");
            });
        }
    }
}

