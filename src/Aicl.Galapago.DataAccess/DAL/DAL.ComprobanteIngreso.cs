using System;
using Aicl.Galapago.Model.Types;
using ServiceStack.Common;
using ServiceStack.OrmLite;

namespace Aicl.Galapago.DataAccess
{
	public static partial class DAL
	{

		public static void Create(this ComprobanteIngreso comprobante, DALProxy proxy)
        {
            var visitor = ReadExtensions.CreateExpression<ComprobanteIngreso>();
            visitor.Insert( f=> new { f.Id, f.Descripcion, f.Fecha,
                f.Periodo, f.IdSucursal,f.Numero,f.IdTercero,
                f.IdCuentaReceptora });                  
            proxy.Create(comprobante, visitor);
        }

		public static ComprobanteIngreso CreateComprobanteIngreso(DALProxy proxy,
                                            int idSucursal, int idCuentaReceptora, int idTercero, 
		                                    decimal valor,
                                            string descripcion,
                                            DateTime? fechaAsentado=null,
                                            bool? externo=false)
        {
			var today= DateTime.Today;
			var periodo= today.Year.ToString() + today.Month.ToString().PadLeft(2,'0');

            ComprobanteIngreso ce = new ComprobanteIngreso(){
                IdSucursal=idSucursal,
                IdCuentaReceptora= idCuentaReceptora,
                Fecha=today,
				Periodo= periodo,
                IdTercero=idTercero,
                Valor=valor,
                Descripcion=descripcion,
                FechaAsentado= fechaAsentado.HasValue?fechaAsentado.Value:fechaAsentado, //UTC ?                Externo= externo.HasValue?externo.Value:false

            };   
            ce.Numero=proxy.GetNextConsecutivo(idSucursal, Definiciones.ComprobranteIngreso).Numero;
            proxy.Create(ce);
            return ce;
        }

		public static ComprobanteIngresoItem CreateItem(this ComprobanteIngreso comprobanteIngreso,
		                                               DALProxy proxy,
                                                       int idIngreso, decimal valor)
        {
            ComprobanteIngresoItem cei= new ComprobanteIngresoItem(){
                IdIngreso= idIngreso,
                Abono= valor,
                IdComprobanteIngreso= comprobanteIngreso.Id

            };
            proxy.Create(cei);
            return cei;
        }

		public static void Anular(this ComprobanteIngreso comprobanteIngreso, DALProxy proxy, string descripcion)
        {
            comprobanteIngreso.FechaAnulado= DateTime.Today;
            comprobanteIngreso.Descripcion= descripcion.IsNullOrEmpty()?"Anulado":descripcion;
            comprobanteIngreso.Valor=0;

            proxy.Execute(dbCmd=>{
                proxy.Delete<ComprobanteIngresoItem>(r=>r.IdComprobanteIngreso==comprobanteIngreso.Id);
				proxy.Update(comprobanteIngreso,
				     ev=> ev.Update(f=>new {f.FechaAnulado, f.Descripcion,f.Valor}).
				          Where(q=>q.Id==comprobanteIngreso.Id ));
            });
        }

		public static void AsignarConsecutivo(this ComprobanteIngreso comprobanteIngreso, DALProxy proxy)
        {
            comprobanteIngreso.Numero= proxy.GetNextConsecutivo(comprobanteIngreso.IdSucursal,
                                                  Definiciones.ComprobranteIngreso).Numero;
        }

		public static string GetLockKeyConsecutivo(this ComprobanteIngreso comprobanteIngreso)
        {
            return string.Format("urn:lock:Consecutivo:IdSucursal:{0}:Documento:{1}",
                                 comprobanteIngreso.IdSucursal, Definiciones.ComprobranteIngreso); 
        }

		public static void Actualizar(this ComprobanteIngreso documento,DALProxy proxy)
		{
			proxy.Update(documento,
			         ev=> ev.Update(f=>new{f.Descripcion,f.Fecha,f.Periodo,f.IdTercero,f.IdCuentaReceptora}).
			         Where(q=>q.Id==documento.Id));
        }

		public static void ActualizarValor(this ComprobanteIngreso documento, DALProxy proxy)
        {
            proxy.Update(documento, ev=> ev.Update(f=>f.Valor).Where(q=>q.Id==documento.Id));
        }


		public static void ActualizarValor(this ComprobanteIngresoItem documento, DALProxy proxy)
        {
            proxy.Update( documento, ev=> ev.Update(f=>f.Abono).Where(q=>q.Id==documento.Id));
        }

		public static void Asentar(this ComprobanteIngreso documento,DALProxy  proxy)
		{
            documento.FechaAsentado=DateTime.Today;
			proxy.Update(documento, ev=> ev.Update(f=>f.FechaAsentado).Where(q=>q.Id==documento.Id));
        }

        public static void Reversar(this ComprobanteIngreso documento, DALProxy proxy)
		{
            documento.FechaAsentado=null;
			proxy.Update(documento, ev=> ev.Update(f=>f.FechaAsentado).Where(q=>q.Id==documento.Id));
        }


		public static void Borrar(this ComprobanteIngresoItem item, DALProxy proxy)
        {
			proxy.Delete<ComprobanteIngresoItem>(q=>q.Id==item.Id);
            proxy.Delete<ComprobanteIngresoRetencion>(x=>
			                                         x.IdComprobanteIngresoItem==item.Id &&
                                                     x.IdComprobanteIngreso==item.IdComprobanteIngreso);

        }


	}
}

