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
	[Permission("Infante.read")]
	[Permission(ApplyTo.Post, "Infante.create")]	
	[Permission(ApplyTo.Put , "Infante.update")]	
	[Permission(ApplyTo.Delete, "Infante.destroy")]
	public class InfanteService:AppRestService<Infante>
	{
	}
}