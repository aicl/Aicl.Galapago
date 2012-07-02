using System;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("AsientoItem.read")]
	[Permission(ApplyTo.Post, "AsientoItem.create")]	
	[Permission(ApplyTo.Put , "AsientoItem.update")]	
	[Permission(ApplyTo.Delete, "AsientoItem.destroy")]
	public class AsientoItemService:AppRestService<AsientoItem>
	{
		public override object OnPost (AsientoItem request)
		{
			return request.Post(Factory, RequestContext); 
		}
	}
}