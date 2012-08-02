using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using Mono.Linq.Expressions;

namespace Aicl.Galapago.BusinessLogic
{
	public static  partial class BL
	{

        #region Get
        public static Response<Ingreso> Get(this Ingreso request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
            return factory.Execute(proxy=>{

				long? totalCount=null;

				var paginador= new Paginador(httpRequest);
            	var queryString= httpRequest.QueryString;

                var predicate=PredicateBuilder.True<Ingreso>();

                var periodo= queryString["Periodo"];
				if(! periodo.IsNullOrEmpty()) //periodo= DateTime.Today.ObtenerPeriodo();
				{
	                if (periodo.Length==6)
	                    predicate= q=>q.Periodo==periodo;
	                else
	                    predicate= q=>q.Periodo.StartsWith(periodo) ;
				}

				var p =queryString["IdSucursal"];
				if(!p.IsNullOrEmpty())
				{
					int idSucursal;
					if(int.TryParse(p,out idSucursal) && idSucursal!=default(int))
						predicate= predicate.AndAlso(q=>q.IdSucursal==idSucursal);
				}

				p=queryString["IdTercero"];
				if(!p.IsNullOrEmpty())
				{
					int idTercero;
					if(int.TryParse(p,out idTercero) && idTercero!=default(int))
						predicate= predicate.AndAlso(q=>q.IdTercero==idTercero);
				}

                var nombre= queryString["NombreTercero"];
                if(!nombre.IsNullOrEmpty())
                    predicate= predicate.AndAlso(q=>q.NombreTercero.Contains(nombre));

				var sucursal= queryString["NombreSucursal"];
                if(!sucursal.IsNullOrEmpty())
                    predicate= predicate.AndAlso(q=>q.NombreSucursal.Contains(sucursal));

				p= queryString["Asentado"];
            	if(!p.IsNullOrEmpty())
           		{
                	bool asentado;
	                if( bool.TryParse(p,out asentado))
	                {
						if(asentado)
							predicate= predicate.AndAlso(q=>q.FechaAsentado!=null);
						else
							predicate= predicate.AndAlso(q=>q.FechaAsentado==null);
	                }
	            }

				p= queryString["ConSaldo"];
            	if(!p.IsNullOrEmpty())
           		{
                	bool saldo;
	                if( bool.TryParse(p,out saldo))
	                {
						if (saldo) predicate= predicate.AndAlso(q=>q.Saldo!=0);
						else predicate= predicate.AndAlso(q=>q.Saldo==0);
	                }
	            }

                var visitor = ReadExtensions.CreateExpression<Ingreso>();
				visitor.Where(predicate);
                if(paginador.PageNumber.HasValue)
                {
					visitor.Select(r=> Sql.Count(r.Id));
					totalCount= proxy.Count(visitor);
					visitor.Select();
                    int rows= paginador.PageSize.HasValue? paginador.PageSize.Value:BL.PageSize;
                    visitor.Limit(paginador.PageNumber.Value*rows, rows);
                }
                                
                visitor.OrderByDescending(r=>r.Numero);
                
				return new Response<Ingreso>(){
                	Data=proxy.Get(visitor),
                	TotalCount=totalCount
            	};
            });  
        }
        #endregion Get

		#region Post		
		public static Response<Ingreso> Post(this Ingreso request,
                                            Factory factory,
                                            IAuthSession authSession)
		{
            request.ValidateAndThrowHttpError(Operaciones.Create);
            var idUsuario = int.Parse(authSession.UserAuthId);
            request.Periodo= request.Fecha.ObtenerPeriodo();
         
            factory.Execute(proxy=>{

                var sucursal=request.CheckSucursal(proxy,idUsuario);

                var tercero=request.CheckTercero(proxy);
                
                request.CheckCodigoDocumento(proxy);
                request.CheckPeriodo(proxy);

                request.NombreSucursal=sucursal.Nombre;

                request.NombreTercero=tercero.Nombre;
                request.DocumentoTercero= tercero.Documento;
                request.NombreDocumentoTercero= tercero.NombreDocumento;
                request.DVTercero= tercero.DigitoVerificacion;

                using (proxy.AcquireLock(request.GetLockKeyConsecutivo(), Definiciones.LockSeconds))
                {
                    proxy.BeginDbTransaction();
                    request.AsignarConsecutivo(proxy);
					request.AsignarDocumento(proxy);
                    request.Create(proxy);
                    proxy.CommitDbTransaction();
                }
            });
		
			List<Ingreso> data = new List<Ingreso>();
			data.Add(request);
			
			return new Response<Ingreso>(){
				Data=data
			};	
			
		}
		#endregion Post

