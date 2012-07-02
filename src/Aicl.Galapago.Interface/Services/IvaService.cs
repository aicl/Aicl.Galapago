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
	[Permission("Iva.read")]
	[Permission(ApplyTo.Post, "Iva.create")]	
	[Permission(ApplyTo.Put , "Iva.update")]	
	[Permission(ApplyTo.Delete, "Iva.destroy")]
	public class IvaService:AppRestService<Iva>
	{
	}
}