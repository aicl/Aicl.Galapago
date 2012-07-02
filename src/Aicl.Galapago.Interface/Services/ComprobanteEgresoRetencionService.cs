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
        public override object OnPost (ComprobanteEgresoRetencion request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Post(Factory, httpRequest.GetSession());
        }


        public override object OnPut (ComprobanteEgresoRetencion request)
        {
            throw new HttpError("Operacion Update no implementada");
        }

        public override object OnDelete (ComprobanteEgresoRetencion request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Delete(Factory,httpRequest.GetSession());
        }

    }
}