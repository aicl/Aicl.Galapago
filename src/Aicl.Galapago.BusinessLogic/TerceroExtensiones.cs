using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceInterface;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
namespace Aicl.Galapago.BusinessLogic
{
	public static class TerceroExtensiones
	{	
		public static void ValidateAndThrowHttpError(this Tercero request, string ruleSet)
		{
			TerceroValidator av = new TerceroValidator();
			av.ValidateAndThrowHttpError(request, ruleSet);
		}
		
		
	}
}

