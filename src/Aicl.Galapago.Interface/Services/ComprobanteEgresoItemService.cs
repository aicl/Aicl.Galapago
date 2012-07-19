using System;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("ComprobanteEgresoItem.read")]
	[Permission(ApplyTo.Post, "ComprobanteEgresoItem.create")]	
	[Permission(ApplyTo.Put , "ComprobanteEgresoItem.update")]	
	[Permission(ApplyTo.Delete, "ComprobanteEgresoItem.destroy")]
	public class ComprobanteEgresoItemService:AppRestService<ComprobanteEgresoItem>
	{

		public override object OnGet (ComprobanteEgresoItem request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteEgresoItem>>(e,"GetErrorComprobanteEgresoItem");
			}
        }

        public override object OnPost (ComprobanteEgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteEgresoItem>>(e,"PostErrorComprobanteEgresoItem");
			}
        }


        public override object OnPut (ComprobanteEgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Put(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteEgresoItem>>(e,"PutErrorComprobanteEgresoItem");
			}
        }

        public override object OnDelete (ComprobanteEgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Delete(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteEgresoItem>>(e,"DeleteErrorComprobanteEgresoItem");
			}
        }

	}
}