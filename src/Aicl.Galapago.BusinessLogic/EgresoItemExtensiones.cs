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
using ServiceStack.Text;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;

namespace Aicl.Galapago.BusinessLogic
{
    public static class EgresoItemExtensiones
    {

        #region Get
        public static Response<EgresoItem> Get(this EgresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {


            var data = factory.Execute(proxy=>{
               var visitor = ReadExtensions.CreateExpression<EgresoItem>();
               
               visitor.Where(r=>r.IdEgreso==request.IdEgreso).OrderBy(r=>r.TipoPartida);

               return proxy.Get(visitor);
            });

                        
            return new Response<EgresoItem>(){
                Data=data
            };

        }
        #endregion Get

        #region Post        
        public static Response<EgresoItem> Post(this EgresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            if(request.IdTercero.HasValue && request.IdTercero.Value==default(int)) request.IdTercero=null;

            request.ValidateAndThrowHttpError(Operaciones.Create);


            factory.Execute(proxy=>{

                PresupuestoItem pi= Check1(proxy,request, int.Parse(authSession.UserAuthId));

                // bloquear el Egreso parent para evitar actualizaciones....
                using(proxy.AcquireLock(request.IdEgreso.GetLockKey<Egreso>(), Definiciones.LockSeconds))
                {
                    Egreso egreso= DAL.GetEgresoById(proxy,request.IdEgreso);
                    egreso.AssertExists(request.IdEgreso);
                    egreso.ValidateAndThrowHttpError(Operaciones.Update);
                    egreso.CheckPeriodo(proxy);
                    request.CheckCentro(proxy, egreso.IdSucursal,int.Parse(authSession.UserAuthId));

                    CodigoDocumento cd = DAL.GetCodigoDocumento(proxy,egreso.CodigoDocumento);
                    cd.AssertExists(egreso.CodigoDocumento); 
                    cd.AssertEstaActivo();

                    if(request.TipoPartida==1)
                    {
                        cd.CheckDebitos(pi.Codigo);
                        egreso.Valor= egreso.Valor+request.Valor;
                        egreso.Saldo=egreso.Saldo+request.Valor;
                    }
                    else
                    {
                        cd.CheckCreditos(pi.Codigo);
                        egreso.Saldo=egreso.Saldo-request.Valor;
                    }

                    proxy.BeginDbTransaction();
                    egreso.ActualizarValorSaldo(proxy);
                    request.Create(proxy);
                    proxy.CommitDbTransaction();

                }
            });

            List<EgresoItem> data = new List<EgresoItem>();
            data.Add(request);
            
            return new Response<EgresoItem>(){
                Data=data
            };  
            
        }
        #endregion Post

        #region Put
        public static Response<EgresoItem> Put(this EgresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            if(request.IdTercero.HasValue && request.IdTercero.Value==default(int)) request.IdTercero=null;

            request.CheckId(Operaciones.Update);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdEgreso.GetLockKey<Egreso>(), Definiciones.LockSeconds))
                {
                    EgresoItem oldData = DAL.FirstOrDefaultById<EgresoItem>(proxy, request.Id);
                    oldData.AssertExists(request.Id);

                    Egreso egreso=  DAL.GetEgresoById(proxy, oldData.IdEgreso);
                    egreso.AssertExists(request.IdEgreso);

                    CheckOldAndNew(egreso,request,oldData, proxy, int.Parse(authSession.UserAuthId));
                    PresupuestoItem pi= Check1(proxy, request, int.Parse(authSession.UserAuthId));

                    CodigoDocumento cd = DAL.GetCodigoDocumento(proxy,egreso.CodigoDocumento);
                    cd.AssertExists(egreso.CodigoDocumento);
                    cd.AssertEstaActivo();


                    if(request.TipoPartida!=oldData.TipoPartida || request.Valor!=oldData.Valor)
                    {
                        if(oldData.TipoPartida==1)
                        {
                            egreso.Valor= egreso.Valor-oldData.Valor;
                            egreso.Saldo= egreso.Saldo-oldData.Valor;
                        }
                        else
                            egreso.Saldo=egreso.Saldo+oldData.Valor;

                        if(request.TipoPartida==1)
                        {
                            cd.CheckDebitos(pi.Codigo);
                            egreso.Valor= egreso.Valor+request.Valor;
                            egreso.Saldo=egreso.Saldo+request.Valor;
                        }
                        else
                        {
                            cd.CheckCreditos(pi.Codigo);
                            egreso.Saldo=egreso.Saldo-request.Valor;
                        }
    
                        proxy.BeginDbTransaction();
                        egreso.ActualizarValorSaldo(proxy);
                        request.Actualizar(proxy);
                        proxy.CommitDbTransaction();
                    }
                    else
                        request.Actualizar(proxy);
                }
            });


            List<EgresoItem> data = new List<EgresoItem>();
            data.Add(request);

            return new Response<EgresoItem>(){
                Data=data
            };
        }
        #endregion Put

        #region Delete
        public static Response<EgresoItem> Delete(this EgresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.CheckId(Operaciones.Destroy);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdEgreso.GetLockKey<Egreso>(), Definiciones.LockSeconds))
                {
                    EgresoItem oldData = DAL.FirstOrDefaultById<EgresoItem>(proxy, request.Id);
                    oldData.AssertExists(request.Id);

                    Egreso egreso=  DAL.GetEgresoById(proxy, oldData.IdEgreso);
                    egreso.AssertExists(request.IdEgreso);

                    CheckOldAndNew(egreso,request,oldData, proxy, int.Parse(authSession.UserAuthId));
                   
                    if(oldData.TipoPartida==1)
                    {
                        egreso.Valor= egreso.Valor-oldData.Valor;
                        egreso.Saldo= egreso.Saldo-oldData.Valor;
                    }
                    else
                        egreso.Saldo=egreso.Saldo+oldData.Valor;
                                     
                    proxy.BeginDbTransaction();
                    egreso.ActualizarValorSaldo(proxy);
                    request.Borrar(proxy);
                    proxy.CommitDbTransaction();

                }
            });



            List<EgresoItem> data = new List<EgresoItem>();
            data.Add(request);

            return new Response<EgresoItem>(){
                Data=data
            };
        }
        #endregion Destroy


        private static void ValidateAndThrowHttpError(this EgresoItem  request, string ruleSet)
        {
            EgresoItemValidator av = new EgresoItemValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }


        private static PresupuestoItem Check1(DALProxy proxy, EgresoItem request, int idUsuario)
        {
            PresupuestoItem pi = CheckPresupuestoItem(proxy,request);

            Presupuesto pr =DAL.GetPresupuestoById(proxy,pi.IdPresupuesto);
            pr.AssertExists(pi.IdPresupuesto);

            Centro centro= DAL.FirstOrDefaultById<Centro>(proxy, request.IdCentro);
            centro.AssertExists(request.IdCentro);

            Tercero tercero= default(Tercero);
            if(!pi.UsaTercero) request.IdTercero=null;
            if(request.IdTercero.HasValue)
                tercero= DAL.FirstOrDefaultById<Tercero>(proxy, request.IdTercero.Value);


            EgresoItemAlCrear ei = new EgresoItemAlCrear(){
                NewItem=request,
                Prs= pr,
                Pi= pi,
                CentroItem= centro,
                TerceroItem=tercero
            };

            EgresoItemAlCrearValidador eiv = new EgresoItemAlCrearValidador();
            eiv.ValidateAndThrowHttpError(ei, EgresoItemAlCrear.Regla1);

            pi.CheckUsuarioGiradora(proxy, idUsuario, request.IdTercero);


            request.CodigoItem= pi.Codigo;
            request.NombreItem= pi.Nombre;
            request.NombreCentro=centro.Nombre;
            if(tercero!=default(Tercero))
            {
                request.NombreTercero=tercero.Nombre;
                request.DocumentoTercero=tercero.Documento;
                request.DVTercero= tercero.DigitoVerificacion;
            }

            return pi;
        }


        private static void CheckOldAndNew(Egreso egreso, EgresoItem request, EgresoItem oldData,
                                           DALProxy proxy,
                                           int idUsuario)
        {
            egreso.ValidateAndThrowHttpError(Operaciones.Update);
            egreso.CheckPeriodo(proxy);

            oldData.CheckCentro(proxy,egreso.IdSucursal, idUsuario);

            EgresoItem data = new EgresoItem();
            data.PopulateWith(oldData);

            if(request.IdCentro!=default(int) && request.IdCentro!=data.IdCentro)
            {
                data.IdCentro=request.IdCentro;
                data.CheckCentro(proxy, egreso.IdSucursal, idUsuario);
            }

            if(request.IdTercero.HasValue &&
               ( !data.IdTercero.HasValue ||  (data.IdTercero.HasValue && request.IdTercero.Value!=data.IdTercero.Value)))
            {
                data.IdTercero= request.IdTercero;
            }

            if(request.IdPresupuestoItem!=default(int) && request.IdPresupuestoItem!= data.IdPresupuestoItem)
                data.IdPresupuestoItem= request.IdPresupuestoItem;

            if(request.TipoPartida!=default(short) && request.TipoPartida!=data.TipoPartida)
                data.TipoPartida=request.TipoPartida;

            if(request.Valor!=default(decimal) && request.Valor!=data.Valor)
                data.Valor=request.Valor;

            request.PopulateWith(data);

        }   

        private static PresupuestoItem CheckPresupuestoItem(DALProxy proxy,EgresoItem egresoItem)
        {
            PresupuestoItem pi = DAL.GetPresupuestoItem(proxy, egresoItem.IdPresupuestoItem);
            pi.AssertExists(egresoItem.IdPresupuestoItem);
            // en check 1 se hace el resto de las validaciones para pi
            return pi;

        }

    }
}

