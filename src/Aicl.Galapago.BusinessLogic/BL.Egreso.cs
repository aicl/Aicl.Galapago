using System;
using System.Data;
using System.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
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
        public static Response<Egreso> Get(this Egreso request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
            return factory.Execute(proxy=>{

				long? totalCount=null;

				var paginador= new Paginador(httpRequest);
            	var queryString= httpRequest.QueryString;

                Expression<Func<Egreso, bool>> predicate;

                var periodo= queryString["Periodo"];
				if(periodo.IsNullOrEmpty()) periodo= DateTime.Today.ObtenerPeriodo();
                if (periodo.Length==6)
                    predicate= q=>q.Periodo==periodo;
                else
                    predicate= q=>q.Periodo.StartsWith(periodo) ;


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

                var visitor = ReadExtensions.CreateExpression<Egreso>();
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
                
				return new Response<Egreso>(){
                	Data=proxy.Get(visitor),
                	TotalCount=totalCount
            	};
            });
  
        }
        #endregion Get

		#region Post		
		public static Response<Egreso> Post(this Egreso request,
                                            Factory factory,
                                            IAuthSession authSession)
		{
            request.ValidateAndThrowHttpError(Operaciones.Create);
            var idUsuario = int.Parse(authSession.UserAuthId);
            request.Periodo= request.Fecha.ObtenerPeriodo();
            if(request.IdTerceroReceptor.HasValue && request.IdTerceroReceptor.Value==default(int))
                request.IdTerceroReceptor=null;

            factory.Execute(proxy=>{

                var sucursal=request.CheckSucursal(proxy,idUsuario);

                var tercero=request.CheckTercero(proxy);
                Tercero tr= default(Tercero);
                if(request.IdTerceroReceptor.HasValue)
                {

                    if(request.IdTerceroReceptor!=request.IdTercero)
                        tr= request.CheckTerceroReceptor(proxy);
                    else
                        tr= tercero;
                }


                request.CheckCodigoDocumento(proxy);
                request.CheckPeriodo(proxy);

                request.NombreSucursal=sucursal.Nombre;

                request.NombreTercero=tercero.Nombre;
                request.DocumentoTercero= tercero.Documento;
                request.NombreDocumentoTercero= tercero.NombreDocumento;
                request.DVTercero= tercero.DigitoVerificacion;

                if(tr!=default(Tercero))
                {
                    request.DocumentoReceptor= tr.Documento;
                    request.NombreDocumentoReceptor= tr.NombreDocumento;
                    request.NombreReceptor=tr.Nombre;
                    request.DVReceptor= tr.DigitoVerificacion;
                }

                using (proxy.AcquireLock(request.GetLockKeyConsecutivo(), Definiciones.LockSeconds))
                {
                    // TODO : Revisar documento si es vacio y CC traer numero de Tercero
                    proxy.BeginDbTransaction();
                    request.AsignarConsecutivo(proxy);
                    request.Create(proxy);
                    proxy.CommitDbTransaction();
                }
            });
		
			List<Egreso> data = new List<Egreso>();
			data.Add(request);
			
			return new Response<Egreso>(){
				Data=data
			};	
			
		}
		#endregion Post
		
        #region Put
        public static Response<Egreso> Put(this Egreso request,
                                              Factory factory,
                                              IAuthSession authSession)                                
        {

            // TODO :  si cambio cambiar el Tercero y si el  Nro. Dto viene vacio traer Numero del nuevo Tercero...

            request.ValidateAndThrowHttpError(Definiciones.CheckRequestBeforeUpdate);
            var idUsuario = int.Parse(authSession.UserAuthId);

            factory.Execute(proxy=>{
                using(proxy.AcquireLock(request.GetLockKey(), Definiciones.LockSeconds))
                {
                    Egreso oldData= DAL.GetEgresoById(proxy, request.Id);
                    oldData.AssertExists(request.Id);
                    CheckOldAndNew(oldData, request, proxy, idUsuario );
                    request.Update(proxy);

                    //Action<DALProxy> action=(p)=>request.Update(p);
                    //Action<DALProxy> action= request.Update;
                    //proxy.ExecuteUpdate(action,request,oldData); // me permite ejecutar con  o sin triggers
                }
            });

            List<Egreso> data = new List<Egreso>();
            data.Add(request);
            
            return new Response<Egreso>(){
                Data=data
            };  
            
        }
        #endregion Put

        #region Patch
        public static Response<Egreso> Patch(this Egreso request,
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
                throw new HttpError(string.Format("Operacion:'{0}' NO implementada para Egreso",
                                                      action ));            
             
            request.ValidateAndThrowHttpError(rule);
            var idUsuario = int.Parse(authSession.UserAuthId);

            factory.Execute(proxy=>{
                using(proxy.AcquireLock(request.GetLockKey(), Definiciones.LockSeconds))
                {
                    Egreso oldData= DAL.GetEgresoById(proxy, request.Id);
                    oldData.AssertExists(request.Id);
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

                    List<EgresoItem> items = request.GetItems(proxy);
                    decimal saldo=0; // si queremos reversar debe coincidir con request.Saldo

                    proxy.BeginDbTransaction();
                    #region ActualizarCuentaPorPagar
                    if(request.Saldo>0)
                    {
                        saldo = 
                            (from r in items 
                             select r.Valor*(r.TipoPartida==1?1:-1)).Sum();

                        var  prs= DAL.GetPresupuestoActivo(proxy,request.IdSucursal,Definiciones.IdCentroGeneral);
                        prs.AssertExistsActivo(request.IdSucursal, Definiciones.IdCentroGeneral);
                        //urn:PresupuestoItem:IdPresupuesto:{0}:Codigo:{1}"
                        CodigoDocumento cd = proxy.GetCodigoDocumento(request.CodigoDocumento);
                        cd.AssertExists(request.CodigoDocumento);
                        cd.AssertEstaActivo();
                        using(proxy.AcquireLock(prs.GetLockKey(cd.CodigoPresupuesto), Definiciones.LockSeconds))
                        {
                            var pi= prs.GetPresupuestoItem(proxy,cd.CodigoPresupuesto);
                            pi.AssertExists(prs.Id,cd.CodigoPresupuesto );
                            pi.UpdatePresupuesto(proxy,request.IdSucursal,Definiciones.IdCentroGeneral,
                                                 request.Periodo,
                                                 (saldo>0?(short)2:(short)1),
                                                 Math.Abs(saldo)*factor,request.IdTercero);
                        }

                    }
                    #endregion ActualizarCuentaPorPagar

                    List<EgresoItem> itemsCajaBancos= new List<EgresoItem>();
                    #region ActualizarSaldoItems
                    foreach(var item in items)
                    {
                        var  prs= DAL.GetPresupuestoActivo(proxy,request.IdSucursal, item.IdCentro);
                        prs.AssertExistsActivo(request.IdSucursal, item.IdCentro);
                        //urn:PresupuestoItem:Id{0}"
                        using(proxy.AcquireLock(item.IdPresupuestoItem.GetLockKey<PresupuestoItem>(), Definiciones.LockSeconds))
                        {
                            var pi= DAL.GetPresupuestoItem(proxy,item.IdPresupuestoItem);
                            pi.AssertExists(item.IdPresupuestoItem);
                            CheckTercero(pi,item);
                            pi.UpdatePresupuesto(proxy, request.IdSucursal,item.IdCentro,request.Periodo,item.TipoPartida,item.Valor*factor, item.IdTercero);
                            if (pi.Codigo.StartsWith(Definiciones.GrupoCajaBancos)) itemsCajaBancos.Add(item);
                        }
                    }
                    #endregion ActualizarSaldoItems
                    #region CrearComprobanteEgresos
                    // solo es crear el comprobante y un detalle, los saldos ya estan actualizados...
                    // el centro de costo no interesa...

                    if(factor==1) // asentar --Generar el comprobante de egreso por cuenta y sus items
                    {
                        var valorPorCuentaGiradora=
                            (from r in itemsCajaBancos  group r by r.IdPresupuestoItem into g
                             select new { IdPresupuestoItem=g.Key, Valor=g.Sum(p => p.Valor*(p.TipoPartida==1?-1:1))*factor}).
                                ToList(); 
                        foreach(var cg in valorPorCuentaGiradora)
                        {
                            string descripcion=string.Format("Cancelacion {0}:{1} ${2}",request.CodigoDocumento,
                                                             request.Documento, cg.Valor); 
                            ComprobanteEgreso ce= DAL.CreateComprobanteEgreso(proxy,request.IdSucursal,
                                                                              cg.IdPresupuestoItem,
                                                                              request.IdTercero,
                                                                              cg.Valor,
                                                                              descripcion,
                                                                              request.IdTerceroReceptor,
                                                                              DateTime.Today,true);
                            ce.CreateItem(proxy,request.Id,cg.Valor);

                        }
                        proxy.ExecuteBeforePatch(request, oldData, operacion);
                        request.Asentar(proxy);
                        proxy.ExecuteAfterPatch(request, oldData, operacion);
                    }
                    else if(factor==-1)  // Reversar= anular todos los comprobantes de este egreso!!
                    {
                        if( itemsCajaBancos.Count!=0)
                        {
                            CheckSaldo(request,saldo);
                            var ei=  request.GetComprobanteEgresoItems(proxy);
                            foreach(var cei in ei){
                                using(proxy.AcquireLock(cei.IdComprobanteEgreso.GetLockKey<ComprobanteEgreso>(),Definiciones.LockSeconds))
                                {
                                    ComprobanteEgreso ce = DAL.GetComprobanteEgreso(proxy, cei.IdComprobanteEgreso);
                                    ce.Anular(proxy,string.Format("Anulado. Egreso:'{0}' reversado",request.Numero));
                                }
                            }
                        }
                        proxy.ExecuteBeforePatch(request, oldData, operacion);
                        request.Reversar(proxy);
                        proxy.ExecuteAfterPatch(request, oldData, operacion);
                    }
                    #endregion CrearComprobanteEgresos

                    proxy.CommitDbTransaction();

                }
            });

                    
            List<Egreso> data = new List<Egreso>();
            data.Add(request);
            
            return new Response<Egreso>(){
                Data=data
            };  
            
        }
        #endregion Patch


		public static void ValidateAndThrowHttpError(this Egreso request, string ruleSet)
		{
			EgresoValidator av = new EgresoValidator();
			av.ValidateAndThrowHttpError(request, ruleSet);
		}
		
			

        private static void CheckOldAndNew(Egreso oldData, Egreso request,
                                           DALProxy proxy,
                                           int idUsuario)
        {
            oldData.ValidateAndThrowHttpError(Operaciones.Update);

            Egresos egresos= new Egresos(){Nuevo=request, Viejo=oldData};
            EgresosValidator ev = new EgresosValidator();
            ev.ValidateAndThrowHttpError(egresos,Operaciones.Update);

            oldData.CheckSucursal(proxy, idUsuario);

            var data = new Egreso();
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

            if(request.IdTerceroReceptor.HasValue && request.IdTerceroReceptor.Value!=default(int))
            {
                if(!data.IdTerceroReceptor.HasValue ||
                   (data.IdTerceroReceptor.HasValue &&
                    data.IdTerceroReceptor.Value!=request.IdTerceroReceptor.Value))
                {
                    data.IdTerceroReceptor=request.IdTerceroReceptor;
                    var tr =data.CheckTerceroReceptor(proxy);
                    data.DocumentoReceptor= tr.Documento;
                    data.NombreDocumentoReceptor= tr.NombreDocumento;
                    data.NombreReceptor=tr.Nombre;
                    data.DVReceptor= tr.DigitoVerificacion;
                }

            }

            if(!request.Documento.IsNullOrEmpty() && request.Documento!=data.Documento)
                data.Documento=request.Documento;

            if(!request.Descripcion.IsNullOrEmpty() && request.Descripcion!=data.Descripcion)
                data.Descripcion=request.Descripcion;

            if(request.DiasCredito!=data.DiasCredito) data.DiasCredito=request.DiasCredito;

            request.PopulateWith(data);
        }


        private static void CheckBeforePatch(Egreso oldData, Egreso request,
                                             DALProxy proxy,
                                             int idUsuario,
                                             string operacion)
        {
            oldData.ValidateAndThrowHttpError(operacion);
            Egresos egresos= new Egresos(){Nuevo=request, Viejo=oldData};
            EgresosValidator ev = new EgresosValidator();
            ev.ValidateAndThrowHttpError(egresos,operacion);

            oldData.CheckSucursal(proxy,idUsuario);
            oldData.CheckPeriodo(proxy);

            var data = new Egreso();
            data.PopulateWith(oldData);

            data.FechaAnulado=request.FechaAnulado;
            data.FechaAsentado= request.FechaAsentado;

            request.PopulateWith(data);

        }


        private static void CheckTercero(PresupuestoItem presupuestoItem, EgresoItem egresoItem)
        {
            if(presupuestoItem.UsaTercero)
            {
                if(!egresoItem.IdTercero.HasValue)
                {
                    throw new HttpError(string.Format("Item de Presupuesto: '{0}' usa Tercero. EgresoItem.Id:'{1}'",
                                                      presupuestoItem.Nombre, egresoItem.Id ));            
                }
            }
        }

        private static void CheckSaldo(Egreso request, decimal saldo)
        {
            if(saldo!=request.Saldo)
                throw new HttpError(string.Format("El Egreso:'{0}' NO puede ser Reversado. Revise los comprobantes de Egreso",
                                                      request.Numero ));            
        }


        private static Tercero CheckTerceroReceptor(this Egreso request, DALProxy proxy)
        {
            Tercero t = proxy.FirstOrDefaultByIdFromCache<Tercero>(request.IdTerceroReceptor.Value);

            t.AssertExists(request.IdTerceroReceptor.Value);
            return t;

        }

		
	}
}


