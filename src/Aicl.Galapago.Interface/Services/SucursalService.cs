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
	[Permission("Sucursal.read")]
	[Permission(ApplyTo.Post, "Sucursal.create")]	
	[Permission(ApplyTo.Put , "Sucursal.update")]	
	[Permission(ApplyTo.Delete, "Sucursal.destroy")]
	public class SucursalService:AppRestService<Sucursal>
	{
	}
}