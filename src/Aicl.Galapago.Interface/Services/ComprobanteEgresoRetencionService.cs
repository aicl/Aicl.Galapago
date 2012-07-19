using System;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
    [RequiresAuthenticate]
    [Permission("ComprobanteEgresoRetencion.read")]
    [Permission(ApplyTo.Post, "ComprobanteEgresoRetencion.create")]  
    [Permission(ApplyTo.Put , "ComprobanteEgresoRetencion.update")]  
    [Permission(ApplyTo.Delete, "ComprobanteEgresoRetencion.destroy")]
    public class ComprobanteEgresoRetencionService:AppRestService<ComprobanteEgresoRetencion>
    {

		public override object OnGet (ComprobanteEgresoRetencion request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteEgresoRetencion>>(e,"GetErrorComprobanteEgresoRetencion");
			}
        }

        public override object OnPost (ComprobanteEgresoRetencion request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			try{
            	return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteEgresoRetencion>>(e,"PostErrorComprobanteEgresoRetencion");
			}
        }


        public override object OnPut (ComprobanteEgresoRetencion request)
        {
            return HttpResponse.
				ErrorResult<Response<ComprobanteEgresoRetencion>>("Put NoImplementado para ComprobanteEgresoRetencion",
				                                                  "PutErrorComprobanteEgresoRetencion");
        }

        public override object OnDelete (ComprobanteEgresoRetencion request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Delete(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteEgresoRetencion>>(e,"DeleteErrorComprobanteEgresoRetencion");
			}
        }

    }
}