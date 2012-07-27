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
		public static Response<ComprobanteIngresoItem> Get(this ComprobanteIngresoItem request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
			return factory.Execute(proxy=>
			{
				return new Response<ComprobanteIngresoItem>(){
                	Data=proxy.Get<ComprobanteIngresoItem>(q=> q.IdComprobanteIngreso==request.IdComprobanteIngreso),   	
            	};
			});
		}
		#endregion Get

        #region Post        
        public static Response<ComprobanteIngresoItem> Post(this ComprobanteIngresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.ValidateAndThrowHttpError(Operaciones.Create);

            factory.Execute(proxy=>{

                // bloquear el ComprobanteIngreso parent para evitar actualizaciones....
                using(proxy.AcquireLock(request.IdComprobanteIngreso.GetLockKey<ComprobanteIngreso>(), Definiciones.LockSeconds))
                {

                    var ce = proxy.FirstOrDefaultById<ComprobanteIngreso>(request.IdComprobanteIngreso);
                    ce.AssertExists(request.IdComprobanteIngreso);
                    ce.CheckPeriodo(proxy);
                                         
                    var ingreso= proxy.FirstOrDefaultById<Ingreso>(request.IdIngreso);
                    ingreso.AssertExists(request.IdIngreso);

                    request.ValidateAndThrowHttpError(ce,ingreso,Operaciones.InsertarIngresoEnCI);

                    ce.Valor+=request.Abono;

                    proxy.BeginDbTransaction();
                    ce.ActualizarValor(proxy);
                    proxy.Create(request);
                    proxy.CommitDbTransaction();


					request.DiasCredito= ingreso.DiasCredito;
					request.Numero= ingreso.Numero;
					request.Fecha= ingreso.Fecha;
					request.IdTercero= ingreso.IdTercero;
					request.IdSucursal= ingreso.IdSucursal;
					request.Saldo=ingreso.Saldo;
					request.Valor= ingreso.Valor;
                }
            });

            var data = new List<ComprobanteIngresoItem>();
            data.Add(request);
            
            return new Response<ComprobanteIngresoItem>(){
                Data=data
            };  
            
        }
        #endregion Post


        #region Put
        public static Response<ComprobanteIngresoItem> Put(this ComprobanteIngresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.CheckId(Operaciones.Update);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdComprobanteIngreso.GetLockKey<ComprobanteIngreso>(), Definiciones.LockSeconds))
                {
                    ComprobanteIngresoItem oldData = proxy.FirstOrDefaultById<ComprobanteIngresoItem>(request.Id);
                    oldData.AssertExists(request.Id);

                    var ce = proxy.FirstOrDefaultById<ComprobanteIngreso>(oldData.IdComprobanteIngreso);
                    ce.AssertExists(oldData.IdComprobanteIngreso);
                    ce.CheckPeriodo(proxy);
                                         
                    Ingreso egreso= proxy.FirstOrDefaultById<Ingreso>(oldData.IdIngreso);
                    egreso.AssertExists(oldData.IdIngreso);

                    request.ValidateAndThrowHttpError(oldData, ce,egreso,Operaciones.ActualizarIngresoEnCI);
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


            List<ComprobanteIngresoItem> data = new List<ComprobanteIngresoItem>();
            data.Add(request);

            return new Response<ComprobanteIngresoItem>(){
                Data=data
            };
        }
        #endregion Put


        #region Delete
        public static Response<ComprobanteIngresoItem> Delete(this ComprobanteIngresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {

            request.CheckId(Operaciones.Destroy);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdComprobanteIngreso.GetLockKey<ComprobanteIngreso>(), Definiciones.LockSeconds))
                {
                    ComprobanteIngresoItem oldData = proxy.FirstOrDefaultById<ComprobanteIngresoItem>(request.Id);
                    oldData.AssertExists(request.Id);

                    var ce = proxy.FirstOrDefaultById<ComprobanteIngreso>(oldData.IdComprobanteIngreso);
                    ce.AssertExists(oldData.IdComprobanteIngreso);
                    ce.CheckPeriodo(proxy);
                                         
                    var ingreso= proxy.FirstOrDefaultById<Ingreso>(oldData.IdIngreso);
                    ingreso.AssertExists(oldData.IdIngreso);

                    request.ValidateAndThrowHttpError(oldData,ce,ingreso,Operaciones.BorrarIngresoEnCI);

                    ce.Valor-=oldData.Abono;
                    proxy.BeginDbTransaction();
                    ce.ActualizarValor(proxy);
                    request.Borrar(proxy);
                    proxy.CommitDbTransaction();
                }
            });


            List<ComprobanteIngresoItem> data = new List<ComprobanteIngresoItem>();
            data.Add(request);

            return new Response<ComprobanteIngresoItem>(){
                Data=data
            };

        }
        #endregion Delete


        public static void ValidateAndThrowHttpError(this ComprobanteIngresoItem request, string ruleSet)
        {
            ComprobanteIngresoItemValidator av = new ComprobanteIngresoItemValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }

        static void ValidateAndThrowHttpError(this ComprobanteIngresoItem request,
                                                       ComprobanteIngreso comprobante,
                                                       Ingreso egreso,string ruleSet)
        {
            request.ValidateAndThrowHttpError(new ComprobanteIngresoItem(), comprobante,
                                              egreso, ruleSet);
        }

        static void ValidateAndThrowHttpError(this ComprobanteIngresoItem request,
                                                       ComprobanteIngresoItem oldData,
                                                       ComprobanteIngreso comprobante,
                                                       Ingreso egreso,string ruleSet)
        {
            IngresoCI ece= new IngresoCI(){
                Ingreso=egreso,
                Ce= comprobante,
                Cei= request,
                OldCei= oldData,
            };
            IngresoCIValidator av = new IngresoCIValidator();

            av.ValidateAndThrowHttpError(ece,ruleSet );
        }


        static void CheckOldAndNew(this ComprobanteIngresoItem request,
                                           ComprobanteIngresoItem oldData,
                                           DALProxy proxy
                                           )
        {
            ComprobanteIngresoItem data = new ComprobanteIngresoItem();
            data.PopulateWith(oldData);

            if(request.Abono!=default(decimal) && request.Abono!=data.Abono)
                data.Abono=request.Abono;

            request.PopulateWith(data);
        }

    }
}

