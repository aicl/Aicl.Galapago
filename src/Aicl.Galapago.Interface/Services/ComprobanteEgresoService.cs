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
	[Permission("ComprobanteEgreso.read")]
	[Permission(ApplyTo.Post, "ComprobanteEgreso.create")]	
	[Permission(ApplyTo.Put , "ComprobanteEgreso.update")]	
	[Permission(ApplyTo.Delete, "ComprobanteEgreso.destroy")]
	public class ComprobanteEgresoService:AppRestService<ComprobanteEgreso>
	{
        public override object OnPost (ComprobanteEgreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Post(Factory, httpRequest.GetSession());
        }
        
        
        public override object OnGet (ComprobanteEgreso request)
        {
            return OnPost(request);
        }

        public override object OnPut (ComprobanteEgreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Put(Factory,httpRequest.GetSession());
        }


        public override object OnPatch (ComprobanteEgreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            var action= httpRequest.PathInfo.Substring(httpRequest.PathInfo.LastIndexOf("/")+1);
            return request.Patch(Factory, httpRequest.GetSession(), action);
        }

	}
}