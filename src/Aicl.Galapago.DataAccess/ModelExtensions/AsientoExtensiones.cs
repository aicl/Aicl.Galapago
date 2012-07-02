using System;
using System.Data;
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
	public static class AsientoExtensiones
	{
				
		public static Response<Asiento> Post(this Asiento request,
		                                     Factory factory,
		                                     IRequestContext requestContext,
		                                     bool checkSucursal=true,
		                                     bool checkPeriodo=true,
		                                     Action<IDbCommand,Asiento> runBeforePostDbCommandsFn=null,
		                                     Action<IDbCommand,Asiento> runAfterPostDbCommandsFn=null)
		{
			
						
			var httpRequest = requestContext.Get<IHttpRequest>();		
			request.ValidateAndThrowHttpError(Operaciones.Create,httpRequest, factory);
						
			if(checkSucursal) request.CheckSucursal(factory, httpRequest);
						
			request.Periodo= request.Fecha.ObtenerPeriodo();
			
			if(checkPeriodo) request.CheckPeriodo(factory,httpRequest);			
									
						
			try
			{
				SqlExpressionVisitor<Asiento> ev = OrmLiteConfig.DialectProvider.ExpressionVisitor<Asiento>();
				ev.Insert( f=> new { f.Id, f.Descripcion, f.Fecha , f.Periodo, f.IdSucursal,f.Numero,f.CodigoDocumento,f.Documento });                  
				
				var data = factory.Post<Asiento>(request,ev,
				dbCmd=>
				{
					request.Numero= dbCmd.GetConsecutivo(request.IdSucursal,Definiciones.ComprobranteContable,
					                                     httpRequest).Numero;
					
					if(request.Documento.IsNullOrEmpty()) request.Documento=request.Numero.ToString();
					
					if(runBeforePostDbCommandsFn!=null) runBeforePostDbCommandsFn(dbCmd,request);					
				},
				(dbCmd,result)=>
				{
					httpRequest.CacheClientExec(
					cache=>
						cache.Set(result.GetCacheKey(),
					          result,
					          TimeSpan.FromDays(Definiciones.DiasEnCache))
					);
					if(runAfterPostDbCommandsFn!=null) runAfterPostDbCommandsFn(dbCmd,result);
				});
				
				return new Response<Asiento>(){
				Data= data
				};
			}
			catch(Exception e ){
				throw new HttpError(e.Message);
			}
			
		}
		
		public static Response<Asiento> Delete(this Asiento request,
		                                     Factory factory,
		                                     IRequestContext requestContext,
		                                     bool checkSucursal=true,
		                                     bool checkPeriodo=false,
		                                     Action<IDbCommand,Asiento> runBeforeDeleteDbCommandsFn=null,
		                                     Action<IDbCommand,Asiento> runAfterDeleteDbCommandsFn=null)
		{
			
			request.CheckId(Operaciones.Destroy);
			
			var httpRequest = requestContext.Get<IHttpRequest>();		
			string lockKey=request.GetLockKey(); 	
						
			Action block=()=>{
		
				factory.Delete(request,(dbCmd)=>
				{	
					httpRequest.CacheClientExec(cache=>
					{
						var id = request.Id;
						var cacheKey=request.GetCacheKey();
						request= cache.Get<Asiento>(cacheKey);
						if(request== default(Asiento))
						{
							request= dbCmd.FirstOrDefault<Asiento>(q=> q.Id==id);
							AssertExists(request, id);	
						}
						else
						{
							cache.Remove(cacheKey);
						}
						
						request.ValidateAndThrowHttpError(Operaciones.Destroy);
												
						if(checkSucursal) request.CheckSucursal(factory, httpRequest);
						if(checkPeriodo)  request.CheckPeriodo(factory,httpRequest);			
						if(runBeforeDeleteDbCommandsFn!=null) runBeforeDeleteDbCommandsFn(dbCmd, request);	
					});
	
				},(dbCmd)=>
				{	
					if(runAfterDeleteDbCommandsFn!=null) runAfterDeleteDbCommandsFn(dbCmd,request);
				});
			}; 			
			
			IRedisClientsManager cm = httpRequest.GetCacheClient() as IRedisClientsManager;
	
			try
			{
				if( cm != null)
				{
					cm.Exec(redisClient=>
					{
						using (redisClient.AcquireLock(lockKey, TimeSpan.FromSeconds(Definiciones.LockSeconds)))
						{
							block();
						}	
					});
				}
				else
					block();
				
				return new Response<Asiento>(){};
			}		
			catch(Exception e)
			{
				throw new HttpError(e.Message);
			}
			
		}
				
		
		public static Response<Asiento> Put(this Asiento request,
		                                     Factory factory,
		                                     IRequestContext requestContext,
		                                     bool checkSucursal=true,
		                                     bool checkPeriodo=false,
		                                     Action<IDbCommand,Asiento> runBeforePutDbCommandsFn=null,
		                                     Action<IDbCommand,Asiento> runAfterPutDbCommandsFn=null)
		{
			
			request.CheckId(Operaciones.Update);
			
			string lockKey=request.GetLockKey(); 
			
			var httpRequest = requestContext.Get<IHttpRequest>();		
							
			Action block=()=>{
				
				SqlExpressionVisitor<Asiento> ev = OrmLiteConfig.DialectProvider.ExpressionVisitor<Asiento>();
				ev.Update( f=> new { f.Descripcion, f.Fecha , f.Periodo, f.IdSucursal, f.CodigoDocumento, f.Documento })
					.Where(f=>f.Id==request.Id);                  
				
				var cacheKey=request.GetCacheKey();
				
				factory.Put(request, ev,
				(dbCmd)=>
				{	
					httpRequest.CacheClientExec(cache=>
					{		
						var data=cache.Get<Asiento>(cacheKey);
						if(data== default(Asiento))
						{
							data= dbCmd.FirstOrDefault<Asiento>(q=> q.Id==request.Id);
							AssertExists(data, request.Id);
						}
						
						if(!request.CodigoDocumento.IsNullOrEmpty() )
							data.CodigoDocumento=request.CodigoDocumento;
						
						if(!request.Documento.IsNullOrEmpty())
							data.Documento=request.Documento;
						
						data.ValidateAndThrowHttpError(Operaciones.Update,httpRequest, factory);
						
						if(request.IdSucursal!=default(int))
						{
							data.IdSucursal= request.IdSucursal;
						}
						
						if(request.Fecha!=default(DateTime))
						{  							
							data.Fecha= request.Fecha;
							data.Periodo=  data.Fecha.ObtenerPeriodo();
						}
						
												
						if(checkSucursal) data.CheckSucursal(factory, httpRequest);	
						if(checkPeriodo)  data.CheckPeriodo(factory,httpRequest);
						
						if(!request.Descripcion.IsNullOrEmpty()) data.Descripcion=request.Descripcion;
												
						request.PopulateWith(data);
						if(runBeforePutDbCommandsFn!=null) runBeforePutDbCommandsFn(dbCmd,request);						
					});
				},
				(dbCmd)=>
				{
					if(runAfterPutDbCommandsFn!=null) runAfterPutDbCommandsFn(dbCmd, request);
					httpRequest.CacheClientExec(cache=> cache.Set(cacheKey, request,
					                                              TimeSpan.FromDays(Definiciones.DiasEnCache)));
				});
			}; 			
			
			
			IRedisClientsManager cm = httpRequest.GetCacheClient() as IRedisClientsManager;
		
			try
			{
				if( cm != null)
				{
					cm.Exec(redisClient=>
					{
						using (redisClient.AcquireLock(lockKey, TimeSpan.FromSeconds(Definiciones.LockSeconds)))
						{
							block();
						}	
					});
				}
				else
					block();
				
				List<Asiento> data = new List<Asiento>();
				data.Add(request);
				return new Response<Asiento>(){Data=data};
			}
		
			catch(Exception e)
			{
				throw new HttpError(e.Message);
			}
		}
		
		public static void ValidateAndThrowHttpError(this Asiento request, string ruleSet)
		{
			AsientoValidator av = new AsientoValidator();
			av.ValidateAndThrowHttpError(request, ruleSet);
		}
		
		private static void ValidateAndThrowHttpError(this Asiento request, string ruleSet,
		                                              IHttpRequest httpRequest, Factory factory)
		{
			ValidateAndThrowHttpError(request, ruleSet);
			
			CodigoDocumento cd = factory.GetCodigoDocumento(request.CodigoDocumento, httpRequest);
		
			CodigoDocumentoExtensiones.AssertExists(cd, request.CodigoDocumento);
			
			
			
		}
		
		public static void AssertExists(Asiento request, int id)
		{
			if( request== default(Asiento))
				throw new HttpError(
						string.Format("No existe Asiento con Id:'{0}'", id));			
		}
		
	}
}
/*


using (redisClient.AcquireLock(lockKey, TimeSpan.FromSeconds(Definiciones.LockSeconds)))
					{
						factory.Delete(request,(dbCmd)=>
						{
							
							httpRequest.GetFromCache(request.GetCacheKey(),()=>  // TTL?
							{
								var id = request.Id;
								request= dbCmd.FirstOrDefault<Asiento>(q=> q.Id==id);
								if( request== default(Asiento))
									throw HttpError.NotFound(
										string.Format("No existe Asiento con Id:'{0}'", id));
								
								if(checkSucursal) request.CheckSucursal(factory, httpRequest);
				
								return request; default(Asiento)?
							});
						},
						(dbCmd,asiento)=>
						{
							httpRequest.CacheClientExec(cache=> cache.Remove(request.GetCacheKey()));
						});
					}

*/