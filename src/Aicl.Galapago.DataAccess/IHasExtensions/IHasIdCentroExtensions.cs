using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.Redis;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
	public static class IHasIdCentroExtensions
	{
		public static void CheckCentro<T>(this T request, 
		                                  DALProxy proxy,
                                          int idSucursal, int idUsuario)
			where T:IHasIdCentro, new()
		{
					
            var usc = proxy.GetByIdUsuarioFromCache<UsuarioSucursalCentro>(idUsuario);

            var r = usc.FirstOrDefault(q=>q.IdSucursal==idSucursal && q.IdCentro==request.IdCentro);
			if(r ==default(UsuarioSucursalCentro))
				throw HttpError.Unauthorized(string.Format
				      ("Centro:'{0}'  no autorizado en la Sucursal:'{1}'",request.IdCentro, idSucursal));


		}
		
	}
}

