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
	[Permission("PeriodoSucursal.read")]
	[Permission(ApplyTo.Post, "PeriodoSucursal.create")]	
	[Permission(ApplyTo.Put , "PeriodoSucursal.update")]	
	[Permission(ApplyTo.Delete, "PeriodoSucursal.destroy")]
	public class PeriodoSucursalService:AppRestService<PeriodoSucursal>
	{
	}
}