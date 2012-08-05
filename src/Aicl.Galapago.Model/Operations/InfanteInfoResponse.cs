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
            PadreList= new List<InfantePadre>();
            AcudienteList= new List<InfanteAcudiente>();
            MatriculaList = new List<Matricula>();
            PensionList = new List<Pension>();

        }

        public int Id{get;set;}

        public List<InfantePadre> PadreList{get; set;}

        public List<InfanteAcudiente> AcudienteList{get; set;}

        public List<Matricula> MatriculaList{get; set;}

        public List<Pension> PensionList{get;set;}
        
        public ResponseStatus ResponseStatus { get; set; }

        
    }
}
