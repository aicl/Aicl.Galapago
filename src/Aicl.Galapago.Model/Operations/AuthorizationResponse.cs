using System;
using System.Collections.Generic;
using ServiceStack.ServiceInterface.ServiceModel;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.Model.Operations
{
	public class AuthorizationResponse:IHasResponseStatus
	{
		public AuthorizationResponse ()
		{
			ResponseStatus= new ResponseStatus();
			Permissions= new List<string>();
			Roles = new List<AuthRole>();
			Sucursales= new List<SucursalAutorizada>();
			Centros = new List<CentroAutorizado>();
            CodigosDocumento = new List<CodigoDocumento>();
            Rubros = new List<Rubro>();
			Ciudades = new List<Ciudad>();
			TiposDocumento = new List<TipoDocumento>();
		}
		
		public ResponseStatus ResponseStatus { get; set; }
		
		public List<string> Permissions {get; set;}
		public List<AuthRole> Roles {get; set;}
		public List<SucursalAutorizada> Sucursales {get; set;}
		public List<CentroAutorizado> Centros {get; set;}
		public List<CodigoDocumento> CodigosDocumento {get;set;}
        public List<Rubro> Rubros {get; set;}
		public List<Ciudad> Ciudades {get; set;}
		public List<TipoDocumento> TiposDocumento {get; set;}
	}
}