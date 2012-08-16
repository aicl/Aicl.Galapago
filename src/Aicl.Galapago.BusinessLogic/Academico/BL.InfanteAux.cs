using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;

namespace Aicl.Galapago.BusinessLogic
{
	public static partial class BL
	{
		#region get
		public static InfanteAuxResponse Get(this InfanteAux request, Factory factory, IHttpRequest httpRequest)
		{

			var clase=new Clase();
			var claseResponse= clase.Get(factory,httpRequest);
			var curso= new Curso();
			var cursoResponse= curso.Get(factory,httpRequest);

			return new InfanteAuxResponse{
				ClaseList= claseResponse.Data,
				CursoList= cursoResponse.Data
			};


       
		}
		#endregion get
	}
}