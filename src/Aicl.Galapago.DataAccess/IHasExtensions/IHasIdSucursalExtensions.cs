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
	public static class IHasIdSucursalExtensions
	{
				
		public static Sucursal CheckSucursal<T>(this T request, DALProxy proxy, int idUsuario)
            where T:IHasIdSucursal, new()
        {

            var uscL = proxy.GetByIdUsuarioFromCache<UsuarioSucursalCentro>(idUsuario);

            var uscR = uscL.FirstOrDefault(r=>r.IdSucursal==request.IdSucursal);

            if( uscR==default(UsuarioSucursalCentro))
               throw HttpError.Unauthorized("Sucursal no autorizada");

            var sucursales= proxy.GetFromCache<Sucursal>();
            return sucursales.First(r=>r.Id==uscR.IdSucursal);

        }
        

		public static void CheckPeriodo<T>(this T request, DALProxy proxy)
			where T:IHasIdSucursal, IHasPeriodo, new()
		{
			
            PeriodoSucursal ps = proxy.GetPeriodoSucursal(request.Periodo,request.IdSucursal); 

			if (ps.Bloqueado)
				throw HttpError.Unauthorized(string.Format("Periodo {0} Cerrado para la Sucursal {1}",
				                                           request.Periodo, request.IdSucursal));
		}
		

        public static Tercero CheckTercero<T>(this T request, DALProxy proxy)
            where T:IHasIdTercero, new()
        {
           
            Tercero tercero = proxy.FirstOrDefaultByIdFromCache<Tercero>(request.IdTercero); 
            tercero.AssertExists(request.IdTercero);
            return tercero;

        }

       
	}
}

