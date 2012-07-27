using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("ComprobanteIngreso.read")]
	[Permission(ApplyTo.Post, "ComprobanteIngreso.create")]	
	[Permission(ApplyTo.Put , "ComprobanteIngreso.update")]	
	[Permission(ApplyTo.Delete, "ComprobanteIngreso.destroy")]
	public class ComprobanteIngresoService:AppRestService<ComprobanteIngreso>
	{
		public override object OnGet (ComprobanteIngreso request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngreso>>(e,"GetErrorComprobanteIngreso");
			}
        }

        public override object OnPost (ComprobanteIngreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			try{
            	return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngreso>>(e,"PostErrorComprobanteIngreso");
			}
        }       

        public override object OnPut (ComprobanteIngreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Put(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngreso>>(e,"PutErrorComprobanteIngreso");
			}
        }

        public override object OnPatch (ComprobanteIngreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            var action= httpRequest.PathInfo.Substring(httpRequest.PathInfo.LastIndexOf("/")+1);
			try{
            	return request.Patch(Factory, httpRequest.GetSession(), action);
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngreso>>(e,"PatchErrorComprobanteIngreso");
			}
        }
	}
}