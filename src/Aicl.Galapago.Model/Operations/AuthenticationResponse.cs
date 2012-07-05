using System;
using System.Collections.Generic;
using ServiceStack.ServiceInterface.ServiceModel;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.Model.Operations
{
	public class AuthenticationResponse:IHasResponseStatus
	{
		public AuthenticationResponse ()
		{
			ResponseStatus= new ResponseStatus();
			Permissions= new List<string>();
			Roles = new List<AuthRole>();
			Sucursales = new List<SucursalAutorizada>();
			Centros = new List<CentroAutorizado>();
            CodigosEgreso= new List<CodigoDocumento>();
            CodigosIngreso=new List<CodigoDocumento>();
		}
		
		public ResponseStatus ResponseStatus { get; set; }
		
		public List<string> Permissions {get; set;}
		public List<AuthRole> Roles {get; set;}
		public string DisplayName { get; set;}
		
		public List<SucursalAutorizada> Sucursales {get; set;}
		public List<CentroAutorizado> Centros {get; set;}
        public List<CodigoDocumento> CodigosEgreso {get;set;}
        public List<CodigoDocumento> CodigosIngreso {get;set;}
	}
}