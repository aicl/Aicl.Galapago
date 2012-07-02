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
	[Permission("MatriculaPago.read")]
	[Permission(ApplyTo.Post, "MatriculaPago.create")]	
	[Permission(ApplyTo.Put , "MatriculaPago.update")]	
	[Permission(ApplyTo.Delete, "MatriculaPago.destroy")]
	public class MatriculaPagoService:AppRestService<MatriculaPago>
	{
	}
}