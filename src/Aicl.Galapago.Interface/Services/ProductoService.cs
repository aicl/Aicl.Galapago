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
	[Permission("Producto.read")]
	[Permission(ApplyTo.Post, "Producto.create")]	
	[Permission(ApplyTo.Put , "Producto.update")]	
	[Permission(ApplyTo.Delete, "Producto.destroy")]
	public class ProductoService:AppRestService<Producto>
	{
	}
}