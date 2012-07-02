using System;
using System.Data;
using System.Text;
using System.IO;
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
    public static class PresupuestoExtensiones
    {
        public static void AssertExistsActivo(this Presupuesto request, int idSucursal, int idCentro)
        {
            if( request== default(Presupuesto))
                throw new HttpError(
                    string.Format("No existe Presupuesto Activo para IdSucursal:'{0}' e IdCentro:'{1}'",
                              idSucursal, idCentro));            
        }

        /*public static string GetLockKeyCodigo(this Presupuesto request,  string codigo)
        {
            return string.Format("urn:lock:PresupuestoItem:IdPresupuesto:{0}:Codigo:{1}",
                                 request.Id, codigo);
        }*/

        public static string GetLockKey(this Presupuesto request, string codigo)
        {
            return string.Format("urn:lock:PresupuestoItem:IdPresupuesto:{0}:Codigo:{1}",
                                 request.Id, codigo);
        }

        public static string GetCacheKeyForActivo(int idSucursal, int idCentro){
            return string.Format("unr:Presupuesto:IdSucursal:{0}:IdCentro:{1}",idSucursal, idCentro);
        }




    }
}

