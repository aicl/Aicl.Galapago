using ServiceStack.FluentValidation;
namespace Aicl.Galapago.Model.Types
{
    public class ComprobanteIngresoItemValidator:AbstractValidator<ComprobanteIngresoItem>
    {
        public ComprobanteIngresoItemValidator ()
        {
            RuleSet(Operaciones.Create, () => {
                RuleFor(x => x.Id).Must(x=> x==default(int)).
                    WithMessage("Se debe omitir el Id").WithErrorCode("ConId");

                RuleFor(x => x.IdIngreso ).Must(x=> x!=default(int)).
                    WithMessage("Debe Indicar el IdIngreso").WithErrorCode("SinIdIngreso");

                RuleFor(x => x.IdComprobanteIngreso).Must(x=>x!=default(int)).
                    WithMessage("Debe Indicar el IdComprobanteIngreso").WithErrorCode("SinIdComprobanteIngreso");

                RuleFor(x => x.Abono).Must(x=>x!=0).
                    WithMessage("Debe Indicar Valor a Pagar").WithErrorCode("SinValor");
            });
        }
    }
}

