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
	[Permission("CuentaPresupuesto.read")]
	[Permission(ApplyTo.Post, "CuentaPresupuesto.create")]	
	[Permission(ApplyTo.Put , "CuentaPresupuesto.update")]	
	[Permission(ApplyTo.Delete, "CuentaPresupuesto.destroy")]
	public class CuentaPresupuestoService:AppRestService<CuentaPresupuesto>
	{
	}
}