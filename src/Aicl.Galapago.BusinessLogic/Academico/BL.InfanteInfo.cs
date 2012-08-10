using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using ServiceStack.ServiceHost;
using Mono.Linq.Expressions;

namespace Aicl.Galapago.BusinessLogic
{
    public static partial class BL
    {
        #region get
        public static InfanteInfoResponse Get(this InfanteInfo request, Factory factory, IHttpRequest httpRequest)
        {
            return factory.Execute(proxy=>{

				var padres = proxy.Get<InfantePadre>(q=>q.IdInfante==request.Id);
				var acudientes =proxy.Get<InfanteAcudiente>(q=>q.IdInfante==request.Id);
				var matriculas = proxy.Get<Matricula>(
					ev=>ev.Where(q=>q.IdInfante==request.Id).OrderByDescending(q=>q.FechaInicio));

				var predicate= PredicateBuilder.False<Pension>();

				foreach( Matricula m in matriculas)
				{
					var idMatricula= m.Id;
					predicate= predicate.OrElse(q=>q.IdMatricula==idMatricula);
				}

				var pensiones = proxy.Get<Pension>(ev=> ev.Where(predicate).OrderByDescending(q=>q.Periodo));

                return new InfanteInfoResponse{
					PadreList= padres,
					AcudienteList=acudientes,
					MatriculaList=matriculas,
					PensionList= pensiones
                };
            });
        }
        #endregion get
    }
}

