using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
    [RequiresAuthenticate]
    [Permission("ComprobanteIngresoRetencion.read")]
    [Permission(ApplyTo.Post, "ComprobanteIngresoRetencion.create")]  
    [Permission(ApplyTo.Put , "ComprobanteIngresoRetencion.update")]  
    [Permission(ApplyTo.Delete, "ComprobanteIngresoRetencion.destroy")]
    public class ComprobanteIngresoRetencionService:AppRestService<ComprobanteIngresoRetencion>
    {
		public override object OnGet (ComprobanteIngresoRetencion request)
        {
			try{
				return request.Get(Factory, RequestContext.Get<IHttpRequest>());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngresoRetencion>>(e,"GetErrorComprobanteIngresoRetencion");
			}
        }

        public override object OnPost (ComprobanteIngresoRetencion request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			try{
            	return request.Post(Factory, httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngresoRetencion>>(e,"PostErrorComprobanteIngresoRetencion");
			}
        }

        public override object OnPut (ComprobanteIngresoRetencion request)
        {
            return HttpResponse.
				ErrorResult<Response<ComprobanteIngresoRetencion>>("Put NoImplementado para ComprobanteIngresoRetencion",
				                                                  "PutErrorComprobanteIngresoRetencion");
        }

        public override object OnDelete (ComprobanteIngresoRetencion request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
			try{
            	return request.Delete(Factory,httpRequest.GetSession());
			}
			catch(Exception e){
				return HttpResponse.ErrorResult<Response<ComprobanteIngresoRetencion>>(e,"DeleteErrorComprobanteIngresoRetencion");
			}
        }
    }
}