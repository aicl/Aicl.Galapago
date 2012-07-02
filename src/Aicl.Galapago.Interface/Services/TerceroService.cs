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
	[Permission("Tercero.read")]
	[Permission(ApplyTo.Post, "Tercero.create")]	
	[Permission(ApplyTo.Put , "Tercero.update")]	
	[Permission(ApplyTo.Delete, "Tercero.destroy")]
	public class TerceroService:AppRestService<Tercero>
	{
	}
}