//310 359 62 14

/*
#region ActualizarCuentaPorPagar
                    if(request.Saldo>0)
                    {
                        var saldoPorCentro = 
                            (from r in items group r by r.IdCentro into g
                             select new { IdCentro=g.Key, Valor=g.Sum(p => p.Valor*(p.TipoPartida==1?1:-1))}).
                                ToList(); 
                                    
                        foreach(var r in saldoPorCentro){
                            var  prs= DAL.GetPresupuestoActivo(proxy,request.IdSucursal,r.IdCentro);
                            prs.AssertExistsActivo(request.IdSucursal, r.IdCentro);
                            //urn:PresupuestoItem:IdPresupuesto:{0}:Codigo:{1}"
                            CodigoDocumento cd = DAL.GetCodigoDocumento(proxy, request.CodigoDocumento);
                            cd.AssertExists(request.CodigoDocumento);
                            cd.AssertEstaActivo();
                            using(proxy.AcquireLock(prs.GetLockKey(cd.CodigoPresupuesto), Definiciones.LockSeconds))
                            {
                                var pi= prs.GetPresupuestoItem(proxy,cd.CodigoPresupuesto);
                                pi.AssertExists(prs.Id,cd.CodigoPresupuesto );
                                pi.UpdatePresupuesto(proxy,request.IdSucursal,r.IdCentro,request.Periodo,
                                                     (r.Valor>0?(short)2:(short)1),
                                                     Math.Abs(r.Valor)*factor,request.IdTercero);
                            }
                            saldo+=r.Valor;
                        }
                    }
                    #endregion ActualizarCuentaPorPagar
*/


