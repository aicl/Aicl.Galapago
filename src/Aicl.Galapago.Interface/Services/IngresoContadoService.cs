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
	[Permission("IngresoContado.read")]
	[Permission(ApplyTo.Post, "IngresoContado.create")]	
	[Permission(ApplyTo.Put , "IngresoContado.update")]	
	[Permission(ApplyTo.Delete, "IngresoContado.destroy")]
	public class IngresoContadoService:AppRestService<IngresoContado>
	{
	}
}