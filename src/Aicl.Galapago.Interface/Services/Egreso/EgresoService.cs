using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("Egreso.read")]
	[Permission(ApplyTo.Post, "Egreso.create")]	
	[Permission(ApplyTo.Put , "Egreso.update")]	
	[Permission(ApplyTo.Delete, "Egreso.destroy")]
	public class EgresoService:AppRestService<Egreso>
	{
        public override object OnGet (Egreso request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<Egreso>>(e,"GetErrorEgreso");
			}
        }

		public override object OnPost (Egreso request)
		{
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
				return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<Egreso>>(e,"PostErrorEgreso");
			}
		}	

        public override object OnPut (Egreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Put(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<Egreso>>(e,"PutErrorEgreso");
			}
        }

        public override object OnPatch (Egreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            var action= httpRequest.PathInfo.Substring(httpRequest.PathInfo.LastIndexOf("/")+1);
			try{
            	return request.Patch(Factory, httpRequest.GetSession(), action);
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<Egreso>>(e,"PatchErrorEgreso");
			}
        }
	}
}