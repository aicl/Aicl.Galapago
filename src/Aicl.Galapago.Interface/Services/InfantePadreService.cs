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
	[Permission("InfantePadre.read")]
	[Permission(ApplyTo.Post, "InfantePadre.create")]	
	[Permission(ApplyTo.Put , "InfantePadre.update")]	
	[Permission(ApplyTo.Delete, "InfantePadre.destroy")]
	public class InfantePadreService:AppRestService<InfantePadre>
	{
	}
}