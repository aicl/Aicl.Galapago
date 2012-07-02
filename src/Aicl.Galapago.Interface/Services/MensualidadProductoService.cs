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
	[Permission("MensualidadProducto.read")]
	[Permission(ApplyTo.Post, "MensualidadProducto.create")]	
	[Permission(ApplyTo.Put , "MensualidadProducto.update")]	
	[Permission(ApplyTo.Delete, "MensualidadProducto.destroy")]
	public class MensualidadProductoService:AppRestService<MensualidadProducto>
	{
	}
}