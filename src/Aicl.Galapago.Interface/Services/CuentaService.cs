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
	[Permission("Cuenta.read")]
	[Permission(ApplyTo.Post, "Cuenta.create")]	
	[Permission(ApplyTo.Put , "Cuenta.update")]	
	[Permission(ApplyTo.Delete, "Cuenta.destroy")]
	public class CuentaService:AppRestService<Cuenta>
	{
	}
}