using System;
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
    public static partial class BL
    {
        #region Get
        public static Response<ComprobanteIngreso> Get(this ComprobanteIngreso request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
            return factory.Execute(proxy=>{

				long? totalCount=null;

				var paginador= new Paginador(httpRequest);
            	var queryString= httpRequest.QueryString;

                Expression<Func<ComprobanteIngreso, bool>> predicate;

				var periodo= queryString["Periodo"];
				if(periodo.IsNullOrEmpty()) periodo= DateTime.Today.ObtenerPeriodo();
                if (periodo.Length==6)
                    predicate= q=>q.Periodo==periodo;
                else
                    predicate= q=>q.Periodo.StartsWith(periodo) ;

				var nombre= queryString["NombreTercero"];
                if(!nombre.IsNullOrEmpty())
                    predicate= predicate.AndAlso(q=>q.NombreTercero.Contains(nombre));

				var sucursal= queryString["NombreSucursal"];
                if(!sucursal.IsNullOrEmpty())
                    predicate= predicate.AndAlso(q=>q.NombreSucursal.Contains(sucursal));

				string asentado= queryString["Asentado"];
            	if(!asentado.IsNullOrEmpty())
           		{
                	bool tomarSoloAsentado;
	                if( bool.TryParse(asentado,out tomarSoloAsentado))
	                {
						if(tomarSoloAsentado)
							predicate= predicate.AndAlso(q=>q.FechaAsentado!=null);
						else
							predicate= predicate.AndAlso(q=>q.FechaAsentado==null);
	                }
	            }

                var visitor = ReadExtensions.CreateExpression<ComprobanteIngreso>();
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

				return new Response<ComprobanteIngreso>(){
                	Data=proxy.Get(visitor),
                	TotalCount=totalCount
            	};

            });

        }
        #endregion Get

        // TODO:   No permitir Valor < 0 ??? confirmar .... seria una devolucion ?
        #region Post        
        public static Response<ComprobanteIngreso> Post(this ComprobanteIngreso request,
                                            Factory factory,
                                            IAuthSession authSession)
        {


            request.ValidateAndThrowHttpError(Operaciones.Create);
            var idUsuario = int.Parse(authSession.UserAuthId);
            request.Periodo= request.Fecha.ObtenerPeriodo();

            factory.Execute(proxy=>{

                var sucursal= request.CheckSucursal(proxy,idUsuario);
                var tercero =request.CheckTercero(proxy);

                request.CheckPeriodo(proxy);
                var pi = request.CheckUsuarioReceptora(proxy,int.Parse(authSession.UserAuthId)); 

                using (proxy.AcquireLock(request.GetLockKeyConsecutivo(), Definiciones.LockSeconds))
                {
                    proxy.BeginDbTransaction();
                    request.AsignarConsecutivo(proxy);
                    request.Create(proxy);
                    proxy.CommitDbTransaction();
                }

				request.NombreSucursal=sucursal.Nombre;

                request.NombreTercero=tercero.Nombre;
                request.DocumentoTercero= tercero.Documento;
                request.NombreDocumentoTercero= tercero.NombreDocumento;
                request.DVTercero= tercero.DigitoVerificacion;

                
				request.CodigoItem= pi.Codigo;
				request.NombreItem = pi.Nombre;

            });
        
            var data = new List<ComprobanteIngreso>();
            data.Add(request);
            
            return new Response<ComprobanteIngreso>(){
                Data=data
            };  
            
        }
        #endregion Post



        #region Put
        public static Response<ComprobanteIngreso> Put(this ComprobanteIngreso request,
                                              Factory factory,
                                              IAuthSession authSession)                                
        {

            // TODO :  si cambio cambiar el Tercero y si el  Nro. Dto viene vacio traer Numero del nuevo Tercero...

            request.ValidateAndThrowHttpError(Definiciones.CheckRequestBeforeUpdate);
            var idUsuario = int.Parse(authSession.UserAuthId);

            factory.Execute(proxy=>{
                using(proxy.AcquireLock(request.GetLockKey(), Definiciones.LockSeconds))
                {
                    ComprobanteIngreso oldData= proxy.FirstOrDefaultById<ComprobanteIngreso>(request.Id);
                    oldData.AssertExists(request.Id);
                    CheckOldAndNew(oldData, request, proxy, idUsuario );
                    request.Actualizar(proxy);
                }
            });

            var data = new List<ComprobanteIngreso>();
            data.Add(request);
            
            return new Response<ComprobanteIngreso>(){
                Data=data
            };  
            
        }
        #endregion Put

        #region Patch
        public static Response<ComprobanteIngreso> Patch(this ComprobanteIngreso request,
                                             Factory factory,
                                             IAuthSession authSession,
                                             string action)
        {
            int factor;
            string operacion;
            string rule;
            Action<DALProxy> toDo=null;

            if(action=="asentar")
            {
                rule=Definiciones.CheckRequestBeforeAsentar;
                operacion= Operaciones.Asentar;
                factor=1;
                toDo= request.Asentar;
            }
            else if(action=="reversar")
            {
                rule=Definiciones.CheckRequestBeforeReversar;
                operacion= Operaciones.Reversar;
                factor=-1;
                toDo=request.Reversar;
            }
            else if(action=="anular")
            {
                 rule= Definiciones.CheckRequestBeforeAnular;
                 operacion= Operaciones.Anular;
                 factor=0;
            }
            else
                throw new HttpError(string.Format("Operacion:'{0}' NO implementada para ComprobanteIngreso",
                                                      action ));            
            
            var idUsuario = int.Parse(authSession.UserAuthId);

            factory.Execute(proxy=>{
                using(proxy.AcquireLock(request.GetLockKey(), Definiciones.LockSeconds))
                {
                    ComprobanteIngreso oldData= proxy.FirstOrDefaultById<ComprobanteIngreso>(request.Id);
					oldData.ValidateAndThrowHttpError(rule);
                    oldData.AssertExists(request.Id);
                    CheckBeforePatch(oldData, request, proxy, idUsuario, operacion);

                    if(factor==0)
                    {
                        proxy.BeginDbTransaction();
                        request.Anular(proxy,"Anulado por Usuario");
                        proxy.CommitDbTransaction();
                        return;
                    }

                    List<ComprobanteIngresoItem> items = proxy.Get<ComprobanteIngresoItem>(q=> q.IdComprobanteIngreso==request.Id);

                    proxy.BeginDbTransaction();
                    #region ActualizarCuentaPorPagar
                    foreach(ComprobanteIngresoItem cei in items)
                    {
                        using (proxy.AcquireLock(cei.IdIngreso.GetLockKey<Ingreso>(), Definiciones.LockSeconds))
                        {
                            var ingreso = proxy.FirstOrDefaultById<Ingreso>( cei.IdIngreso);

							if(operacion=="asentar")
							{
                            	var ece= new IngresoCI(){Ingreso=ingreso, Cei= cei};
                            	var ecv= new IngresoCIValidator();
                            	ecv.ValidateAndThrowHttpError(ece, Operaciones.ActualizarValorIngresoAlAsentarCI);
							}

                            ingreso.Saldo= ingreso.Saldo-( cei.Abono*factor);
                            ingreso.ActualizarValorSaldo(proxy);

                            var  prs= DAL.GetPresupuestoActivo(proxy,request.IdSucursal,Definiciones.IdCentroGeneral);
                            prs.AssertExistsActivo(request.IdSucursal, Definiciones.IdCentroGeneral);

                            CodigoDocumento cd = proxy.GetCodigoDocumento(ingreso.CodigoDocumento);
                            cd.AssertExists(ingreso.CodigoDocumento);
                            cd.AssertEstaActivo();

                            //urn:PresupuestoItem:IdPresupuesto:{0}:Codigo:{1}"
                            using(proxy.AcquireLock(prs.GetLockKey(cd.CodigoPresupuesto), Definiciones.LockSeconds))
                            {
                                var pi= prs.GetPresupuestoItem(proxy,cd.CodigoPresupuesto);
                                pi.AssertExists(prs.Id,cd.CodigoPresupuesto );
                                pi.UpdatePresupuesto(proxy,request.IdSucursal,Definiciones.IdCentroGeneral,
                                                     request.Periodo,
                                                     (cei.Abono>0?(short)2:(short)1),
                                                     Math.Abs(cei.Abono)*factor,request.IdTercero);
                            }

                            var retList = proxy.Get<ComprobanteIngresoRetencion>(q=>q.IdComprobanteIngresoItem==cei.Id);
                            foreach(ComprobanteIngresoRetencion ret in retList)
                            {
                                using(proxy.AcquireLock(ret.IdPresupuestoItem.GetLockKey<PresupuestoItem>(), Definiciones.LockSeconds))
                                {
                                    var pi= DAL.GetPresupuestoItem(proxy,ret.IdPresupuestoItem);
                                    pi.AssertExists(ret.IdPresupuestoItem);
                                    pi.UpdatePresupuesto(proxy, request.IdSucursal,
                                                         Definiciones.IdCentroGeneral,request.Periodo,
                                                         (ret.Valor>0?(short)1:(short)2), 
                                                         Math.Abs(ret.Valor)*factor, null);
                                }
                            }
                        }

                    }
                    //Actualizar Valor en Comprobante Ingreso  NO
                    // Actualizar el presupuesto_item  de la cuenta giradora....

                    using(proxy.AcquireLock(request.IdCuentaReceptora.GetLockKey<PresupuestoItem>(), Definiciones.LockSeconds))
                    {
                        var pi= DAL.GetPresupuestoItem(proxy,request.IdCuentaReceptora);
                        pi.AssertExists(request.IdCuentaReceptora);

                        pi.UpdatePresupuesto(proxy, request.IdSucursal,
                                             Definiciones.IdCentroGeneral,request.Periodo,
                                             (request.Valor>0?(short)1:(short)2),
                                             request.Valor*factor, null);
                    }

                    //}
                    #endregion ActualizarCuentaPorPagar

                    toDo(proxy);
                    proxy.CommitDbTransaction();

                }
            });


                    
            List<ComprobanteIngreso> data = new List<ComprobanteIngreso>();
            data.Add(request);
            
            return new Response<ComprobanteIngreso>(){
                Data=data
            };  
            
        }
        #endregion Patch



        private static void CheckOldAndNew(ComprobanteIngreso oldData, ComprobanteIngreso request,
                                           DALProxy proxy,
                                           int idUsuario)
        {
            oldData.ValidateAndThrowHttpError(Operaciones.Update);

            var cis= new CIs(){Nuevo=request, Viejo=oldData};
            CIsValidator ev = new CIsValidator();
            ev.ValidateAndThrowHttpError(cis,Operaciones.Update);

            oldData.CheckSucursal(proxy, idUsuario);

            var data = new ComprobanteIngreso();
            data.PopulateWith(oldData);

            if( request.Fecha!=default(DateTime) && request.Fecha!=data.Fecha)
            {
                data.Fecha=request.Fecha;
                data.Periodo= data.Fecha.ObtenerPeriodo();
            }

            data.CheckPeriodo(proxy);

            //if(request.IdTercero!=default(int) && request.IdTercero!=data.IdTercero)
            //{
            //    data.IdTercero=request.IdTercero;
            //    data.CheckTercero(proxy);
            //}

            

            if(!request.Descripcion.IsNullOrEmpty() && request.Descripcion!=data.Descripcion)
                data.Descripcion=request.Descripcion;

            bool checkcg=false;

            if(request.IdCuentaReceptora!=default(int) && request.IdCuentaReceptora!=data.IdCuentaReceptora)
            {
                data.IdCuentaReceptora= request.IdCuentaReceptora;
                checkcg=true;
            }


            if((request.IdTerceroReceptora.HasValue && request.IdTerceroReceptora.Value!=default(int)) &&
               ( !data.IdTerceroReceptora.HasValue || 
                    (data.IdTerceroReceptora.HasValue && request.IdTerceroReceptora.Value!=data.IdTerceroReceptora.Value)))
            {
                data.IdTerceroReceptora= request.IdTerceroReceptora;
                checkcg=true;
            }

            if(checkcg){
				var pi = data.CheckUsuarioReceptora(proxy, idUsuario);
				data.CodigoItem= pi.Codigo;
				data.NombreItem = pi.Nombre;
			}

            request.PopulateWith(data);
        }

        public static void ValidateAndThrowHttpError(this ComprobanteIngreso request, string ruleSet)
        {
            ComprobanteIngresoValidator av = new ComprobanteIngresoValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }

		       


        static void CheckBeforePatch(ComprobanteIngreso oldData, ComprobanteIngreso request,
                                             DALProxy proxy,
                                             int idUsuario,
                                             string operacion)
        {
            oldData.ValidateAndThrowHttpError(operacion);
            CIs ces= new CIs(){Nuevo=request, Viejo=oldData};
            CIsValidator ev = new CIsValidator();
            ev.ValidateAndThrowHttpError(ces,operacion);

            oldData.CheckSucursal(proxy,idUsuario);
            oldData.CheckPeriodo(proxy);

            request.PopulateWith(oldData);

        }

        static PresupuestoItem CheckUsuarioReceptora(this ComprobanteIngreso documento, DALProxy proxy, int idUsuario)
        {
            PresupuestoItem pi = DAL.GetPresupuestoItem(proxy, documento.IdCuentaReceptora);
            pi.AssertExists(documento.IdCuentaReceptora);

            PresupuestoItemValidador piv= new PresupuestoItemValidador();
            piv.ValidateAndThrowHttpError(pi, Definiciones.PrspItemActivo);

			if(!pi.UsaTercero) documento.IdTerceroReceptora=null;

            pi.CheckUsuarioGiradora(proxy,idUsuario, documento.IdTerceroReceptora);
			return pi;
        }

    }
}