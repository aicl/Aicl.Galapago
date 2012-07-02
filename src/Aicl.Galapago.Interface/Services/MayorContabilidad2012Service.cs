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
	[Permission("MayorContabilidad2012.read")]
	[Permission(ApplyTo.Post, "MayorContabilidad2012.create")]	
	[Permission(ApplyTo.Put , "MayorContabilidad2012.update")]	
	[Permission(ApplyTo.Delete, "MayorContabilidad2012.destroy")]
	public class MayorContabilidad2012Service:AppRestService<MayorContabilidad2012>
	{
	}
}