using System;
using System.Collections.Generic;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;

using Aicl.Galapago.DataAccess;

using ServiceStack.ServiceInterface.ServiceModel;

namespace Aicl.Galapago.Interface
{

	[RequiresAuthenticate]
	[Permission("Asiento.read")]
	[Permission(ApplyTo.Post, "Asiento.create")]	
	[Permission(ApplyTo.Put , "Asiento.update")]	
	[Permission(ApplyTo.Delete, "Asiento.destroy")]
	public class AsientoService:AppRestService<Asiento>
	{
		public override object OnPost (Asiento request)
		{
		 	return request.Post(Factory, RequestContext); 
		}
		
		public override object OnDelete (Asiento request)
		{
			return request.Delete(Factory, RequestContext);
		}
		
		public override object OnPut (Asiento request)
		{
			return request.Put(Factory, RequestContext);
		}		
		
		public override object OnGet (Asiento request)
		{
			Console.WriteLine("reenviando Get a Delete");
			return request.Delete(Factory, RequestContext);
		}
			
		
		public override object OnPatch (Asiento request)
		{
			Console.WriteLine("OnPatch");
			var httpRequest = RequestContext.Get<IHttpRequest>();
			Console.WriteLine(httpRequest.QueryString["action"]);
			return base.OnGet(request);
		}
	}
}