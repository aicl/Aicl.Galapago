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
	[Permission("Departamento.read")]
	[Permission(ApplyTo.Post, "Departamento.create")]	
	[Permission(ApplyTo.Put , "Departamento.update")]	
	[Permission(ApplyTo.Delete, "Departamento.destroy")]
	public class DepartamentoService:AppRestService<Departamento>
	{
	}
}