using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
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
        public static Response<IngresoItem> Get(this IngresoItem request,
                                            Factory factory,
                                            IHttpRequest httpRequest)
        {
            return factory.Execute(proxy=>{
            
				var visitor = ReadExtensions.CreateExpression<IngresoItem>();
                 
				visitor.Where(r=>r.IdIngreso==request.IdIngreso).OrderBy(r=>r.TipoPartida);

				return new Response<IngresoItem>(){
	                Data= proxy.Get(visitor)
	            };
            });
        }
        #endregion Get

        #region Post        
        public static Response<IngresoItem> Post(this IngresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {


            request.ValidateAndThrowHttpError(Operaciones.Create);

            factory.Execute(proxy=>{

                PresupuestoItem pi= Check1(proxy,request, int.Parse(authSession.UserAuthId));

                // bloquear el Ingreso parent para evitar actualizaciones....
                using(proxy.AcquireLock(request.IdIngreso.GetLockKey<Ingreso>(), Definiciones.LockSeconds))
                {
                    Ingreso ingreso=proxy.FirstOrDefaultById<Ingreso>(request.IdIngreso);
                    ingreso.AssertExists(request.IdIngreso);
                    ingreso.ValidateAndThrowHttpError(Operaciones.Update);
                    ingreso.CheckPeriodo(proxy);
                    request.CheckCentro(proxy, ingreso.IdSucursal,int.Parse(authSession.UserAuthId));

                    CodigoDocumento cd = proxy.GetCodigoDocumento(ingreso.CodigoDocumento);
                    cd.AssertExists(ingreso.CodigoDocumento); 
                    cd.AssertEstaActivo();

                    if(request.TipoPartida==1)
                    {
                        cd.CheckDebitos(pi.Codigo);
                        ingreso.Valor= ingreso.Valor+request.Valor;
                        ingreso.Saldo=ingreso.Saldo+request.Valor;
                    }
                    else
                    {
                        cd.CheckCreditos(pi.Codigo);
                        ingreso.Saldo=ingreso.Saldo-request.Valor;
                    }

                    proxy.BeginDbTransaction();
                    ingreso.ActualizarValorSaldo(proxy);
                    proxy.Create(request);
                    proxy.CommitDbTransaction();


                }
            });

            List<IngresoItem> data = new List<IngresoItem>();
            data.Add(request);
            
            return new Response<IngresoItem>(){
                Data=data
            };  
            
        }
        #endregion Post

        #region Put
        public static Response<IngresoItem> Put(this IngresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            

            request.CheckId(Operaciones.Update);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdIngreso.GetLockKey<Ingreso>(), Definiciones.LockSeconds))
                {
                    IngresoItem oldData = proxy.FirstOrDefaultById<IngresoItem>(request.Id);
                    oldData.AssertExists(request.Id);

                    Ingreso ingreso=  proxy.FirstOrDefaultById<Ingreso>( oldData.IdIngreso);
                    ingreso.AssertExists(request.IdIngreso);

                    CheckOldAndNew(ingreso,request,oldData, proxy, int.Parse(authSession.UserAuthId));
                    PresupuestoItem pi= Check1(proxy, request, int.Parse(authSession.UserAuthId));

                    CodigoDocumento cd = proxy.GetCodigoDocumento(ingreso.CodigoDocumento);
                    cd.AssertExists(ingreso.CodigoDocumento);
                    cd.AssertEstaActivo();


                    if(request.TipoPartida!=oldData.TipoPartida || request.Valor!=oldData.Valor)
                    {
                        if(oldData.TipoPartida==1)
                        {
                            ingreso.Valor= ingreso.Valor-oldData.Valor;
                            ingreso.Saldo= ingreso.Saldo-oldData.Valor;
                        }
                        else
                            ingreso.Saldo=ingreso.Saldo+oldData.Valor;

                        if(request.TipoPartida==1)
                        {
                            cd.CheckDebitos(pi.Codigo);
                            ingreso.Valor= ingreso.Valor+request.Valor;
                            ingreso.Saldo=ingreso.Saldo+request.Valor;
                        }
                        else
                        {
                            cd.CheckCreditos(pi.Codigo);
                            ingreso.Saldo=ingreso.Saldo-request.Valor;
                        }
    
                        proxy.BeginDbTransaction();
                        ingreso.ActualizarValorSaldo(proxy);
						proxy.Update(request);
                        proxy.CommitDbTransaction();
                    }
                    else
						proxy.Update(request);
                }
            });


            List<IngresoItem> data = new List<IngresoItem>();
            data.Add(request);

            return new Response<IngresoItem>(){
                Data=data
            };
        }
        #endregion Put

        #region Delete
        public static Response<IngresoItem> Delete(this IngresoItem request,
                                            Factory factory,
                                            IAuthSession authSession)
        {
            request.CheckId(Operaciones.Destroy);

            factory.Execute(proxy=>{
                using (proxy.AcquireLock(request.IdIngreso.GetLockKey<Ingreso>(), Definiciones.LockSeconds))
                {
                    IngresoItem oldData = proxy.FirstOrDefaultById<IngresoItem>(request.Id);
                    oldData.AssertExists(request.Id);

                    Ingreso egreso= proxy.FirstOrDefaultById<Ingreso>(oldData.IdIngreso);
                    egreso.AssertExists(request.IdIngreso);

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
					proxy.Delete<EgresoItem>(q=>q.Id==request.Id);
                    proxy.CommitDbTransaction();
                }
            });

            List<IngresoItem> data = new List<IngresoItem>();
            data.Add(request);

            return new Response<IngresoItem>(){
                Data=data
            };
        }
        #endregion Destroy

        static void ValidateAndThrowHttpError(this IngresoItem  request, string ruleSet)
        {
            IngresoItemValidator av = new IngresoItemValidator();
            av.ValidateAndThrowHttpError(request, ruleSet);
        }

        static PresupuestoItem Check1(DALProxy proxy, IngresoItem request, int idUsuario)
        {
            PresupuestoItem pi = CheckPresupuestoItem(proxy,request);

            Presupuesto pr =DAL.GetPresupuestoById(proxy,pi.IdPresupuesto);
            pr.AssertExists(pi.IdPresupuesto);

            Centro centro= proxy.FirstOrDefaultById<Centro>(request.IdCentro);
            centro.AssertExists(request.IdCentro);

        
            IngresoItemAlCrear ei = new IngresoItemAlCrear(){
                NewItem=request,
                Prs= pr,
                Pi= pi,
                CentroItem= centro
            };

            IngresoItemAlCrearValidador eiv = new IngresoItemAlCrearValidador();
            eiv.ValidateAndThrowHttpError(ei, IngresoItemAlCrear.Regla1);

            pi.CheckUsuarioGiradora(proxy, idUsuario, null);


            request.CodigoItem= pi.Codigo;
            request.NombreItem= pi.Nombre;
            request.NombreCentro=centro.Nombre;
            
            return pi;
        }


        static void CheckOldAndNew(Ingreso egreso, IngresoItem request, IngresoItem oldData,
                                           DALProxy proxy,
                                           int idUsuario)
        {
            egreso.ValidateAndThrowHttpError(Operaciones.Update);
            egreso.CheckPeriodo(proxy);

            oldData.CheckCentro(proxy,egreso.IdSucursal, idUsuario);

            IngresoItem data = new IngresoItem();
            data.PopulateWith(oldData);

            if(request.IdCentro!=default(int) && request.IdCentro!=data.IdCentro)
            {
                data.IdCentro=request.IdCentro;
                data.CheckCentro(proxy, egreso.IdSucursal, idUsuario);
            }

            if(request.IdPresupuestoItem!=default(int) && request.IdPresupuestoItem!= data.IdPresupuestoItem)
                data.IdPresupuestoItem= request.IdPresupuestoItem;

            if(request.TipoPartida!=default(short) && request.TipoPartida!=data.TipoPartida)
                data.TipoPartida=request.TipoPartida;

            if(request.Valor!=default(decimal) && request.Valor!=data.Valor)
                data.Valor=request.Valor;

            request.PopulateWith(data);
        }   

        static PresupuestoItem CheckPresupuestoItem(DALProxy proxy,IngresoItem egresoItem)
        {
            PresupuestoItem pi = DAL.GetPresupuestoItem(proxy, egresoItem.IdPresupuestoItem);
            pi.AssertExists(egresoItem.IdPresupuestoItem);
            return pi;
        }

    }
}

