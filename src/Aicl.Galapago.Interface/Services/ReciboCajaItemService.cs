using System;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("ReciboCajaItem.read")]
	[Permission(ApplyTo.Post, "ReciboCajaItem.create")]	
	[Permission(ApplyTo.Put , "ReciboCajaItem.update")]	
	[Permission(ApplyTo.Delete, "ReciboCajaItem.destroy")]
	public class ReciboCajaItemService:AppRestService<ReciboCajaItem>
	{
	}
}