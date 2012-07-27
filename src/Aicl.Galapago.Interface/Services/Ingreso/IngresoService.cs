using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("Ingreso.read")]
	[Permission(ApplyTo.Post, "Ingreso.create")]	
	[Permission(ApplyTo.Put , "Ingreso.update")]	
	[Permission(ApplyTo.Delete, "Ingreso.destroy")]
	public class IngresoService:AppRestService<Ingreso>
	{
        public override object OnGet (Ingreso request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<Ingreso>>(e,"GetErrorIngreso");
			}
        }

		public override object OnPost (Ingreso request)
		{
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
				return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<Ingreso>>(e,"PostErrorIngreso");
			}
		}	

        public override object OnPut (Ingreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Put(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<Ingreso>>(e,"PutErrorIngreso");
			}
        }

        public override object OnPatch (Ingreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            var action= httpRequest.PathInfo.Substring(httpRequest.PathInfo.LastIndexOf("/")+1);
			try{
            	return request.Patch(Factory, httpRequest.GetSession(), action);
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<Ingreso>>(e,"PatchErrorIngreso");
			}
        }
	}
}