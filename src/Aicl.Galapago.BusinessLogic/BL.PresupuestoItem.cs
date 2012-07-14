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
    public static  partial class BL
    {

        public static void AssertExists(this PresupuestoItem request, int idPresupuesto, string codigo)
        {
            if( request== default(PresupuestoItem))
                throw new HttpError(
                    string.Format("No existe PresupuestoItem para IdPresupuesto:'{0}' y codigo'{2}'",
                              idPresupuesto, codigo));            
        }

        public static void AssertEstaActivo(this PresupuestoItem request)
        {
            if(!request.Activo)
                throw new HttpError(
                    string.Format("PresupuestoItem con Id:'{0}' y codigo'{2}' esta inactivo",
                              request.Id, request.Codigo));            

        }
    


        public static string GetLockKeyCodigo(this PresupuestoItem request, Presupuesto presupuesto)
        {
            return string.Format("urn:lock:PresupuestoItem:IdPresupuesto:{0}:Codigo:{1}",
                                 presupuesto.Id, request.Codigo);
        }


        public  static void UpdatePresupuesto(this PresupuestoItem presupuestoItem, DALProxy proxy,
                                              int idSucursal, int idCentro, string periodo,
                                              short tipoPartida, decimal valor, int? idTercero)
        {


            using(proxy.AcquireLock(MayorPresupuesto.GetLockKey(presupuestoItem.Id, idTercero),Definiciones.LockSeconds))
            {
                var pm= DAL.GetMayorPresupuesto(proxy, periodo,presupuestoItem.Id, idTercero);
    
                if(pm==default(MayorPresupuesto))
                {
                    pm= new MayorPresupuesto(){
                        IdPresupuestoItem=presupuestoItem.Id,
                        IdSucursal=idSucursal,
                        IdCentro=idCentro,
                        IdTercero=idTercero
                    };
                }
    
                if(pm.Id==default(int))
                {
                   pm.Insert(proxy,periodo,tipoPartida, valor);
                }
                else
                {
                    pm.Update(proxy,periodo,tipoPartida, valor);
                }
    
                presupuestoItem.UpdateDbCr(proxy, tipoPartida, valor);   
            }
        }
    }
}