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
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;

namespace Aicl.Galapago.DataAccess
{
	public static class AsientoItemExtensiones
	{
		
		#region post
		public static Response<AsientoItem> Post(this AsientoItem request,
		                                     Factory factory,
		                                     IRequestContext requestContext,
		                                     bool checkCentro=true,
		                                     bool checkPeriodo=false,
		                                     bool revisarCuenta=true,    
		                                     Action<IDbCommand,AsientoItem> runBeforePostDbCommandsFn=null,
		                                     Action<IDbCommand,AsientoItem> runAfterPostDbCommandsFn=null)
		{
						
			request.ValidateAndThrowHttpError(Operaciones.Create);
						
			var httpRequest = requestContext.Get<IHttpRequest>();
						
			httpRequest.RedisClientExec(redisClient=>
			{
				
				if(revisarCuenta){
				
					Cuenta cuenta= redisClient.GetFromCache(request.IdCuenta.GetCacheKey<Cuenta>(),()=>
					{
						return factory.FirstOrDefaultById<Cuenta>(request.IdCuenta);				
					},
					TimeSpan.FromDays(Definiciones.DiasEnCache) );
								
					CuentaExtensiones.AssertExists(cuenta,request.IdCuenta);
					
					cuenta.ValidateAndThrowHttpError(Definiciones.CuentaDetalleActiva);
					
					if(cuenta.UsaTercero)
					{	
						request.ValidateAndThrowHttpError(Definiciones.UsaTercero);
						
						Tercero tercero= redisClient.GetFromCache(request.IdTercero.Value.GetCacheKey<Tercero>(),()=>
						{
							return factory.FirstOrDefaultById<Tercero>(request.IdTercero.Value);				
						},
						TimeSpan.FromDays(Definiciones.DiasEnCache) );
									
						TerceroExtensiones.AssertExists(tercero,request.IdTercero.Value);
						
						tercero.ValidateAndThrowHttpError(Definiciones.RegistroActivo);
					}
					else request.IdTercero=null;
				}				
			
				
				string lockKey= request.IdAsiento.GetLockKey<Asiento>(); 	
				
				Asiento asiento= default(Asiento);
				List<AsientoItem> items= new List<AsientoItem>();
				
				using (redisClient.AcquireLock(lockKey, TimeSpan.FromSeconds(Definiciones.LockSeconds)))
				{

					factory.Post<AsientoItem>(request,
					runBeforePostDbCommandsFn:(dbCmd)=> 
					{	
						asiento= redisClient.GetFromCache(request.IdAsiento.GetCacheKey<Asiento>() ,
						()=>
						{
							return dbCmd.FirstOrDefault<Asiento>(q=>q.Id==request.IdAsiento);
						},
						TimeSpan.FromDays(Definiciones.DiasEnCache));
						
						AsientoExtensiones.AssertExists(asiento,request.Id);												
						asiento.ValidateAndThrowHttpError(Operaciones.Update);
											
						if(checkCentro) request.CheckCentro(asiento.IdSucursal, factory, httpRequest); 
						if(checkPeriodo) asiento.CheckPeriodo(factory, httpRequest);
						
						
						items= redisClient.GetFromCache(asiento.GetCacheKey(typeof(AsientoItem)),
						()=>
						{
							return dbCmd.Select<AsientoItem>(q=> q.IdAsiento==asiento.Id);
						});
						
						if(runBeforePostDbCommandsFn!=null) runBeforePostDbCommandsFn(dbCmd, request);
						
					},
					runAfterPostDbCommandsFn:(dbCmd,result)=>
					{
						items.Add(result);
						asiento.Debitos = items.Where(r=>r.TipoPartida==1).Sum(r=> r.Valor);
						asiento.Creditos = items.Where(r=>r.TipoPartida==2).Sum(r=> r.Valor);
						SqlExpressionVisitor<Asiento> ev = OrmLiteConfig.DialectProvider.ExpressionVisitor<Asiento>();
						ev.Update( f=> new { f.Debitos, f.Creditos }).Where( r=> r.Id==request.IdAsiento);                  
						dbCmd.Update(asiento, ev);
						if(runAfterPostDbCommandsFn!=null) runAfterPostDbCommandsFn(dbCmd, result);
						
						using (var trans = redisClient.CreateTransaction())
						{
							trans.QueueCommand(r =>{
								r.Set(asiento.GetCacheKey(), asiento, TimeSpan.FromDays(Definiciones.DiasEnCache));
							});	
							
							trans.QueueCommand(r =>{
								r.Set(asiento.GetCacheKey(typeof(AsientoItem)), items , TimeSpan.FromDays(Definiciones.DiasEnCache));
							});										
								
							trans.Commit();
						}	
					});
				}
			});
						
			
			List<AsientoItem> data = new List<AsientoItem>();
			data.Add(request);
			
			return new Response<AsientoItem>(){
				Data=data
			};	
		}
		#endregion post

		
		#region put	
		public static Response<AsientoItem> Put(this AsientoItem request,
		                                     Factory factory,
		                                     IRequestContext requestContext,
		                                     bool checkCentro=true,
		                                     bool checkPeriodo=false,
		                                     bool revisarCuenta=true,    
		                                     Action<IDbCommand,AsientoItem> runBeforePutDbCommandsFn=null,
		                                     Action<IDbCommand,AsientoItem> runAfterPutDbCommandsFn=null)
		{

			request.CheckId(Operaciones.Update);
						
			var httpRequest = requestContext.Get<IHttpRequest>();
						
			httpRequest.RedisClientExec(redisClient=>
			{
				//  bloquearlo el asiento , traerlo compara los Ids request.IdAsiento == asiento.Id
								
				string lockKey= request.IdAsiento.GetLockKey<Asiento>(); 	
				
				Asiento asiento= default(Asiento);
				List<AsientoItem> items= new List<AsientoItem>();
				
				using (redisClient.AcquireLock(lockKey, TimeSpan.FromSeconds(Definiciones.LockSeconds)))
				{
					SqlExpressionVisitor<AsientoItem> ev = OrmLiteConfig.DialectProvider.ExpressionVisitor<AsientoItem>();
					ev.Update(f=> new { f.IdCentro, f.IdCuenta, f.IdTercero, f.Valor, f.TipoPartida }).Where(f=>f.Id==request.Id);                  	
				
					factory.Put(request, ev,
					runBeforePutDbCommandsFn:(dbCmd)=>  
					{	
						asiento= redisClient.GetFromCache(request.IdAsiento.GetCacheKey<Asiento>() ,
						()=>
						{
							return dbCmd.FirstOrDefault<Asiento>(q=>q.Id==request.IdAsiento);
						},
						TimeSpan.FromDays(Definiciones.DiasEnCache));
						
						AsientoExtensiones.AssertExists(asiento,request.Id);												
						asiento.ValidateAndThrowHttpError(Operaciones.Update);
						request.CheckIsChild(asiento);						
						if(checkPeriodo) asiento.CheckPeriodo(factory, httpRequest);
						
						items= redisClient.GetFromCache(asiento.GetCacheKey(typeof(AsientoItem)),
						()=>
						{
							return dbCmd.Select<AsientoItem>(q=> q.IdAsiento==asiento.Id);
						});
						
						var data= items.FirstOrDefault(r=>r.Id==request.Id);
						AssertExists(data, request.Id);
						items.Remove(data);
						
						if(request.IdCentro!=default(int))
						{
							data.IdCentro= request.IdCentro;
						}
						
						if(request.IdCuenta!=default(int))
						{
							if(revisarCuenta)
							{
								Cuenta cuenta= redisClient.GetFromCache(request.IdCuenta.GetCacheKey<Cuenta>(),()=>
								{
									return factory.FirstOrDefaultById<Cuenta>(request.IdCuenta);				
								},
								TimeSpan.FromDays(Definiciones.DiasEnCache) );
											
								CuentaExtensiones.AssertExists(cuenta,request.IdCuenta);
								
								cuenta.ValidateAndThrowHttpError(Definiciones.CuentaDetalleActiva);
								
								if(cuenta.UsaTercero)
								{
									request.ValidateAndThrowHttpError(Definiciones.UsaTercero);
									
									if(request.IdTercero.HasValue && request.IdTercero.Value !=default(int))
									{
										Tercero tercero= redisClient.GetFromCache(request.IdTercero.Value.GetCacheKey<Tercero>(),()=>
										{
											return factory.FirstOrDefaultById<Tercero>(request.IdTercero.Value);				
										},
										TimeSpan.FromDays(Definiciones.DiasEnCache) );
													
										TerceroExtensiones.AssertExists(tercero,request.IdTercero.Value);
										
										tercero.ValidateAndThrowHttpError(Definiciones.RegistroActivo);
										data.IdTercero= request.IdTercero;
									}
								}
								else data.IdTercero=null;
							}
							data.IdCuenta=request.IdCuenta;		
						}
						
						data.Valor= request.Valor;
						data.TipoPartida= request.TipoPartida;
						data.ValidateAndThrowHttpError(Operaciones.Update);
						if(checkCentro) data.CheckCentro(asiento.IdSucursal,factory,httpRequest);
						request.PopulateWith(data);
												
						if(runBeforePutDbCommandsFn!=null) runBeforePutDbCommandsFn(dbCmd, request);					
	
					},
					runAfterPutDbCommandsFn: (dbCmd)=> 
					{
						//var item= items.FindIndex(r=> r.Id== request.Id);					
						//items[item]= request;
						items.Add(request);
						asiento.Debitos = items.Where(r=>r.TipoPartida==1).Sum(r=> r.Valor);
						asiento.Creditos = items.Where(r=>r.TipoPartida==2).Sum(r=> r.Valor);
						SqlExpressionVisitor<Asiento> ev2 = OrmLiteConfig.DialectProvider.ExpressionVisitor<Asiento>();
						ev2.Update( f=> new { f.Debitos, f.Creditos }).Where( r=> r.Id==request.IdAsiento);                  
						dbCmd.Update(asiento, ev2);
						if(runAfterPutDbCommandsFn!=null) runAfterPutDbCommandsFn(dbCmd, request);
						
						using (var trans = redisClient.CreateTransaction())
						{
							trans.QueueCommand(r =>{
								r.Set(asiento.GetCacheKey(), asiento, TimeSpan.FromDays(Definiciones.DiasEnCache));
							});	
							
							trans.QueueCommand(r =>{
								r.Set(asiento.GetCacheKey(typeof(AsientoItem)), items , TimeSpan.FromDays(Definiciones.DiasEnCache));
							});										
								
							trans.Commit();
						}	
						
					});					
					
				}
				
			});
						
			List<AsientoItem> result = new List<AsientoItem>();
			result.Add(request);
			
			return new Response<AsientoItem>(){
				Data=result
			};
		}
		#endregion put

