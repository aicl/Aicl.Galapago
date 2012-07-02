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
	[Permission("MayorPresupuesto2012.read")]
	[Permission(ApplyTo.Post, "MayorPresupuesto2012.create")]	
	[Permission(ApplyTo.Put , "MayorPresupuesto2012.update")]	
	[Permission(ApplyTo.Delete, "MayorPresupuesto2012.destroy")]
	public class MayorPresupuesto2012Service:AppRestService<MayorPresupuesto2012>
	{
	}
}