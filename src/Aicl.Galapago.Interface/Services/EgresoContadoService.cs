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
	[Permission("EgresoContado.read")]
	[Permission(ApplyTo.Post, "EgresoContado.create")]	
	[Permission(ApplyTo.Put , "EgresoContado.update")]	
	[Permission(ApplyTo.Delete, "EgresoContado.destroy")]
	public class EgresoContadoService:AppRestService<EgresoContado>
	{
	}
}