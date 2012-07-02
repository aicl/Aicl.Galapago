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
using Aicl.Galapago.DataAccess;
namespace Aicl.Galapago.BusinessLogic
{
	public static class CuentaExtensiones
	{

		
		public static void ValidateAndThrowHttpError(this Cuenta request, string ruleSet)
		{
			CuentaValidator av = new CuentaValidator();
			av.ValidateAndThrowHttpError(request, ruleSet);
		}
		
		
		public static void AssertExists(Cuenta request, int id)
		{
			if( request== default(Cuenta))
				throw new HttpError(
						string.Format("No existe Cuenta con Id:'{0}'", id));			
		}
	}
}

