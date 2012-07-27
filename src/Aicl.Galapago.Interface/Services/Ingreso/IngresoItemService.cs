using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("IngresoItem.read")]
	[Permission(ApplyTo.Post, "IngresoItem.create")]	
	[Permission(ApplyTo.Put , "IngresoItem.update")]	
	[Permission(ApplyTo.Delete, "IngresoItem.destroy")]
	public class IngresoItemService:AppRestService<IngresoItem>
	{
        public override object OnGet (IngresoItem request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<IngresoItem>>(e,"GetErrorIngresoItem");
			}
        }

        public override object OnPost (IngresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			try{
            	return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<IngresoItem>>(e,"PostErrorIngresoItem");
			}
        }

        public override object OnPut (IngresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			try{
            	return request.Put(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<IngresoItem>>(e,"PutErrorIngresoItem");
			}
        }

        public override object OnDelete (IngresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Delete(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<IngresoItem>>(e,"DeleteErrorIngresoItem");
			}
        }
	}
}