using System.Collections.Generic;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;

namespace Aicl.Galapago.BusinessLogic
{
    public static partial class BL
    {
		#region Get
		public static Response<ComprobanteIngresoRetencion> Get(this ComprobanteIngresoRetencion request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
			return factory.Execute(proxy=>
			{
				return new Response<ComprobanteIngresoRetencion>(){
                	Data=proxy.Get<ComprobanteIngresoRetencion>(q=> q.IdComprobanteIngreso ==request.IdComprobanteIngreso),   	
            	};
			});
		}
		#endregion Get

        #region Post        
        public static Response<ComprobanteIngresoRetencion> Post(this ComprobanteIngresoRetencion request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.ValidateAndThrowHttpError(Operaciones.Create);

            factory.Execute(proxy=>{

                var pi = request.CheckPresupuestoItem(proxy);

                // bloquear el ComprobanteIngreso parent para evitar actualizaciones....
                using(proxy.AcquireLock(request.IdComprobanteIngreso.GetLockKey<ComprobanteIngreso>(), Definiciones.LockSeconds))
                {

                    ComprobanteIngreso ce = proxy.FirstOrDefaultById<ComprobanteIngreso>(request.IdComprobanteIngreso);
                    ce.AssertExists(request.IdComprobanteIngreso);
                    ce.CheckPeriodo(proxy);
                                         
                    ComprobanteIngresoItem cei= proxy.FirstOrDefaultById<ComprobanteIngresoItem>(request.IdComprobanteIngresoItem);
                    cei.AssertExists(request.IdComprobanteIngresoItem);

                    Ingreso egreso= proxy.FirstOrDefaultById<Ingreso>(cei.IdIngreso);
                    egreso.AssertExists(cei.IdIngreso);

                    request.ValidateAndThrowHttpError(ce, cei, egreso,
                                                      pi,
                                                      Operaciones.InsertarRetencionEnCE);
                    ce.Valor-= request.Valor;
                    proxy.BeginDbTransaction();
                    ce.ActualizarValor(proxy);
                    proxy.Create(request);
                    proxy.CommitDbTransaction();
                }
            });

            List<ComprobanteIngresoRetencion> data = new List<ComprobanteIngresoRetencion>();
            data.Add(request);
            
            return new Response<ComprobanteIngresoRetencion>(){
                Data=data
            };  
        }
        #endregion Post

        #region Delete
        public static Response<ComprobanteIngresoRetencion> Delete(this ComprobanteIngresoRetencion request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.CheckId(Operaciones.Destroy);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdComprobanteIngreso.GetLockKey<ComprobanteIngreso>(), Definiciones.LockSeconds))
                {
                    ComprobanteIngresoRetencion oldData = 
                        proxy.FirstOrDefaultById<ComprobanteIngresoRetencion>(request.Id);
                    oldData.AssertExists(request.Id);

                    ComprobanteIngresoItem cei= proxy.FirstOrDefaultById<ComprobanteIngresoItem>(oldData.IdComprobanteIngresoItem);
                    cei.AssertExists(request.IdComprobanteIngresoItem);

                    ComprobanteIngreso ce = proxy.FirstOrDefaultById<ComprobanteIngreso>(oldData.IdComprobanteIngreso);
                    ce.AssertExists(oldData.IdComprobanteIngreso);
                    ce.CheckPeriodo(proxy);
                                         
                    Ingreso egreso= proxy.FirstOrDefaultById<Ingreso>(cei.IdIngreso);
                    egreso.AssertExists(cei.IdIngreso);

                    request.ValidateAndThrowHttpError(oldData,ce,
                                                      cei,
                                                      egreso,
                                                      null,
                                                      Operaciones.BorrarRetencionEnCE);
                    ce.Valor+=oldData.Valor;
                    proxy.BeginDbTransaction();
                    ce.ActualizarValor(proxy);
                    proxy.Delete<ComprobanteIngresoRetencion>(q=>q.Id==request.Id);
                    proxy.CommitDbTransaction();
                }
            });

            List<ComprobanteIngresoRetencion> data = new List<ComprobanteIngresoRetencion>();
            data.Add(request);

            return new Response<ComprobanteIngresoRetencion>(){
                Data=data
            };
        }
        #endregion Delete

        public static void ValidateAndThrowHttpError(this ComprobanteIngresoRetencion request, string ruleSet)
        {
            ComprobanteIngresoRetencionValidator av = new ComprobanteIngresoRetencionValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }

        public static void ValidateAndThrowHttpError(this ComprobanteIngresoRetencion request, 
                                                     ComprobanteIngreso ce,
                                                     ComprobanteIngresoItem cei,
                                                     Ingreso egreso,
                                                     PresupuestoItem pi,
                                                     string ruleSet)
        {
            request.ValidateAndThrowHttpError(new ComprobanteIngresoRetencion(),
                                              ce, cei, egreso, pi, ruleSet);
        }

        public static void ValidateAndThrowHttpError(this ComprobanteIngresoRetencion request, 
                                                     ComprobanteIngresoRetencion oldData,
                                                     ComprobanteIngreso ce,
                                                     ComprobanteIngresoItem cei,
                                                     Ingreso egreso,
                                                     PresupuestoItem pi,
                                                     string ruleSet)
        {
            IngresoCIRet ret = new IngresoCIRet(){
                Ingreso= egreso,
                Cei=cei,
                Ce=ce,
                OldRet= oldData,
                Ret= request,
                Pi=pi
            };

            IngresoCIRetValidator av = new IngresoCIRetValidator();
            av.ValidateAndThrowHttpError(ret, ruleSet);
        }

        static PresupuestoItem CheckPresupuestoItem(this ComprobanteIngresoRetencion item, DALProxy proxy)
        {
            PresupuestoItem pi = DAL.GetPresupuestoItem(proxy, item.IdPresupuestoItem);
            pi.AssertExists(item.IdPresupuestoItem);
            return pi;
        }
    }
}

