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
	[Permission("Ingreso.read")]
	[Permission(ApplyTo.Post, "Ingreso.create")]	
	[Permission(ApplyTo.Put , "Ingreso.update")]	
	[Permission(ApplyTo.Delete, "Ingreso.destroy")]
	public class IngresoService:AppRestService<Ingreso>
	{
	}
}