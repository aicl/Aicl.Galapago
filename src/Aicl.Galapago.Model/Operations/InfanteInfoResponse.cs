using System.Collections.Generic;
using ServiceStack.ServiceInterface.ServiceModel;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.Model.Operations
{
    public class  InfanteInfoResponse:IHasResponseStatus
    {
        public InfanteInfoResponse ()
        {
            ResponseStatus= new ResponseStatus();
            InfantePadreList= new List<InfantePadre>();
            InfanteAcudienteList= new List<InfanteAcudiente>();
            MatriculaList = new List<Matricula>();
            PensionList = new List<Pension>();
        }

        //public int Id{get;set;}

        public List<InfantePadre> InfantePadreList{get; set;}

        public List<InfanteAcudiente> InfanteAcudienteList{get; set;}

        public List<Matricula> MatriculaList{get; set;}

        public List<Pension> PensionList{get;set;}
        
        public ResponseStatus ResponseStatus { get; set; }
    }
}
