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
        public static Response<ComprobanteEgreso> Get(this ComprobanteEgreso request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
            return factory.Execute(proxy=>{

				long? totalCount=null;

				var paginador= new Paginador(httpRequest);
            	var queryString= httpRequest.QueryString;

                Expression<Func<ComprobanteEgreso, bool>> predicate;

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

                var visitor = ReadExtensions.CreateExpression<ComprobanteEgreso>();
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

				return new Response<ComprobanteEgreso>(){
                	Data=proxy.Get(visitor),
                	TotalCount=totalCount
            	};

            });

        }
        #endregion Get

        // TODO:   No permitir Valor < 0 ??? confirmar .... seria una devolucion ?
        #region Post        
        public static Response<ComprobanteEgreso> Post(this ComprobanteEgreso request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
			if(request.IdTerceroReceptor==default(int)){
				request.IdTerceroReceptor= request.IdTercero;
			}

            request.ValidateAndThrowHttpError(Operaciones.Create);
            var idUsuario = int.Parse(authSession.UserAuthId);
            request.Periodo= request.Fecha.ObtenerPeriodo();

            factory.Execute(proxy=>{

                var sucursal= request.CheckSucursal(proxy,idUsuario);
                var tercero =request.CheckTercero(proxy);
				Tercero tr = default(Tercero);
				if( request.IdTercero!=request.IdTerceroReceptor)
                	tr= request.CheckTerceroReceptor(proxy);
				else
					tr=tercero;

                request.CheckPeriodo(proxy);
                var pi = request.CheckUsuarioGiradora(proxy,int.Parse(authSession.UserAuthId)); 

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

                request.DocumentoReceptor= tr.Documento;
                request.NombreDocumentoReceptor= tr.NombreDocumento;
                request.NombreReceptor=tr.Nombre;
                request.DVReceptor= tr.DigitoVerificacion;

				request.CodigoItem= pi.Codigo;
				request.NombreItem = pi.Nombre;

            });
        
            List<ComprobanteEgreso> data = new List<ComprobanteEgreso>();
            data.Add(request);
            
            return new Response<ComprobanteEgreso>(){
                Data=data
            };  
            
        }
        #endregion Post



        #region Put
        public static Response<ComprobanteEgreso> Put(this ComprobanteEgreso request,
                                              Factory factory,
                                              IAuthSession authSession)                                
        {

            // TODO :  si cambio cambiar el Tercero y si el  Nro. Dto viene vacio traer Numero del nuevo Tercero...

            request.ValidateAndThrowHttpError(Definiciones.CheckRequestBeforeUpdate);
            var idUsuario = int.Parse(authSession.UserAuthId);

            factory.Execute(proxy=>{
                using(proxy.AcquireLock(request.GetLockKey(), Definiciones.LockSeconds))
                {
                    ComprobanteEgreso oldData= DAL.GetComprobanteEgreso(proxy, request.Id);
                    oldData.AssertExists(request.Id);
                    CheckOldAndNew(oldData, request, proxy, idUsuario );
                    request.Actualizar(proxy);
                }
            });

            var data = new List<ComprobanteEgreso>();
            data.Add(request);
            
            return new Response<ComprobanteEgreso>(){
                Data=data
            };  
            
        }
        #endregion Put

        #region Patch
        public static Response<ComprobanteEgreso> Patch(this ComprobanteEgreso request,
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
                throw new HttpError(string.Format("Operacion:'{0}' NO implementada para ComprobanteEgreso",
                                                      action ));            
             
            request.ValidateAndThrowHttpError(rule);
            var idUsuario = int.Parse(authSession.UserAuthId);

            factory.Execute(proxy=>{
                using(proxy.AcquireLock(request.GetLockKey(), Definiciones.LockSeconds))
                {
                    ComprobanteEgreso oldData= DAL.GetComprobanteEgreso(proxy, request.Id);
                    oldData.AssertExists(request.Id);
                    CheckBeforePatch(oldData, request, proxy, idUsuario, operacion);

                    if(factor==0)
                    {
                        proxy.BeginDbTransaction();
                        request.Anular(proxy,"Anulado por Usuario");
                        proxy.CommitDbTransaction();
                        return;
                    }

                    List<ComprobanteEgresoItem> items = request.GetItems(proxy);

                    proxy.BeginDbTransaction();
                    #region ActualizarCuentaPorPagar
                    foreach(ComprobanteEgresoItem cei in items)
                    {
                        using (proxy.AcquireLock(cei.IdEgreso.GetLockKey<Egreso>(), Definiciones.LockSeconds))
                        {
                            var egreso = DAL.GetEgresoById(proxy, cei.IdEgreso);

							if(operacion=="asentar")
							{
                            	var ece= new EgresoCE(){Egreso=egreso, Cei= cei};
                            	var ecv= new EgresoCEValidator();
                            	ecv.ValidateAndThrowHttpError(ece, Operaciones.ActualizarValorEgresoAlAsentarCE);
							}

                            egreso.Saldo= egreso.Saldo-( cei.Abono*factor);
                            egreso.ActualizarValorSaldo(proxy);

                            var  prs= DAL.GetPresupuestoActivo(proxy,request.IdSucursal,Definiciones.IdCentroGeneral);
                            prs.AssertExistsActivo(request.IdSucursal, Definiciones.IdCentroGeneral);

                            CodigoDocumento cd = proxy.GetCodigoDocumento(egreso.CodigoDocumento);
                            cd.AssertExists(egreso.CodigoDocumento);
                            cd.AssertEstaActivo();

                            //urn:PresupuestoItem:IdPresupuesto:{0}:Codigo:{1}"
                            using(proxy.AcquireLock(prs.GetLockKey(cd.CodigoPresupuesto), Definiciones.LockSeconds))
                            {
                                var pi= prs.GetPresupuestoItem(proxy,cd.CodigoPresupuesto);
                                pi.AssertExists(prs.Id,cd.CodigoPresupuesto );
                                pi.UpdatePresupuesto(proxy,request.IdSucursal,Definiciones.IdCentroGeneral,
                                                     request.Periodo,
                                                     (cei.Abono>0?(short)1:(short)2),
                                                     Math.Abs(cei.Abono)*factor,request.IdTercero);
                            }

                            var retList = cei.GetRetenciones(proxy);
                            foreach(ComprobanteEgresoRetencion ret in retList)
                            {
                                using(proxy.AcquireLock(ret.IdPresupuestoItem.GetLockKey<PresupuestoItem>(), Definiciones.LockSeconds))
                                {
                                    var pi= DAL.GetPresupuestoItem(proxy,ret.IdPresupuestoItem);
                                    pi.AssertExists(ret.IdPresupuestoItem);
                                    pi.UpdatePresupuesto(proxy, request.IdSucursal,
                                                         Definiciones.IdCentroGeneral,request.Periodo,
                                                         (ret.Valor>0?(short)2:(short)1), 
                                                         Math.Abs(ret.Valor)*factor, null);
                                }
                            }
                        }

                    }
                    //Actualizar Valor en Comprobante Egreso  NO
                    // Actualizar el presupuesto_item  de la cuenta giradora....

                    using(proxy.AcquireLock(request.IdCuentaGiradora.GetLockKey<PresupuestoItem>(), Definiciones.LockSeconds))
                    {
                        var pi= DAL.GetPresupuestoItem(proxy,request.IdCuentaGiradora);
                        pi.AssertExists(request.IdCuentaGiradora);

                        pi.UpdatePresupuesto(proxy, request.IdSucursal,
                                             Definiciones.IdCentroGeneral,request.Periodo,
                                             (request.Valor>0?(short)2:(short)1),
                                             request.Valor*factor, request.IdTerceroGiradora);
                    }

                    //}
                    #endregion ActualizarCuentaPorPagar

                    toDo(proxy);
                    proxy.CommitDbTransaction();

                }
            });


                    
            List<ComprobanteEgreso> data = new List<ComprobanteEgreso>();
            data.Add(request);
            
            return new Response<ComprobanteEgreso>(){
                Data=data
            };  
            
        }
        #endregion Patch



        private static void CheckOldAndNew(ComprobanteEgreso oldData, ComprobanteEgreso request,
                                           DALProxy proxy,
                                           int idUsuario)
        {
            oldData.ValidateAndThrowHttpError(Operaciones.Update);

            CEs ces= new CEs(){Nuevo=request, Viejo=oldData};
            CEsValidator ev = new CEsValidator();
            ev.ValidateAndThrowHttpError(ces,Operaciones.Update);

            oldData.CheckSucursal(proxy, idUsuario);

            var data = new ComprobanteEgreso();
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

            if(request.IdTerceroReceptor!=default(int) && request.IdTerceroReceptor!=data.IdTerceroReceptor )
            {
                data.IdTerceroReceptor=request.IdTerceroReceptor;
                var tr= data.CheckTerceroReceptor(proxy);
				data.DocumentoReceptor= tr.Documento;
                data.NombreDocumentoReceptor= tr.NombreDocumento;
                data.NombreReceptor=tr.Nombre;
                data.DVReceptor= tr.DigitoVerificacion;
            }

            if(!request.Descripcion.IsNullOrEmpty() && request.Descripcion!=data.Descripcion)
                data.Descripcion=request.Descripcion;

            bool checkcg=false;

            if(request.IdCuentaGiradora!=default(int) && request.IdCuentaGiradora!=data.IdCuentaGiradora)
            {
                data.IdCuentaGiradora= request.IdCuentaGiradora;
                checkcg=true;
            }


            if((request.IdTerceroGiradora.HasValue && request.IdTerceroGiradora.Value!=default(int)) &&
               ( !data.IdTerceroGiradora.HasValue || 
                    (data.IdTerceroGiradora.HasValue && request.IdTerceroGiradora.Value!=data.IdTerceroGiradora.Value)))
            {
                data.IdTerceroGiradora= request.IdTerceroGiradora;
                checkcg=true;
            }

            if(checkcg){
				var pi = data.CheckUsuarioGiradora(proxy, idUsuario);
				data.CodigoItem= pi.Codigo;
				data.NombreItem = pi.Nombre;
			}

            request.PopulateWith(data);
        }

        public static void ValidateAndThrowHttpError(this ComprobanteEgreso request, string ruleSet)
        {
            ComprobanteEgresoValidator av = new ComprobanteEgresoValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }


        static Tercero CheckTerceroReceptor(this ComprobanteEgreso request, DALProxy proxy)
        {
            Tercero t = proxy.FirstOrDefaultByIdFromCache<Tercero>(request.IdTerceroReceptor);

            t.AssertExists(request.IdTerceroReceptor);

			return t;

        }


        static void CheckBeforePatch(ComprobanteEgreso oldData, ComprobanteEgreso request,
                                             DALProxy proxy,
                                             int idUsuario,
                                             string operacion)
        {
            oldData.ValidateAndThrowHttpError(operacion);
            CEs ces= new CEs(){Nuevo=request, Viejo=oldData};
            CEsValidator ev = new CEsValidator();
            ev.ValidateAndThrowHttpError(ces,operacion);

            oldData.CheckSucursal(proxy,idUsuario);
            oldData.CheckPeriodo(proxy);

            var data = new ComprobanteEgreso();
            data.PopulateWith(oldData);

            data.FechaAnulado=request.FechaAnulado;
            data.FechaAsentado= request.FechaAsentado;

            request.PopulateWith(data);

        }

        static PresupuestoItem CheckUsuarioGiradora(this ComprobanteEgreso documento, DALProxy proxy, int idUsuario)
        {
            PresupuestoItem pi = DAL.GetPresupuestoItem(proxy, documento.IdCuentaGiradora);
            pi.AssertExists(documento.IdCuentaGiradora);

            PresupuestoItemValidador piv= new PresupuestoItemValidador();
            piv.ValidateAndThrowHttpError(pi, Definiciones.PrspItemActivo);

			if(!pi.UsaTercero) documento.IdTerceroGiradora=null;

            pi.CheckUsuarioGiradora(proxy,idUsuario, documento.IdTerceroGiradora);
			return pi;
        }



    }
}