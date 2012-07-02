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
	[Permission("CuentaPorCobrar.read")]
	[Permission(ApplyTo.Post, "CuentaPorCobrar.create")]	
	[Permission(ApplyTo.Put , "CuentaPorCobrar.update")]	
	[Permission(ApplyTo.Delete, "CuentaPorCobrar.destroy")]
	public class CuentaPorCobrarService:AppRestService<CuentaPorCobrar>
	{
	}
}