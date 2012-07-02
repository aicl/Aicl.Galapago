using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;

namespace Aicl.Galapago.DataAccess
{
	public static class SucursalExtensiones
	{
	
		public static Response<Sucursal> Get(this Sucursal request, 
		                                           Factory factory, IRequestContext requestContext)
		{
		
			var cacheKey = UrnId.Create<Sucursal>("Id");
			List<Sucursal> result = requestContext.GetFromCache( cacheKey, () =>
			{
				return factory.DbFactory.Exec( dbCmd=>
					dbCmd.Select<Sucursal>() );
			}) as List<Sucursal>;
			
			return new Response<Sucursal>(){
				Data= result
			};
		
		}
	}
}

