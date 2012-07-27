using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("ComprobanteIngresoItem.read")]
	[Permission(ApplyTo.Post, "ComprobanteIngresoItem.create")]	
	[Permission(ApplyTo.Put , "ComprobanteIngresoItem.update")]	
	[Permission(ApplyTo.Delete, "ComprobanteIngresoItem.destroy")]
	public class ComprobanteIngresoItemService:AppRestService<ComprobanteIngresoItem>
	{
		public override object OnGet (ComprobanteIngresoItem request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngresoItem>>(e,"GetErrorComprobanteIngresoItem");
			}
        }

        public override object OnPost (ComprobanteIngresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngresoItem>>(e,"PostErrorComprobanteIngresoItem");
			}
        }

        public override object OnPut (ComprobanteIngresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Put(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngresoItem>>(e,"PutErrorComprobanteIngresoItem");
			}
        }

        public override object OnDelete (ComprobanteIngresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Delete(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngresoItem>>(e,"DeleteErrorComprobanteIngresoItem");
			}
        }
	}
}