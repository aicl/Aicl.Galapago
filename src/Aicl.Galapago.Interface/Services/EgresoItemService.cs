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
	[Permission("EgresoItem.read")]
	[Permission(ApplyTo.Post, "EgresoItem.create")]	
	[Permission(ApplyTo.Put , "EgresoItem.update")]	
	[Permission(ApplyTo.Delete, "EgresoItem.destroy")]
	public class EgresoItemService:AppRestService<EgresoItem>
	{

        public override object OnGet (EgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Get(Factory, httpRequest.GetSession());
        }

        public override object OnPost (EgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Post(Factory, httpRequest.GetSession());
        }


        public override object OnPut (EgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Put(Factory,httpRequest.GetSession());
        }

        public override object OnDelete (EgresoItem request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Delete(Factory,httpRequest.GetSession());
        }

	}
}