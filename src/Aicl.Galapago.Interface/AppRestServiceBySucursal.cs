/*
using System;
using System.Linq;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface.Auth;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;


namespace Aicl.Galapago.Interface
{
	public class AppRestServiceBySucursal<T>:RestServiceBase<T> where T:IHasIdSucursal, new()
	{
		public Factory Factory{ get; set;}
		
		protected override void OnBeforeExecute (T request)
		{
			
			var httpReq = RequestContext.Get<IHttpRequest>();
			var idUsuario = int.Parse(httpReq.GetSession().UserAuthId);
			var usc = Factory.GetByIdUsuario<UsuarioSucursalCentro>(idUsuario,RequestContext);
			
			var httpMetod= httpReq.HttpMethod;
			switch(httpMetod){
			case "PUT":
			case "PATCH":
			case "POST":
				if(request.IdSucursal== default(int))
					throw HttpError.Unauthorized("Debe Indicar la sucursal");
				if(usc.FirstOrDefault(r=>r.IdSucursal==request.IdSucursal)==default(UsuarioSucursalCentro))
					throw HttpError.Unauthorized("Sucursal no autorizada");
				break;
			case "GET":
				break;
				
			case "DELETE":
				break;	
			
			}
			Console.WriteLine("{0}--{1}",httpReq.OperationName,httpReq.HttpMethod);
		}
		
		
	}
}

*/