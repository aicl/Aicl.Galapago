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
    public static class ComprobanteEgresoItemExtensiones
    {
        #region Post        
        public static Response<ComprobanteEgresoItem> Post(this ComprobanteEgresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {

         
            request.ValidateAndThrowHttpError(Operaciones.Create);

            factory.Execute(proxy=>{

                // bloquear el ComprobanteEgreso parent para evitar actualizaciones....
                using(proxy.AcquireLock(request.IdComprobanteEgreso.GetLockKey<ComprobanteEgreso>(), Definiciones.LockSeconds))
                {

                    ComprobanteEgreso ce = DAL.GetComprobanteEgreso(proxy, request.IdComprobanteEgreso);
                    ce.AssertExists(request.IdComprobanteEgreso);
                    ce.CheckPeriodo(proxy);
                                         
                    Egreso egreso= DAL.GetEgresoById(proxy,request.IdEgreso);
                    egreso.AssertExists(request.IdEgreso);

                    request.ValidateAndThrowHttpError(ce,egreso,Operaciones.InsertarEgresoEnCE);

                    ce.Valor+=request.Valor;

                    proxy.BeginDbTransaction();
                    ce.ActualizarValor(proxy);
                    request.Create(proxy);
                    proxy.CommitDbTransaction();

                }
            });

            List<ComprobanteEgresoItem> data = new List<ComprobanteEgresoItem>();
            data.Add(request);
            
            return new Response<ComprobanteEgresoItem>(){
                Data=data
            };  
            
        }
        #endregion Post


        #region Put
        public static Response<ComprobanteEgresoItem> Put(this ComprobanteEgresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.CheckId(Operaciones.Update);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdComprobanteEgreso.GetLockKey<ComprobanteEgreso>(), Definiciones.LockSeconds))
                {
                    ComprobanteEgresoItem oldData = DAL.FirstOrDefaultById<ComprobanteEgresoItem>(proxy, request.Id);
                    oldData.AssertExists(request.Id);

                    ComprobanteEgreso ce = DAL.GetComprobanteEgreso(proxy, oldData.IdComprobanteEgreso);
                    ce.AssertExists(oldData.IdComprobanteEgreso);
                    ce.CheckPeriodo(proxy);
                                         
                    Egreso egreso= DAL.GetEgresoById(proxy,oldData.IdEgreso);
                    egreso.AssertExists(oldData.IdEgreso);

                    request.ValidateAndThrowHttpError(oldData, ce,egreso,Operaciones.ActualizarEgresoEnCE);
                    request.CheckOldAndNew(oldData,proxy);
                                    
                    if( request.Valor!=oldData.Valor)
                    {
                        ce.Valor+=request.Valor-oldData.Valor;
                        proxy.BeginDbTransaction();
                        ce.ActualizarValor(proxy);
                        request.ActualizarValor(proxy);
                        proxy.CommitDbTransaction();
                    }
                }
            });


            List<ComprobanteEgresoItem> data = new List<ComprobanteEgresoItem>();
            data.Add(request);

            return new Response<ComprobanteEgresoItem>(){
                Data=data
            };
        }
        #endregion Put



        #region Delete
        public static Response<ComprobanteEgresoItem> Delete(this ComprobanteEgresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {

            request.CheckId(Operaciones.Destroy);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdComprobanteEgreso.GetLockKey<ComprobanteEgreso>(), Definiciones.LockSeconds))
                {
                    ComprobanteEgresoItem oldData = DAL.FirstOrDefaultById<ComprobanteEgresoItem>(proxy, request.Id);
                    oldData.AssertExists(request.Id);

                    ComprobanteEgreso ce = DAL.GetComprobanteEgreso(proxy, oldData.IdComprobanteEgreso);
                    ce.AssertExists(oldData.IdComprobanteEgreso);
                    ce.CheckPeriodo(proxy);
                                         
                    Egreso egreso= DAL.GetEgresoById(proxy,oldData.IdEgreso);
                    egreso.AssertExists(oldData.IdEgreso);

                    request.ValidateAndThrowHttpError(oldData,ce,egreso,Operaciones.BorraregresoEnCE);

                    ce.Valor-=oldData.Valor;
                    proxy.BeginDbTransaction();
                    ce.ActualizarValor(proxy);
                    request.Borrar(proxy);
                    proxy.CommitDbTransaction();
                }
            });


            List<ComprobanteEgresoItem> data = new List<ComprobanteEgresoItem>();
            data.Add(request);

            return new Response<ComprobanteEgresoItem>(){
                Data=data
            };

        }
        #endregion Delete


        public static void ValidateAndThrowHttpError(this ComprobanteEgresoItem request, string ruleSet)
        {
            ComprobanteEgresoItemValidator av = new ComprobanteEgresoItemValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }

        private  static void ValidateAndThrowHttpError(this ComprobanteEgresoItem request,
                                                       ComprobanteEgreso comprobante,
                                                       Egreso egreso,string ruleSet)
        {
            request.ValidateAndThrowHttpError(new ComprobanteEgresoItem(), comprobante,
                                              egreso, ruleSet);
        }

        private  static void ValidateAndThrowHttpError(this ComprobanteEgresoItem request,
                                                       ComprobanteEgresoItem oldData,
                                                       ComprobanteEgreso comprobante,
                                                       Egreso egreso,string ruleSet)
        {
            EgresoCE ece= new EgresoCE(){
                Egreso=egreso,
                Ce= comprobante,
                Cei= request,
                OldCei= oldData,
            };
            EgresoCEValidator av = new EgresoCEValidator();

            av.ValidateAndThrowHttpError(ece,ruleSet );
        }


        private static void CheckOldAndNew(this ComprobanteEgresoItem request,
                                           ComprobanteEgresoItem oldData,
                                           DALProxy proxy
                                           )
        {
            ComprobanteEgresoItem data = new ComprobanteEgresoItem();
            data.PopulateWith(oldData);

            if(request.Valor!=default(decimal) && request.Valor!=data.Valor)
                data.Valor=request.Valor;

            request.PopulateWith(data);



        }

    }
}