        #region Put
        public static Response<Ingreso> Put(this Ingreso request,
                                              Factory factory,
                                              IAuthSession authSession)                                
        {

            request.ValidateAndThrowHttpError(Definiciones.CheckRequestBeforeUpdate);
            var idUsuario = int.Parse(authSession.UserAuthId);

            factory.Execute(proxy=>{
                using(proxy.AcquireLock(request.GetLockKey(), Definiciones.LockSeconds))
                {
                    Ingreso oldData=proxy.FirstOrDefaultById<Ingreso>( request.Id);
                    oldData.AssertExists(request.Id);
                    CheckOldAndNew(oldData, request, proxy, idUsuario );
                    request.Update(proxy);
                }
            });

            List<Ingreso> data = new List<Ingreso>();
            data.Add(request);
            
            return new Response<Ingreso>(){
                Data=data
            };  
            
        }
        #endregion Put

        #region Patch
        public static Response<Ingreso> Patch(this Ingreso request,
                                             Factory factory,
                                             IAuthSession authSession,
                                             string action)
        {
            int factor;
            string operacion;
            string rule;

            if(action=="asentar")
            {
                rule=Definiciones.CheckRequestBeforeAsentar;
                operacion= Operaciones.Asentar;
                factor=1;
            }
            else if(action=="reversar")
            {
                rule=Definiciones.CheckRequestBeforeReversar;
                operacion= Operaciones.Reversar;
                factor=-1;
            }
            else if(action=="anular")
            {
                 rule= Definiciones.CheckRequestBeforeAnular;
                 operacion= Operaciones.Anular;
                 factor=0;
            }
            else
                throw new HttpError(string.Format("Operacion:'{0}' NO implementada para Ingreso",
                                                      action ));            
             
            var idUsuario = int.Parse(authSession.UserAuthId);

            factory.Execute(proxy=>{
                using(proxy.AcquireLock(request.GetLockKey(), Definiciones.LockSeconds))
                {
                    var oldData= proxy.FirstOrDefaultById<Ingreso>(request.Id);
                    oldData.AssertExists(request.Id);
					oldData.ValidateAndThrowHttpError(rule);
                    CheckBeforePatch(oldData, request, proxy, idUsuario, operacion);

                    if(factor==0)
                    {
                        proxy.BeginDbTransaction();
                        proxy.ExecuteBeforePatch(request, oldData, operacion);
                        request.Anular(proxy);
                        proxy.ExecuteAfterPatch(request, oldData, operacion);
                        proxy.CommitDbTransaction();
                        return;
                    }

                    List<IngresoItem> items =  proxy.Get<IngresoItem>(q=> q.IdIngreso==request.Id);
                    decimal saldo=0; // si queremos reversar debe coincidir con request.Saldo

                    proxy.BeginDbTransaction();
                    #region ActualizarCuentaPorPagar
                    if(request.Saldo>0)
                    {
                        saldo = 
                            (from r in items 
                             select r.Valor*(r.TipoPartida==1?-1:1)).Sum();

                        var  prs= DAL.GetPresupuestoActivo(proxy,request.IdSucursal,Definiciones.IdCentroGeneral);
                        prs.AssertExistsActivo(request.IdSucursal, Definiciones.IdCentroGeneral);
                        CodigoDocumento cd = proxy.GetCodigoDocumento(request.CodigoDocumento);
                        cd.AssertExists(request.CodigoDocumento);
                        cd.AssertEstaActivo();
                        using(proxy.AcquireLock(prs.GetLockKey(cd.CodigoPresupuesto), Definiciones.LockSeconds))
                        {
                            var pi= prs.GetPresupuestoItem(proxy,cd.CodigoPresupuesto);
                            pi.AssertExists(prs.Id,cd.CodigoPresupuesto );
                            pi.UpdatePresupuesto(proxy,request.IdSucursal,Definiciones.IdCentroGeneral,
                                                 request.Periodo,
                                                 (saldo>0?(short)1:(short)2),
                                                 Math.Abs(saldo)*factor,request.IdTercero);
                        }

                    }
                    #endregion ActualizarCuentaPorPagar

                    List<IngresoItem> itemsCajaBancos= new List<IngresoItem>();
                    #region ActualizarSaldoItems
                    foreach(var item in items)
                    {
                        var  prs= DAL.GetPresupuestoActivo(proxy,request.IdSucursal, item.IdCentro);
                        prs.AssertExistsActivo(request.IdSucursal, item.IdCentro);

                        using(proxy.AcquireLock(item.IdPresupuestoItem.GetLockKey<PresupuestoItem>(), Definiciones.LockSeconds))
                        {
                            var pi= DAL.GetPresupuestoItem(proxy,item.IdPresupuestoItem);
                            pi.AssertExists(item.IdPresupuestoItem);
                            pi.UpdatePresupuesto(proxy, request.IdSucursal,item.IdCentro,request.Periodo,item.TipoPartida,item.Valor*factor, null);
                            if (pi.Codigo.StartsWith(Definiciones.GrupoCajaBancos)) itemsCajaBancos.Add(item);
                        }
                    }
                    #endregion ActualizarSaldoItems
                    #region CrearComprobanteIngresos
                    // solo es crear el comprobante y un detalle, los saldos ya estan actualizados...
                    // el centro de costo no interesa...

                    if(factor==1) // asentar --Generar el comprobante de egreso por cuenta y sus items
                    {
                        var valorPorCuentaGiradora=
                            (from r in itemsCajaBancos  group r by r.IdPresupuestoItem into g
                             select new { IdPresupuestoItem=g.Key, Valor=g.Sum(p => p.Valor*(p.TipoPartida==1?1:-1))*factor}).
                                ToList(); 
                        foreach(var cg in valorPorCuentaGiradora)
                        {
                            string descripcion=string.Format("Cancelacion {0}:{1} ${2}",request.CodigoDocumento,
                                                             request.Numero, cg.Valor); 
                            ComprobanteIngreso ce= DAL.CreateComprobanteIngreso(proxy,request.IdSucursal,
                                                                              cg.IdPresupuestoItem,
                                                                              request.IdTercero,
                                                                              cg.Valor,
                                                                              descripcion,
                                                                              DateTime.Today,true);
                            ce.CreateItem(proxy,request.Id,cg.Valor);
                        }
                        request.Asentar(proxy);
                    }
                    else if(factor==-1)  // Reversar= anular todos los comprobantes de este egreso!!
                    {
                        if( itemsCajaBancos.Count!=0)
                        {
                            CheckSaldo(request,saldo);
                            var ei= proxy.Get<ComprobanteIngresoItem>(q=> q.IdIngreso==request.Id); 
                            foreach(var cei in ei){
                                using(proxy.AcquireLock(cei.IdComprobanteIngreso.GetLockKey<ComprobanteIngreso>(),Definiciones.LockSeconds))
                                {
                                    ComprobanteIngreso ce =proxy.FirstOrDefaultById<ComprobanteIngreso>(cei.IdComprobanteIngreso);
                                    ce.Anular(proxy,string.Format("Anulado. Ingreso:'{0}' reversado",request.Numero));
                                }
                            }
                        }
                        proxy.ExecuteBeforePatch(request, oldData, operacion);
                        request.Reversar(proxy);
                        proxy.ExecuteAfterPatch(request, oldData, operacion);
                    }
                    #endregion CrearComprobanteIngresos

                    proxy.CommitDbTransaction();

                }
            });

                    
            List<Ingreso> data = new List<Ingreso>();
            data.Add(request);
            
            return new Response<Ingreso>(){
                Data=data
            };  
            
        }
        #endregion Patch


