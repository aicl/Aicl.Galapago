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
	[Permission("Consecutivo.read")]
	[Permission(ApplyTo.Post, "Consecutivo.create")]	
	[Permission(ApplyTo.Put , "Consecutivo.update")]	
	[Permission(ApplyTo.Delete, "Consecutivo.destroy")]
	public class ConsecutivoService:AppRestService<Consecutivo>
	{
	}
}