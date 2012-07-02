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
	[Permission("Ciudad.read")]
	[Permission(ApplyTo.Post, "Ciudad.create")]	
	[Permission(ApplyTo.Put , "Ciudad.update")]	
	[Permission(ApplyTo.Delete, "Ciudad.destroy")]
	public class CiudadService:AppRestService<Ciudad>
	{
	}
}