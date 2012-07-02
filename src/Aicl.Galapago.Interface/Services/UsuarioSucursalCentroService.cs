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
	[Permission("UsuarioSucursalCentro.read")]
	[Permission(ApplyTo.Post, "UsuarioSucursalCentro.create")]	
	[Permission(ApplyTo.Put , "UsuarioSucursalCentro.update")]	
	[Permission(ApplyTo.Delete, "UsuarioSucursalCentro.destroy")]
	public class UsuarioSucursalCentroService:AppRestService<UsuarioSucursalCentro>
	{
	}
}