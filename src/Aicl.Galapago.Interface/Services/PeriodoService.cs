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
	[Permission("Periodo.read")]
	[Permission(ApplyTo.Post, "Periodo.create")]	
	[Permission(ApplyTo.Put , "Periodo.update")]	
	[Permission(ApplyTo.Delete, "Periodo.destroy")]
	public class PeriodoService:AppRestService<Periodo>
	{
	}
}