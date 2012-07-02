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
	[Permission("GrupoProducto.read")]
	[Permission(ApplyTo.Post, "GrupoProducto.create")]	
	[Permission(ApplyTo.Put , "GrupoProducto.update")]	
	[Permission(ApplyTo.Delete, "GrupoProducto.destroy")]
	public class GrupoProductoService:AppRestService<GrupoProducto>
	{
	}
}