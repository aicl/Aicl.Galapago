using System.Collections.Generic;
using ServiceStack.Common;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;

namespace Aicl.Galapago.BusinessLogic
{
    public static partial class  BL
    {
		#region Get
		public static Response<ComprobanteEgresoItem> Get(this ComprobanteEgresoItem request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
			return factory.Execute(proxy=>
			{
				return new Response<ComprobanteEgresoItem>(){
                	Data=proxy.Get<ComprobanteEgresoItem>(q=> q.IdComprobanteEgreso==request.IdComprobanteEgreso),   	
            	};
			});
		}
		#endregion Get

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

                    ce.Valor+=request.Abono;

                    proxy.BeginDbTransaction();
                    ce.ActualizarValor(proxy);
                    proxy.Create(request);
                    proxy.CommitDbTransaction();

					request.Documento=egreso.Documento;
					request.DiasCredito= egreso.DiasCredito;
					request.Numero= egreso.Numero;
					request.Fecha= egreso.Fecha;
					request.IdTercero= egreso.IdTercero;
					request.IdSucursal= egreso.IdSucursal;
					request.Saldo=egreso.Saldo;
					request.Valor= egreso.Valor;
                }
            });

            var data = new List<ComprobanteEgresoItem>();
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
                    ComprobanteEgresoItem oldData = proxy.FirstOrDefaultById<ComprobanteEgresoItem>(request.Id);
                    oldData.AssertExists(request.Id);

                    ComprobanteEgreso ce = DAL.GetComprobanteEgreso(proxy, oldData.IdComprobanteEgreso);
                    ce.AssertExists(oldData.IdComprobanteEgreso);
                    ce.CheckPeriodo(proxy);
                                         
                    Egreso egreso= DAL.GetEgresoById(proxy,oldData.IdEgreso);
                    egreso.AssertExists(oldData.IdEgreso);

                    request.ValidateAndThrowHttpError(oldData, ce,egreso,Operaciones.ActualizarEgresoEnCE);
                    request.CheckOldAndNew(oldData,proxy);
                                    
                    if( request.Abono!=oldData.Abono)
                    {
                        ce.Valor+=request.Abono-oldData.Abono;
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
                    ComprobanteEgresoItem oldData = proxy.FirstOrDefaultById<ComprobanteEgresoItem>(request.Id);
                    oldData.AssertExists(request.Id);

                    ComprobanteEgreso ce = DAL.GetComprobanteEgreso(proxy, oldData.IdComprobanteEgreso);
                    ce.AssertExists(oldData.IdComprobanteEgreso);
                    ce.CheckPeriodo(proxy);
                                         
                    Egreso egreso= DAL.GetEgresoById(proxy,oldData.IdEgreso);
                    egreso.AssertExists(oldData.IdEgreso);

                    request.ValidateAndThrowHttpError(oldData,ce,egreso,Operaciones.BorrarEgresoEnCE);

                    ce.Valor-=oldData.Abono;
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

        static void ValidateAndThrowHttpError(this ComprobanteEgresoItem request,
                                                       ComprobanteEgreso comprobante,
                                                       Egreso egreso,string ruleSet)
        {
            request.ValidateAndThrowHttpError(new ComprobanteEgresoItem(), comprobante,
                                              egreso, ruleSet);
        }

        static void ValidateAndThrowHttpError(this ComprobanteEgresoItem request,
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


        static void CheckOldAndNew(this ComprobanteEgresoItem request,
                                           ComprobanteEgresoItem oldData,
                                           DALProxy proxy
                                           )
        {
            ComprobanteEgresoItem data = new ComprobanteEgresoItem();
            data.PopulateWith(oldData);

            if(request.Abono!=default(decimal) && request.Abono!=data.Abono)
                data.Abono=request.Abono;

            request.PopulateWith(data);
        }

    }
}

