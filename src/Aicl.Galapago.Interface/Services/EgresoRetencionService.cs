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
	[Permission("EgresoRetencion.read")]
	[Permission(ApplyTo.Post, "EgresoRetencion.create")]	
	[Permission(ApplyTo.Put , "EgresoRetencion.update")]	
	[Permission(ApplyTo.Delete, "EgresoRetencion.destroy")]
	public class EgresoRetencionService:AppRestService<EgresoRetencion>
	{
	}
}