		#region Delete
		public static Response<AsientoItem> Delete(this AsientoItem request,
		                                     Factory factory,
		                                     IRequestContext requestContext,
		                                     bool checkCentro=true,
		                                     bool checkPeriodo=false,
		                                     bool revisarCuenta=true,    
		                                     Action<IDbCommand,AsientoItem> runBeforeDeleteDbCommandsFn=null,
		                                     Action<IDbCommand,AsientoItem> runAfterDeleteDbCommandsFn=null)
		{

			request.CheckId(Operaciones.Destroy);
						
			var httpRequest = requestContext.Get<IHttpRequest>();
						
			httpRequest.RedisClientExec(redisClient=>
			{								
				string lockKey= request.IdAsiento.GetLockKey<Asiento>(); 	
				
				Asiento asiento= default(Asiento);
				List<AsientoItem> items= new List<AsientoItem>();
				
				using (redisClient.AcquireLock(lockKey, TimeSpan.FromSeconds(Definiciones.LockSeconds)))
				{
					
					factory.Delete(request, 
					runBeforeDeleteDbCommandsFn:(dbCmd)=>  
					{	
						asiento= redisClient.GetFromCache(request.IdAsiento.GetCacheKey<Asiento>() ,
						()=>
						{
							return dbCmd.FirstOrDefault<Asiento>(q=>q.Id==request.IdAsiento);
						},
						TimeSpan.FromDays(Definiciones.DiasEnCache));
						
						AsientoExtensiones.AssertExists(asiento,request.Id);												
						asiento.ValidateAndThrowHttpError(Operaciones.Update);
						request.CheckIsChild(asiento);						
						
						if(checkPeriodo) asiento.CheckPeriodo(factory, httpRequest);
						
						items= redisClient.GetFromCache(asiento.GetCacheKey(typeof(AsientoItem)),
						()=>
						{
							return dbCmd.Select<AsientoItem>(q=> q.IdAsiento==asiento.Id);
						});
						
						var id = request.Id;
						request= items.FirstOrDefault(r=>r.Id==id);
						AssertExists(request, id);
						items.Remove(request);
								
						if(checkCentro) request.CheckCentro(asiento.IdSucursal, factory, httpRequest);
												
						if(runBeforeDeleteDbCommandsFn!=null) runBeforeDeleteDbCommandsFn(dbCmd, request);					
	
					},
					runAfterDeleteDbCommandsFn: (dbCmd)=> 
					{
						asiento.Debitos = items.Where(r=>r.TipoPartida==1).Sum(r=> r.Valor);
						asiento.Creditos = items.Where(r=>r.TipoPartida==2).Sum(r=> r.Valor);
						SqlExpressionVisitor<Asiento> ev = OrmLiteConfig.DialectProvider.ExpressionVisitor<Asiento>();
						ev.Update( f=> new { f.Debitos, f.Creditos }).Where( r=> r.Id==request.IdAsiento);                  
						dbCmd.Update(asiento, ev);
						if(runAfterDeleteDbCommandsFn!=null) runAfterDeleteDbCommandsFn(dbCmd, request);
						
						using (var trans = redisClient.CreateTransaction())
						{
							trans.QueueCommand(r =>{
								r.Set(asiento.GetCacheKey(), asiento, TimeSpan.FromDays(Definiciones.DiasEnCache));
							});	
							
							trans.QueueCommand(r =>{
								r.Set(asiento.GetCacheKey(typeof(AsientoItem)), items , TimeSpan.FromDays(Definiciones.DiasEnCache));
							});										
								
							trans.Commit();
						}	
						
					});					
					
				}
				
			});
			
			return new Response<AsientoItem>(){};
		}
		
		#endregion Delete
		
		
		public static void ValidateAndThrowHttpError(this AsientoItem request, string ruleSet)
		{
			AsientoItemValidator av = new AsientoItemValidator();
			av.ValidateAndThrowHttpError(request, ruleSet);
		}
		
		public static void AssertExists(AsientoItem request, int id)
		{
			if( request== default(AsientoItem))
				throw new HttpError(
						string.Format("No existe Item de Asiento con Id:'{0}'", id));			
		}
		
		public static void CheckIsChild(this AsientoItem request, Asiento asiento){
			if( request.IdAsiento!= asiento.Id)
				throw new HttpError(
					string.Format("Item con Id:'{0}' no pertenece al Asiento con Id:'{0}'",
						request.IdAsiento, asiento.Id ));			
		}
		
	}
}

