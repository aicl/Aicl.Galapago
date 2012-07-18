using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceInterface;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.DesignPatterns.Model;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
    public static partial class DAL
    {

        #region presupuestoItem
        public static PresupuestoItem GetPresupuestoItem(DALProxy proxy, int idPresupuestoItem)
        {
			return proxy.FirstOrDefault<PresupuestoItem>(r=>r.Id==idPresupuestoItem );
        }

        public static PresupuestoItem GetPresupuestoItem(this Presupuesto presupuesto,DALProxy proxy,string codigo)
        {
			return proxy.FirstOrDefault<PresupuestoItem>(r=>r.IdPresupuesto==presupuesto.Id && r.Codigo==codigo);
        }

       
        #endregion presupuestoItem

        public static Presupuesto GetPresupuestoActivo(DALProxy proxy, int idSucursal, int idCentro){
			return proxy.FirstOrDefault<Presupuesto>(r=>r.IdSucursal==idSucursal && r.IdCentro==idCentro && r.Activo);
            
        }

        public static Presupuesto GetPresupuestoById( DALProxy proxy, int idPresupuesto){
            return proxy.FirstOrDefaultById<Presupuesto>(idPresupuesto);
        }


        public static void UpdateDbCr(this PresupuestoItem presupuestoItem, DALProxy proxy, short tipoPartida, decimal valor )
        {
            presupuestoItem.Update(tipoPartida==1?valor:0, tipoPartida==2? valor:0);
            SqlExpressionVisitor<PresupuestoItem> expression= ReadExtensions.CreateExpression<PresupuestoItem>();
            expression.Where(r=> r.Id== presupuestoItem.Id);
            expression.Update(r=> new{r.Debitos, r.Creditos});
            proxy.Update(presupuestoItem, expression);
        }

        public static void CheckUsuarioGiradora(this PresupuestoItem presupuestoItem, DALProxy proxy, 
                                                int idUsuario,
                                                int? idTercero)
        {
            if(presupuestoItem.Codigo.StartsWith(Definiciones.GrupoCajaBancos))
            {
                var usuarioGiradora = DAL.GetUsuarioGiradora(proxy, idUsuario, presupuestoItem.Id, idTercero);

                if(usuarioGiradora==default(UsuarioGiradora))
                throw new HttpError(
                    string.Format("No existe Cuenta Giradora autorizada para IdUsuario:'{0}'  Codigo:'{1}' idTercero:{2}",
                              idUsuario, presupuestoItem.Codigo, idTercero.HasValue? idTercero.Value: 0));            

            }
        }

    }
}

