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
	[Permission("MatriculaProducto.read")]
	[Permission(ApplyTo.Post, "MatriculaProducto.create")]	
	[Permission(ApplyTo.Put , "MatriculaProducto.update")]	
	[Permission(ApplyTo.Delete, "MatriculaProducto.destroy")]
	public class MatriculaProductoService:AppRestService<MatriculaProducto>
	{
	}
}