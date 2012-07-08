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
        public static void Create(this ComprobanteEgreso comprobante, DALProxy proxy)
        {
            var visitor = ReadExtensions.CreateExpression<ComprobanteEgreso>();
            visitor.Insert( f=> new { f.Id, f.Descripcion, f.Fecha,
                f.Periodo, f.IdSucursal,f.Numero,f.IdTercero,f.IdTerceroReceptor,
                f.IdCuentaGiradora });                  

            proxy.Execute(dbCmd=>
                dbCmd.InsertAndAssertId(comprobante, visitor) 
            );

        }

       

        public static ComprobanteEgreso CreateComprobanteEgreso(DALProxy proxy,
                                            int idSucursal, int idCuentaGiradora, int idTercero, decimal valor,
                                            string descripcion,
                                            int? idTerceroReceptor=null, DateTime? fechaAsentado=null,
                                            bool? externo=false)
        {
            ComprobanteEgreso ce = new ComprobanteEgreso(){
                IdSucursal=idSucursal,
                IdCuentaGiradora= idCuentaGiradora,
                Fecha=DateTime.Today,
                IdTercero=idTercero,
                Valor=valor,
                Descripcion=descripcion,
                IdTerceroReceptor= idTerceroReceptor.HasValue? idTerceroReceptor.Value: idTercero,
                FechaAsentado= fechaAsentado.HasValue?fechaAsentado.Value:fechaAsentado, //UTC ?
                Externo= externo.HasValue?externo.Value:false

            };
            proxy.Execute(dbCmd=>{
                ce.Numero=DAL.GetNextConsecutivo(proxy,idSucursal, Definiciones.ComprobranteEgreso).Numero;
                dbCmd.InsertAndAssertId(ce);
            });
            return ce;
        }


        public static ComprobanteEgresoItem CreateItem(this ComprobanteEgreso comprobanteEgreso,
                                                         DALProxy proxy,
                                                         int idEgreso, decimal valor)
        {
           
            ComprobanteEgresoItem cei= new ComprobanteEgresoItem(){
                IdEgreso= idEgreso,
                Valor= valor,
                IdComprobanteEgreso= comprobanteEgreso.Id

            };
            proxy.Execute(dbCmd=>dbCmd.Insert(cei));
            return cei;
        }


        public static List<ComprobanteEgresoItem> GetItems(this ComprobanteEgreso documento,DALProxy proxy)
        {
            return proxy.Execute(dbCmd=>{return dbCmd.Select<ComprobanteEgresoItem>(q=> q.IdComprobanteEgreso==documento.Id);});
        }



        public static ComprobanteEgreso GetComprobanteEgreso(DALProxy proxy, int idComprobanteEgreso){
            return proxy.Execute(dbCmd=>{
                return dbCmd.GetByIdOrDefault<ComprobanteEgreso>(idComprobanteEgreso);
            });
        }


        public static List<ComprobanteEgresoRetencion> GetRetenciones(this ComprobanteEgresoItem item, DALProxy proxy)
        {
            return proxy.Execute(DbCmd=>{
                return DbCmd.Select<ComprobanteEgresoRetencion>(q=>q.IdComprobanteEgresoItem==item.Id);});
        
        }

        public static void Actualizar(this ComprobanteEgreso documento,DALProxy proxy){

            proxy.Execute(dbCmd=> dbCmd.UpdateOnly(documento,
                f=> new { f.Descripcion,f.Fecha,f.Periodo,f.IdTercero,f.IdTerceroReceptor, f.IdCuentaGiradora},
                q=>q.Id==documento.Id)
            );
        }


        public static void Anular(this ComprobanteEgreso comprobanteEgreso, DALProxy proxy, string descripcion)
        {
            comprobanteEgreso.FechaAnulado= DateTime.Today;
            comprobanteEgreso.Descripcion= descripcion.IsNullOrEmpty()?"Anulado":descripcion;
            comprobanteEgreso.Valor=0;

            proxy.Execute(dbCmd=>{
                dbCmd.Delete<ComprobanteEgresoItem>(r=>r.IdComprobanteEgreso==comprobanteEgreso.Id);
                dbCmd.UpdateOnly(comprobanteEgreso,
                             r=>new {r.FechaAnulado, r.Descripcion,r.Valor},
                             r=> r.Id==comprobanteEgreso.Id );
            });

        }

        public static void AsignarConsecutivo(this ComprobanteEgreso comprobanteEgreso, DALProxy proxy)
        {
            comprobanteEgreso.Numero= DAL.GetNextConsecutivo(proxy,
                                                  comprobanteEgreso.IdSucursal,
                                                  Definiciones.ComprobranteEgreso).Numero;
        }

        public static string GetLockKeyConsecutivo(this ComprobanteEgreso comprobanteEgreso)
        {
            return string.Format("urn:lock:Consecutivo:IdSucursal:{0}:Documento:{1}",
                                 comprobanteEgreso.IdSucursal, Definiciones.ComprobranteEgreso); 
        }


        public static void Asentar(this ComprobanteEgreso documento,DALProxy  proxy){
            documento.FechaAsentado=DateTime.Today;
            proxy.Execute(dbCmd=> dbCmd.UpdateOnly(documento, f=> f.FechaAsentado, r=>r.Id==documento.Id));
        }

        public static void Reversar(this ComprobanteEgreso documento, DALProxy proxy){
            documento.FechaAsentado=null;
            proxy.Execute(dbCmd=> dbCmd.UpdateOnly(documento, f=> f.FechaAsentado, r=>r.Id==documento.Id));
        }

        public static void ActualizarValor(this ComprobanteEgreso documento, DALProxy proxy)
        {
            proxy.Execute(dbCmd=> dbCmd.UpdateOnly(documento, f=> f.Valor, r=>r.Id==documento.Id));
        }


        public static void Create(this ComprobanteEgresoItem documento, DALProxy proxy)
        {
            proxy.Execute(dbCmd=> dbCmd.InsertAndAssertId(documento));
        }

        public static void ActualizarValor(this ComprobanteEgresoItem documento, DALProxy proxy)
        {
            proxy.Execute(dbCmd=> dbCmd.UpdateOnly(documento, f=> f.Valor, r=>r.Id==documento.Id));
        }


        public static void Borrar(this ComprobanteEgresoItem item, DALProxy proxy)
        {
            proxy.Execute(dbCmd=>{
                dbCmd.Delete(item);
                dbCmd.Delete<ComprobanteEgresoRetencion>(x=>
                                                         x.IdComprobanteEgresoItem==item.Id &&
                                                         x.IdComprobanteEgreso==item.IdComprobanteEgreso);
            });

        }


        public static void Create(this ComprobanteEgresoRetencion documento, DALProxy proxy)
        {
            proxy.Execute(dbCmd=> dbCmd.InsertAndAssertId(documento));
        }


        public static void Borrar(this ComprobanteEgresoRetencion documento, DALProxy proxy)
        {
            proxy.Execute(dbCmd=> dbCmd.Delete(documento));
        }

    }
}

