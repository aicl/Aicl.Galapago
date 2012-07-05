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
    public static  partial class DAL
    {
       

        public static void Create(this Egreso egreso, DALProxy proxy)
        {
            var visitor = ReadExtensions.CreateExpression<Egreso>();
            visitor.Insert( f=> new { f.Id, f.Descripcion, f.Fecha , f.Periodo, f.IdSucursal,f.Numero,f.CodigoDocumento,f.Documento, f.IdTercero, f.IdTerceroReceptor, f.DiasCredito });                  

            proxy.Execute(dbCmd=>
                dbCmd.InsertAndAssertId(egreso, visitor) 
            );

        }


        public static Egreso GetEgresoById( DALProxy proxy, int idEgreso, bool excludeJoin=true){
            return proxy.Execute(dbCmd=>{
                var visitor = ReadExtensions.CreateExpression<Egreso>();
                visitor.ExcludeJoin=excludeJoin;
                visitor.Where(q=>q.Id==idEgreso);
                return dbCmd.FirstOrDefault(visitor);
            });
        }



        public static void AsignarConsecutivo(this Egreso egreso, DALProxy proxy)
        {
            egreso.Numero= DAL.GetNextConsecutivo(proxy,egreso.IdSucursal,Definiciones.Egreso).Numero;
        }


        public static void Update(this Egreso egreso,DALProxy proxy){
            var visitor = ReadExtensions.CreateExpression<Egreso>();
            visitor.Update( f=> new { f.Descripcion,f.Fecha,f.Periodo,f.Documento,f.IdTercero,f.CodigoDocumento,f.IdTerceroReceptor, f.DiasCredito});                  
            visitor.Where(r=>r.Id==egreso.Id);
            proxy.Execute(dbCmd=> dbCmd.Update(egreso, visitor));
        }

        public static void Asentar(this Egreso egreso,DALProxy  proxy){
            egreso.FechaAsentado=DateTime.Today;
            var visitor = ReadExtensions.CreateExpression<Egreso>();
            visitor.Update( f=> new {  f.FechaAsentado });                  
            visitor.Where(r=>r.Id==egreso.Id);
            proxy.Execute(dbCmd=> dbCmd.Update(egreso, visitor));
        }

        public static void Reversar(this Egreso egreso, DALProxy proxy){
            egreso.FechaAsentado=null;
            var visitor = ReadExtensions.CreateExpression<Egreso>();
            visitor.Update( f=> new {  f.FechaAsentado });                  
            visitor.Where(r=>r.Id==egreso.Id);
            proxy.Execute(dbCmd=>dbCmd.Update(egreso, visitor));
        }


        public static void Anular(this Egreso egreso,DALProxy proxy){
            egreso.FechaAnulado=DateTime.Today;
            var visitor = ReadExtensions.CreateExpression<Egreso>();
            visitor.Update( f=> new {  f.FechaAnulado });                  
            visitor.Where(r=>r.Id==egreso.Id);
            proxy.Execute(dbCmd=>  dbCmd.Update(egreso, visitor));
        }


        public static void ActualizarValorSaldo(this Egreso egreso,DALProxy  proxy){
            var visitor = ReadExtensions.CreateExpression<Egreso>();
            visitor.Update( f=> new {  f.Valor, f.Saldo });                  
            visitor.Where(r=>r.Id==egreso.Id);
            proxy.Execute(dbCmd=> dbCmd.Update(egreso, visitor));
        }



        public static void Create(this EgresoItem item, DALProxy proxy)
        {
            proxy.Execute(dbCmd=>dbCmd.InsertAndAssertId(item));

        }

        public static void Actualizar(this EgresoItem item, DALProxy proxy)
        {
            proxy.Execute(dbCmd=> dbCmd.Update(item));
        }

        public static void Borrar(this EgresoItem item, DALProxy proxy)
        {
            proxy.Execute(dbCmd=> dbCmd.Delete(item));
        }


        public static string GetLockKeyConsecutivo(this Egreso egreso)
        {
            return string.Format("urn:lock:Consecutivo:IdSucursal:{0}:Documento:{1}",
                                 egreso.IdSucursal, Definiciones.Egreso); 
        }


        public static List<EgresoItem> GetItems(this Egreso egreso,DALProxy proxy)
        {
            return proxy.Execute(dbCmd=>{return dbCmd.Select<EgresoItem>(q=> q.IdEgreso==egreso.Id);});

        }


        public static List<ComprobanteEgresoItem> GetComprobanteEgresoItems(this Egreso egreso, DALProxy proxy)
        {
            return proxy.Execute(dbCmd=>{
                return dbCmd.Select<ComprobanteEgresoItem>(q=> q.IdEgreso==egreso.Id);
            });
        }


    }
}

