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
	[Permission("TipoDocumento.read")]
	[Permission(ApplyTo.Post, "TipoDocumento.create")]	
	[Permission(ApplyTo.Put , "TipoDocumento.update")]	
	[Permission(ApplyTo.Delete, "TipoDocumento.destroy")]
	public class TipoDocumentoService:AppRestService<TipoDocumento>
	{
	}
}