		public static void ValidateAndThrowHttpError(this Ingreso request, string ruleSet)
		{
			IngresoValidator av = new IngresoValidator();
			av.ValidateAndThrowHttpError(request, ruleSet);
		}

        static void CheckOldAndNew(Ingreso oldData, Ingreso request,
                                           DALProxy proxy,
                                           int idUsuario)
        {
            oldData.ValidateAndThrowHttpError(Operaciones.Update);

            Ingresos egresos= new Ingresos(){Nuevo=request, Viejo=oldData};
            IngresosValidator ev = new IngresosValidator();
            ev.ValidateAndThrowHttpError(egresos,Operaciones.Update);

            oldData.CheckSucursal(proxy, idUsuario);

            var data = new Ingreso();
            data.PopulateWith(oldData);

            if( request.Fecha!=default(DateTime) && request.Fecha!=data.Fecha)
            {
                data.Fecha=request.Fecha;
                data.Periodo= data.Fecha.ObtenerPeriodo();
            }

            data.CheckPeriodo(proxy);

            if(request.IdTercero!=default(int) && request.IdTercero!=data.IdTercero)
            {
                data.IdTercero=request.IdTercero;
                var tercero=data.CheckTercero(proxy);
                data.NombreDocumentoTercero=tercero.NombreDocumento;
                data.NombreTercero=tercero.Nombre;
                data.DocumentoTercero=tercero.Documento;
                data.DVTercero= tercero.DigitoVerificacion;
            }
			            
            if(!request.Descripcion.IsNullOrEmpty() && request.Descripcion!=data.Descripcion)
                data.Descripcion=request.Descripcion;

            if(request.DiasCredito!=data.DiasCredito) data.DiasCredito=request.DiasCredito;

            request.PopulateWith(data);
        }


        static void CheckBeforePatch(Ingreso oldData, Ingreso request,
                                             DALProxy proxy,
                                             int idUsuario,
                                             string operacion)
        {
            oldData.ValidateAndThrowHttpError(operacion);
            Ingresos egresos= new Ingresos(){Nuevo=request, Viejo=oldData};
            IngresosValidator ev = new IngresosValidator();
            ev.ValidateAndThrowHttpError(egresos,operacion);

            oldData.CheckSucursal(proxy,idUsuario);
            oldData.CheckPeriodo(proxy);

            request.PopulateWith(oldData);
        }
  
        static void CheckSaldo(Ingreso request, decimal saldo)
        {
            if(saldo!=request.Saldo)
                throw new HttpError(string.Format("El Ingreso:'{0}' NO puede ser Reversado. Revise los comprobantes de Ingreso",
                                                      request.Numero ));            
        }

		       
	}
}
