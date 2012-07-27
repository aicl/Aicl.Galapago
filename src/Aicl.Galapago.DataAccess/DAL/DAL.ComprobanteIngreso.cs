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

				var visitor = ReadExtensions.CreateExpression<ComprobanteIngreso>();
            	visitor.Update(r=> new {r.FechaAnulado, r.Descripcion,r.Valor});                  
            	visitor.Where(r=> r.Id==comprobanteIngreso.Id );

                proxy.Update(comprobanteIngreso,visitor);
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


		public static void Actualizar(this ComprobanteIngreso documento,DALProxy proxy){

			var visitor = ReadExtensions.CreateExpression<ComprobanteIngreso>();
            visitor.Update( f=> new { f.Descripcion,f.Fecha,f.Periodo,f.IdTercero,f.IdCuentaReceptora});                  
            visitor.Where(q=>q.Id==documento.Id);
			proxy.Update(documento,visitor);
        }

		public static void Asentar(this ComprobanteIngreso documento,DALProxy  proxy)
		{
            documento.FechaAsentado=DateTime.Today;
			var visitor = ReadExtensions.CreateExpression<ComprobanteIngreso>();
            visitor.Update(f=> f.FechaAsentado);                  
            visitor.Where(r=>r.Id==documento.Id);
            proxy.Update(documento, visitor);
        }

        public static void Reversar(this ComprobanteIngreso documento, DALProxy proxy)
		{
            documento.FechaAsentado=null;
			var visitor = ReadExtensions.CreateExpression<ComprobanteIngreso>();
            visitor.Update(f=> f.FechaAsentado);                  
            visitor.Where(r=>r.Id==documento.Id);
			proxy.Update(documento, visitor);
        }


	}
}

