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
				
		public static void CheckSucursal<T>(this T request, DALProxy proxy, int idUsuario)
            where T:IHasIdSucursal, new()
        {

            var usc = DAL.GetByIdUsuarioFromCache<UsuarioSucursalCentro>(proxy,idUsuario);

            if(usc.FirstOrDefault(r=>r.IdSucursal==request.IdSucursal)==default(UsuarioSucursalCentro))
               throw HttpError.Unauthorized("Sucursal no autorizada");

        }
        

		public static void CheckPeriodo<T>(this T request, DALProxy proxy)
			where T:IHasIdSucursal, IHasPeriodo, new()
		{
			
            PeriodoSucursal ps = DAL.GetPeriodoSucursal(proxy, request.Periodo,request.IdSucursal); 

			if (ps.Bloqueado)
				throw HttpError.Unauthorized(string.Format("Periodo {0} Cerrado para la Sucursal {1}",
				                                           request.Periodo, request.IdSucursal));
		}
		

        public static void CheckTercero<T>(this T request, DALProxy proxy)
            where T:IHasIdTercero, new()
        {
           
            Tercero tercero = DAL.FirstOrDefaultByIdFromCache<Tercero>(proxy, request.IdTercero); 
            tercero.AssertExists(request.IdTercero);

        }

       
	}
}

