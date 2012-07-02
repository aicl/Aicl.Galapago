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
	[Permission("CuentaDinero.read")]
	[Permission(ApplyTo.Post, "CuentaDinero.create")]	
	[Permission(ApplyTo.Put , "CuentaDinero.update")]	
	[Permission(ApplyTo.Delete, "CuentaDinero.destroy")]
	public class CuentaDineroService:AppRestService<CuentaDinero>
	{
	}
}