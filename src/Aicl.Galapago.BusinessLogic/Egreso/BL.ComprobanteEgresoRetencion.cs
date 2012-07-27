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
		public static Response<ComprobanteEgresoRetencion> Get(this ComprobanteEgresoRetencion request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
			return factory.Execute(proxy=>
			{
				return new Response<ComprobanteEgresoRetencion>(){
                	Data=proxy.Get<ComprobanteEgresoRetencion>(q=> q.IdComprobanteEgreso ==request.IdComprobanteEgreso),   	
            	};
			});
		}
		#endregion Get

        #region Post        
        public static Response<ComprobanteEgresoRetencion> Post(this ComprobanteEgresoRetencion request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.ValidateAndThrowHttpError(Operaciones.Create);

            factory.Execute(proxy=>{

                var pi = request.CheckPresupuestoItem(proxy);

                // bloquear el ComprobanteEgreso parent para evitar actualizaciones....
                using(proxy.AcquireLock(request.IdComprobanteEgreso.GetLockKey<ComprobanteEgreso>(), Definiciones.LockSeconds))
                {
                    ComprobanteEgreso ce = DAL.GetComprobanteEgreso(proxy, request.IdComprobanteEgreso);
                    ce.AssertExists(request.IdComprobanteEgreso);
                    ce.CheckPeriodo(proxy);
                                         
                    ComprobanteEgresoItem cei= proxy.FirstOrDefaultById<ComprobanteEgresoItem>(request.IdComprobanteEgresoItem);
                    cei.AssertExists(request.IdComprobanteEgresoItem);

                    Egreso egreso= DAL.GetEgresoById(proxy,cei.IdEgreso);
                    egreso.AssertExists(cei.IdEgreso);

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

            List<ComprobanteEgresoRetencion> data = new List<ComprobanteEgresoRetencion>();
            data.Add(request);
            
            return new Response<ComprobanteEgresoRetencion>(){
                Data=data
            };  
        }
        #endregion Post

        #region Delete
        public static Response<ComprobanteEgresoRetencion> Delete(this ComprobanteEgresoRetencion request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
			request.CheckId(Operaciones.Destroy);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdComprobanteEgreso.GetLockKey<ComprobanteEgreso>(), Definiciones.LockSeconds))
                {
                    ComprobanteEgresoRetencion oldData = 
                        proxy.FirstOrDefaultById<ComprobanteEgresoRetencion>(request.Id);
                    oldData.AssertExists(request.Id);

                    ComprobanteEgresoItem cei= proxy.FirstOrDefaultById<ComprobanteEgresoItem>(oldData.IdComprobanteEgresoItem);
                    cei.AssertExists(request.IdComprobanteEgresoItem);

                    ComprobanteEgreso ce = DAL.GetComprobanteEgreso(proxy, oldData.IdComprobanteEgreso);
                    ce.AssertExists(oldData.IdComprobanteEgreso);
                    ce.CheckPeriodo(proxy);
                                         
                    Egreso egreso= DAL.GetEgresoById(proxy,cei.IdEgreso);
                    egreso.AssertExists(cei.IdEgreso);

                    request.ValidateAndThrowHttpError(oldData,ce,
                                                      cei,
                                                      egreso,
                                                      null,
                                                      Operaciones.BorrarRetencionEnCE);
					ce.Valor+=oldData.Valor;
                    proxy.BeginDbTransaction();
                    ce.ActualizarValor(proxy);
                    proxy.Delete<ComprobanteEgresoRetencion>(q=>q.Id==request.Id);
                    proxy.CommitDbTransaction();
                }
            });

            List<ComprobanteEgresoRetencion> data = new List<ComprobanteEgresoRetencion>();
            data.Add(request);

            return new Response<ComprobanteEgresoRetencion>(){
                Data=data
            };
        }
        #endregion Delete


        public static void ValidateAndThrowHttpError(this ComprobanteEgresoRetencion request, string ruleSet)
        {
            ComprobanteEgresoRetencionValidator av = new ComprobanteEgresoRetencionValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }

        public static void ValidateAndThrowHttpError(this ComprobanteEgresoRetencion request, 
                                                     ComprobanteEgreso ce,
                                                     ComprobanteEgresoItem cei,
                                                     Egreso egreso,
                                                     PresupuestoItem pi,
                                                     string ruleSet)
        {
            request.ValidateAndThrowHttpError(new ComprobanteEgresoRetencion(),
                                              ce, cei, egreso, pi, ruleSet);
        }

        public static void ValidateAndThrowHttpError(this ComprobanteEgresoRetencion request, 
                                                     ComprobanteEgresoRetencion oldData,
                                                     ComprobanteEgreso ce,
                                                     ComprobanteEgresoItem cei,
                                                     Egreso egreso,
                                                     PresupuestoItem pi,
                                                     string ruleSet)
        {
            EgresoCERet ret = new EgresoCERet(){
                Egreso= egreso,
                Cei=cei,
                Ce=ce,
                OldRet= oldData,
                Ret= request,
                Pi=pi
            };

            EgresoCERetValidator av = new EgresoCERetValidator();
            av.ValidateAndThrowHttpError(ret, ruleSet);
        }

        static PresupuestoItem CheckPresupuestoItem(this ComprobanteEgresoRetencion item, DALProxy proxy)
        {
            PresupuestoItem pi = DAL.GetPresupuestoItem(proxy, item.IdPresupuestoItem);
            pi.AssertExists(item.IdPresupuestoItem);
            return pi;
        }
    }
}