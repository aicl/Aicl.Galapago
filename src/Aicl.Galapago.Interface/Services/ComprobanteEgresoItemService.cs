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
        public override object OnPost (ComprobanteEgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Post(Factory, httpRequest.GetSession());
        }


        public override object OnPut (ComprobanteEgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Put(Factory,httpRequest.GetSession());
        }

        public override object OnDelete (ComprobanteEgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Delete(Factory,httpRequest.GetSession());
        }

	}
}