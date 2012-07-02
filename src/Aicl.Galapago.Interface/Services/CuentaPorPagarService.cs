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
	[Permission("CuentaPorPagar.read")]
	[Permission(ApplyTo.Post, "CuentaPorPagar.create")]	
	[Permission(ApplyTo.Put , "CuentaPorPagar.update")]	
	[Permission(ApplyTo.Delete, "CuentaPorPagar.destroy")]
	public class CuentaPorPagarService:AppRestService<CuentaPorPagar>
	{
	}
}