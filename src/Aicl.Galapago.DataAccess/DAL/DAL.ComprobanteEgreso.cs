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
            proxy.Create(comprobante, visitor);
        }

       

        public static ComprobanteEgreso CreateComprobanteEgreso(DALProxy proxy,
                                            int idSucursal, int idCuentaGiradora, int idTercero, decimal valor,
                                            string descripcion,
                                            int? idTerceroReceptor=null, DateTime? fechaAsentado=null,
                                            bool? externo=false)
        {
			var today= DateTime.Today;
			var periodo= today.Year.ToString() + today.Month.ToString().PadLeft(2,'0');

            ComprobanteEgreso ce = new ComprobanteEgreso(){
                IdSucursal=idSucursal,
                IdCuentaGiradora= idCuentaGiradora,
                Fecha=today,
				Periodo= periodo,
                IdTercero=idTercero,
                Valor=valor,
                Descripcion=descripcion,
                IdTerceroReceptor= idTerceroReceptor.HasValue? idTerceroReceptor.Value: idTercero,
                FechaAsentado= fechaAsentado.HasValue?fechaAsentado.Value:fechaAsentado, //UTC ?
                Externo= externo.HasValue?externo.Value:false

            };   
            ce.Numero=proxy.GetNextConsecutivo(idSucursal, Definiciones.ComprobranteEgreso).Numero;
            proxy.Create(ce);
            return ce;
        }


        public static ComprobanteEgresoItem CreateItem(this ComprobanteEgreso comprobanteEgreso,
		                                               DALProxy proxy,
                                                       int idEgreso, decimal valor)
        {
            ComprobanteEgresoItem cei= new ComprobanteEgresoItem(){
                IdEgreso= idEgreso,
                Abono= valor,
                IdComprobanteEgreso= comprobanteEgreso.Id

            };
            proxy.Create(cei);
            return cei;
        }


        public static List<ComprobanteEgresoItem> GetItems(this ComprobanteEgreso documento,DALProxy proxy)
        {
            return proxy.Get<ComprobanteEgresoItem>(q=> q.IdComprobanteEgreso==documento.Id);
        }


        public static ComprobanteEgreso GetComprobanteEgreso(DALProxy proxy, int idComprobanteEgreso)
		{
            return proxy.FirstOrDefaultById<ComprobanteEgreso>(idComprobanteEgreso);
        }


        public static List<ComprobanteEgresoRetencion> GetRetenciones(this ComprobanteEgresoItem item, DALProxy proxy)
        {
            return proxy.Get<ComprobanteEgresoRetencion>(q=>q.IdComprobanteEgresoItem==item.Id);
        }

        public static void Actualizar(this ComprobanteEgreso documento,DALProxy proxy){

			var visitor = ReadExtensions.CreateExpression<ComprobanteEgreso>();
            visitor.Update( f=> new { f.Descripcion,f.Fecha,f.Periodo,f.IdTercero,f.IdTerceroReceptor, f.IdCuentaGiradora});                  
            visitor.Where(q=>q.Id==documento.Id);
			proxy.Update(documento,visitor);
        }


        public static void Anular(this ComprobanteEgreso comprobanteEgreso, DALProxy proxy, string descripcion)
        {
            comprobanteEgreso.FechaAnulado= DateTime.Today;
            comprobanteEgreso.Descripcion= descripcion.IsNullOrEmpty()?"Anulado":descripcion;
            comprobanteEgreso.Valor=0;

            proxy.Execute(dbCmd=>{
                proxy.Delete<ComprobanteEgresoItem>(r=>r.IdComprobanteEgreso==comprobanteEgreso.Id);

				var visitor = ReadExtensions.CreateExpression<ComprobanteEgreso>();
            	visitor.Update(r=> new {r.FechaAnulado, r.Descripcion,r.Valor});                  
            	visitor.Where(r=> r.Id==comprobanteEgreso.Id );

                proxy.Update(comprobanteEgreso,visitor);
            });

        }

        public static void AsignarConsecutivo(this ComprobanteEgreso comprobanteEgreso, DALProxy proxy)
        {
            comprobanteEgreso.Numero= proxy.GetNextConsecutivo(comprobanteEgreso.IdSucursal,
                                                  Definiciones.ComprobranteEgreso).Numero;
        }

        public static string GetLockKeyConsecutivo(this ComprobanteEgreso comprobanteEgreso)
        {
            return string.Format("urn:lock:Consecutivo:IdSucursal:{0}:Documento:{1}",
                                 comprobanteEgreso.IdSucursal, Definiciones.ComprobranteEgreso); 
        }


        public static void Asentar(this ComprobanteEgreso documento,DALProxy  proxy)
		{
            documento.FechaAsentado=DateTime.Today;
			var visitor = ReadExtensions.CreateExpression<ComprobanteEgreso>();
            visitor.Update(f=> f.FechaAsentado);                  
            visitor.Where(r=>r.Id==documento.Id);
            proxy.Update(documento, visitor);
        }

        public static void Reversar(this ComprobanteEgreso documento, DALProxy proxy)
		{
            documento.FechaAsentado=null;
			var visitor = ReadExtensions.CreateExpression<ComprobanteEgreso>();
            visitor.Update(f=> f.FechaAsentado);                  
            visitor.Where(r=>r.Id==documento.Id);
			proxy.Update(documento, visitor);
        }

        public static void ActualizarValor(this ComprobanteEgreso documento, DALProxy proxy)
        {
			var visitor = ReadExtensions.CreateExpression<ComprobanteEgreso>();
            visitor.Update(f=> f.Valor);                  
            visitor.Where(r=>r.Id==documento.Id);
            proxy.Update(documento, visitor);
        }


        
        public static void ActualizarValor(this ComprobanteEgresoItem documento, DALProxy proxy)
        {
			var visitor = ReadExtensions.CreateExpression<ComprobanteEgresoItem>();
            visitor.Update(f=> f.Abono);                  
            visitor.Where(r=>r.Id==documento.Id);
            proxy.Update(documento, visitor);
        }


        public static void Borrar(this ComprobanteEgresoItem item, DALProxy proxy)
        {
			proxy.Delete<ComprobanteEgresoItem>(q=>q.Id==item.Id);
            proxy.Delete<ComprobanteEgresoRetencion>(x=>
			                                         x.IdComprobanteEgresoItem==item.Id &&
                                                     x.IdComprobanteEgreso==item.IdComprobanteEgreso);

        }


    }
}

