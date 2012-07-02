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
	[Permission("Centro.read")]
	[Permission(ApplyTo.Post, "Centro.create")]	
	[Permission(ApplyTo.Put , "Centro.update")]	
	[Permission(ApplyTo.Delete, "Centro.destroy")]
	public class CentroService:AppRestService<Centro>
	{
	}
}