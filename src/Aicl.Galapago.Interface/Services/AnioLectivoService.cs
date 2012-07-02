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
	[Permission("AnioLectivo.read")]
	[Permission(ApplyTo.Post, "AnioLectivo.create")]	
	[Permission(ApplyTo.Put , "AnioLectivo.update")]	
	[Permission(ApplyTo.Delete, "AnioLectivo.destroy")]
	public class AnioLectivoService:AppRestService<AnioLectivo>
	{
	}
}