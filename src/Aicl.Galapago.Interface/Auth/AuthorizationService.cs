using System;
using System.Collections.Generic;
using System.Linq;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using Aicl.Galapago.BusinessLogic;
namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	public class AuthorizationService:RestServiceBase<Authorization>
	{
		public Factory Factory{ get; set;}
		
		public override object OnPost (Authorization request)
		{
			
			return  request.GetAuthorizations(Factory, RequestContext);
			 
		}
		
	}
}