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
	[Permission("SucursalCentro.read")]
	[Permission(ApplyTo.Post, "SucursalCentro.create")]	
	[Permission(ApplyTo.Put , "SucursalCentro.update")]	
	[Permission(ApplyTo.Delete, "SucursalCentro.destroy")]
	public class SucursalCentroService:AppRestService<SucursalCentro>
	{
	}
}