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
	[Permission("MatriculaFacturado.read")]
	[Permission(ApplyTo.Post, "MatriculaFacturado.create")]	
	[Permission(ApplyTo.Put , "MatriculaFacturado.update")]	
	[Permission(ApplyTo.Delete, "MatriculaFacturado.destroy")]
	public class MatriculaFacturadoService:AppRestService<MatriculaFacturado>
	{
	}
}