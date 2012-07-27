using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("EgresoItem.read")]
	[Permission(ApplyTo.Post, "EgresoItem.create")]	
	[Permission(ApplyTo.Put , "EgresoItem.update")]	
	[Permission(ApplyTo.Delete, "EgresoItem.destroy")]
	public class EgresoItemService:AppRestService<EgresoItem>
	{
        public override object OnGet (EgresoItem request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<EgresoItem>>(e,"GetErrorEgresoItem");
			}
        }

        public override object OnPost (EgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			try{
            	return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<EgresoItem>>(e,"PostErrorEgresoItem");
			}
        }

        public override object OnPut (EgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			try{
            	return request.Put(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<EgresoItem>>(e,"PutErrorEgresoItem");
			}
        }

        public override object OnDelete (EgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Delete(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<EgresoItem>>(e,"DeleteErrorEgresoItem");
			}
        }
	}
}