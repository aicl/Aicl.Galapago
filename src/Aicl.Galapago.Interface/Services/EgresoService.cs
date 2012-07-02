using System;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("Egreso.read")]
	[Permission(ApplyTo.Post, "Egreso.create")]	
	[Permission(ApplyTo.Put , "Egreso.update")]	
	[Permission(ApplyTo.Delete, "Egreso.destroy")]
	public class EgresoService:AppRestService<Egreso>
	{

		public override object OnPost (Egreso request)
		{
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			return request.Post(Factory, httpRequest.GetSession());
		}
		
		
		public override object OnGet (Egreso request)
		{
			return OnPost(request);
		}

        public override object OnPut (Egreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Put(Factory,httpRequest.GetSession());
        }


        public override object OnPatch (Egreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       

            var action= httpRequest.PathInfo.Substring(httpRequest.PathInfo.LastIndexOf("/")+1);
            return request.Patch(Factory, httpRequest.GetSession(), action);
        }

    //TODO : borrar Mayor_Presupuesto para acomodar Saldo_anterior_08 default 0

    

	}
}