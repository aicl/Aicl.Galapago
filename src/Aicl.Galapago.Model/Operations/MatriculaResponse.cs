using System.Collections.Generic;
using ServiceStack.ServiceInterface.ServiceModel;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.Model.Operations
{
	public class MatriculaResponse:Response<Matricula>, IHasResponseStatus
	{
		public MatriculaResponse ():base()
		{
			MatriculaItemList= new List<MatriculaItem>();
		}

		public List<MatriculaItem> MatriculaItemList {get; set;}
	}
}

