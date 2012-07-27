using System;
using Aicl.Galapago.Model.Types;
using ServiceStack.OrmLite;

namespace Aicl.Galapago.DataAccess
{
	public static  partial class DAL
	{
		public static void Create(this Ingreso request, DALProxy proxy)
        {
            var visitor = ReadExtensions.CreateExpression<Ingreso>();
            visitor.Insert( f=> new { f.Id, f.Descripcion, f.Fecha , f.Periodo, f.IdSucursal,f.Numero,f.CodigoDocumento, f.IdTercero, f.DiasCredito });
			proxy.Create(request,visitor);
        }

		public static string GetLockKeyConsecutivo(this Ingreso request)
        {
            return string.Format("urn:lock:Consecutivo:IdSucursal:{0}:Documento:{1}",
                                 request.IdSucursal, Definiciones.Ingreso); 
        }

		public static void AsignarConsecutivo(this Ingreso request, DALProxy proxy)
        {
            request.Numero= proxy.GetNextConsecutivo(request.IdSucursal,Definiciones.Ingreso).Numero;
        }

		public static void Update(this Ingreso ingreso,DALProxy proxy){
            var visitor = ReadExtensions.CreateExpression<Ingreso>();
            visitor.Update( f=> new { f.Descripcion,f.Fecha,f.Periodo,f.IdTercero,f.CodigoDocumento,f.DiasCredito});                  
            visitor.Where(r=>r.Id==ingreso.Id);
            proxy.Execute(dbCmd=> dbCmd.UpdateOnly(ingreso, visitor));
        }

        public static void Asentar(this Ingreso ingreso,DALProxy  proxy){
            ingreso.FechaAsentado=DateTime.Today;
            var visitor = ReadExtensions.CreateExpression<Ingreso>();
            visitor.Update( f=> new {  f.FechaAsentado });                  
            visitor.Where(r=>r.Id==ingreso.Id);
            proxy.Update(ingreso, visitor);
        }

        public static void Reversar(this Ingreso ingreso, DALProxy proxy){
            ingreso.FechaAsentado=null;
            var visitor = ReadExtensions.CreateExpression<Ingreso>();
            visitor.Update( f=> new {  f.FechaAsentado });                  
            visitor.Where(r=>r.Id==ingreso.Id);
            proxy.Update(ingreso, visitor);
        }


        public static void Anular(this Ingreso ingreso,DALProxy proxy){
            ingreso.FechaAnulado=DateTime.Today;
            var visitor = ReadExtensions.CreateExpression<Ingreso>();
            visitor.Update( f=> new {  f.FechaAnulado });                  
            visitor.Where(r=>r.Id==ingreso.Id);
            proxy.Update(ingreso, visitor);
        }


        public static void ActualizarValorSaldo(this Ingreso ingreso,DALProxy  proxy){
            var visitor = ReadExtensions.CreateExpression<Ingreso>();
            visitor.Update( f=> new {  f.Valor, f.Saldo });                  
            visitor.Where(r=>r.Id==ingreso.Id);
            proxy.Update(ingreso, visitor);
        }


	}
}

