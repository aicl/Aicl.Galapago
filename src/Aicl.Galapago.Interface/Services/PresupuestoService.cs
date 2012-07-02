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
	[Permission("Presupuesto.read")]
	[Permission(ApplyTo.Post, "Presupuesto.create")]	
	[Permission(ApplyTo.Put , "Presupuesto.update")]	
	[Permission(ApplyTo.Delete, "Presupuesto.destroy")]
	public class PresupuestoService:AppRestService<Presupuesto>
	{
	}
}