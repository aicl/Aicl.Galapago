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
	[Permission("RoleAndPermission.read")]
	[Permission(ApplyTo.Post, "RoleAndPermission.create")]	
	[Permission(ApplyTo.Put , "RoleAndPermission.update")]	
	[Permission(ApplyTo.Delete, "RoleAndPermission.destroy")]
	public class RoleAndPermissionService:AppRestService<RoleAndPermission>
	{
	}
}