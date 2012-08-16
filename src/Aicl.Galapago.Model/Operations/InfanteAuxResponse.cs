using System.Collections.Generic;
using ServiceStack.ServiceInterface.ServiceModel;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.Model.Operations
{
    public class  InfanteAuxResponse:IHasResponseStatus
    {
        public InfanteAuxResponse ()
        {
            ResponseStatus= new ResponseStatus();
			CursoList = new List<Curso>();
			ClaseList = new List<Clase>();
            
        }

        
        public List<Curso> CursoList{get; set;}

		public List<Clase> ClaseList{get; set;}

		public ResponseStatus ResponseStatus { get; set; }
        
    }
}

