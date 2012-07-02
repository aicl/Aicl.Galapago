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
	[Permission("ReciboCaja.read")]
	[Permission(ApplyTo.Post, "ReciboCaja.create")]	
	[Permission(ApplyTo.Put , "ReciboCaja.update")]	
	[Permission(ApplyTo.Delete, "ReciboCaja.destroy")]
	public class ReciboCajaService:AppRestService<ReciboCaja>
	{
	}
}