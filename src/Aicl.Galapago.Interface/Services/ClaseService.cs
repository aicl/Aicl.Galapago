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
	[Permission("Clase.read")]
	[Permission(ApplyTo.Post, "Clase.create")]	
	[Permission(ApplyTo.Put , "Clase.update")]	
	[Permission(ApplyTo.Delete, "Clase.destroy")]
	public class ClaseService:AppRestService<Clase>
	{
	}
}