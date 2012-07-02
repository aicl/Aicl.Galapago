using System;
using System.Data;
using System.Text;
using System.IO;
using System.Linq;
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

namespace Aicl.Galapago.BusinessLogic
{
    public static class ComprobanteEgresoExtensiones
    {
        // TODO:   No permitir Valor < 0 ??? confirmar .... seria una devolucion ?

        #region Post        
        public static Response<ComprobanteEgreso> Post(this ComprobanteEgreso request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.ValidateAndThrowHttpError(Operaciones.Create);
            var idUsuario = int.Parse(authSession.UserAuthId);
            request.Periodo= request.Fecha.ObtenerPeriodo();

            factory.Execute(proxy=>{

                request.CheckSucursal(proxy,idUsuario);
                request.CheckTercero(proxy);
                request.CheckTerceroReceptor(proxy);
                request.CheckPeriodo(proxy);
                request.CheckUsuarioGiradora(proxy,int.Parse(authSession.UserAuthId)); 

                using (proxy.AcquireLock(request.GetLockKeyConsecutivo(), Definiciones.LockSeconds))
                {
                    proxy.BeginDbTransaction();
                    request.AsignarConsecutivo(proxy);
                    request.Create(proxy);
                    proxy.CommitDbTransaction();
                }
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

            List<ComprobanteEgreso> data = new List<ComprobanteEgreso>();
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
                        proxy.ExecuteBeforePatch(request, oldData, operacion);
                        request.Anular(proxy,"Anulado por Usuario");
                        proxy.ExecuteAfterPatch(request, oldData, operacion);
                        proxy.CommitDbTransaction();
                        return;
                    }

                    List<ComprobanteEgresoItem> items = request.GetItems(proxy);

                    proxy.BeginDbTransaction();
                    #region ActualizarCuentaPorPagar
                    //if(request.Valor!=0)
                    //{
                    foreach(ComprobanteEgresoItem cei in items)
                    {
                        using (proxy.AcquireLock(cei.IdEgreso.GetLockKey<Egreso>(), Definiciones.LockSeconds))
                        {
                            var egreso = DAL.GetEgresoById(proxy, cei.IdEgreso);

                            EgresoCE ece= new EgresoCE(){Egreso=egreso, Cei= cei};
                            EgresoCEValidator ecv= new EgresoCEValidator();
                            ecv.ValidateAndThrowHttpError(ece, Operaciones.ActualizarValorEgresoAlAsentarCE);

                            egreso.Saldo= egreso.Saldo-cei.Valor;
                            egreso.ActualizarValorSaldo(proxy);

                            var  prs= DAL.GetPresupuestoActivo(proxy,request.IdSucursal,Definiciones.IdCentroGeneral);
                            prs.AssertExistsActivo(request.IdSucursal, Definiciones.IdCentroGeneral);

                            CodigoDocumento cd = DAL.GetCodigoDocumento(proxy, egreso.CodigoDocumento);
                            cd.AssertExists(egreso.CodigoDocumento);
                            cd.AssertEstaActivo();

                            //urn:PresupuestoItem:IdPresupuesto:{0}:Codigo:{1}"
                            using(proxy.AcquireLock(prs.GetLockKey(cd.CodigoPresupuesto), Definiciones.LockSeconds))
                            {
                                var pi= prs.GetPresupuestoItem(proxy,cd.CodigoPresupuesto);
                                pi.AssertExists(prs.Id,cd.CodigoPresupuesto );
                                pi.UpdatePresupuesto(proxy,request.IdSucursal,Definiciones.IdCentroGeneral,
                                                     request.Periodo,
                                                     (cei.Valor>0?(short)1:(short)2),
                                                     Math.Abs(cei.Valor)*factor,request.IdTercero);
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
                data.CheckTerceroReceptor(proxy);
            }

            if(!request.Descripcion.IsNullOrEmpty() && request.Descripcion!=data.Descripcion)
                data.Descripcion=request.Descripcion;

            bool checkcg=false;

            if(request.IdCuentaGiradora!=default(int) && request.IdCuentaGiradora!=data.IdCuentaGiradora)
            {
                data.IdCuentaGiradora= request.IdCuentaGiradora;
                checkcg=true;
            }


            if(request.IdTerceroGiradora.HasValue &&
               ( !data.IdTerceroGiradora.HasValue || 
                    (data.IdTerceroGiradora.HasValue && request.IdTerceroGiradora.Value!=data.IdTerceroGiradora.Value)))
            {
                data.IdTerceroGiradora= request.IdTerceroGiradora;
                checkcg=true;
            }

            if(checkcg) data.CheckUsuarioGiradora(proxy, idUsuario);

            request.PopulateWith(data);
        }

        public static void ValidateAndThrowHttpError(this ComprobanteEgreso request, string ruleSet)
        {
            ComprobanteEgresoValidator av = new ComprobanteEgresoValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }


        private static void CheckTerceroReceptor(this ComprobanteEgreso request, DALProxy proxy)
        {
                Tercero t = DAL.FirstOrDefaultByIdFromCache<Tercero>(proxy, request.IdTerceroReceptor);

                t.AssertExists(request.IdTerceroReceptor);

        }


        private static void CheckBeforePatch(ComprobanteEgreso oldData, ComprobanteEgreso request,
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

        private static void CheckUsuarioGiradora(this ComprobanteEgreso documento, DALProxy proxy, int idUsuario)
        {
            PresupuestoItem pi = DAL.GetPresupuestoItem(proxy, documento.IdCuentaGiradora);
            pi.AssertExists(documento.IdCuentaGiradora);

            PresupuestoItemValidador piv= new PresupuestoItemValidador();
            piv.ValidateAndThrowHttpError(pi, Definiciones.PrspItemActivo);

            pi.CheckUsuarioGiradora(proxy,idUsuario, documento.IdTerceroGiradora);
        }



    }
}