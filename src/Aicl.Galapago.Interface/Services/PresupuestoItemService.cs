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
	[Permission("PresupuestoItem.read")]
	[Permission(ApplyTo.Post, "PresupuestoItem.create")]	
	[Permission(ApplyTo.Put , "PresupuestoItem.update")]	
	[Permission(ApplyTo.Delete, "PresupuestoItem.destroy")]
	public class PresupuestoItemService:AppRestService<PresupuestoItem>
	{
	}
}