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
	[Permission("IngresoItem.read")]
	[Permission(ApplyTo.Post, "IngresoItem.create")]	
	[Permission(ApplyTo.Put , "IngresoItem.update")]	
	[Permission(ApplyTo.Delete, "IngresoItem.destroy")]
	public class IngresoItemService:AppRestService<IngresoItem>
	{
	}
}