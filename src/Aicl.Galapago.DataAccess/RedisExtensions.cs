using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.Redis;

namespace Aicl.Galapago.DataAccess
{
	public static class RedisExtensions
	{
	/*
		
		public static void Exec(this IRedisClientsManager redisClientsManager,Action<IRedisClient> runRedisCommandsFn)
		{
			using (IRedisClient redisClient = redisClientsManager.GetClient() )
			{
				runRedisCommandsFn(redisClient);
			}
		}

		public static T Exec<T>(this IRedisClientsManager redisClientsManager,Func<IRedisClient,T> runRedisCommandsFn)
		{
			using (IRedisClient redisClient = redisClientsManager.GetClient() )
			{
				return runRedisCommandsFn(redisClient);
			}
		}

		
		public static T GetFromCache<T>(this IRedisClientsManager redisClientsManager,string cacheKey,
			Func<T> factoryFn,
		    TimeSpan? expiresIn=null)
		{
			return redisClientsManager.Exec( redisClient=>{
				return redisClient.GetFromCache<T>(cacheKey, factoryFn, expiresIn);
			});
		}
		

*/
		internal static T Get<T> (this IRedisClient redisClient,string cacheKey,
			Func<T> factoryFn,
		    TimeSpan? expiresIn=null)
		{
			var res = redisClient.Get<T>(cacheKey);
			if (res != null)
			{ 
				redisClient.CacheSet<T>(cacheKey, res, expiresIn);
				return res;
			}
			else
			{
				res= factoryFn();
				if (res != null ) redisClient.CacheSet<T>(cacheKey, res, expiresIn);
				return res;
			}
		}
		/*
		
		public static  T GetFromCache<T>(this IRedisClientsManager redisClientsManager,
		                                 string cacheKey,Func<IRedisClient, T> factoryFn,
		                                 TimeSpan? expiresIn=null )
		{
			return redisClientsManager.Exec (redisClient=> {
				var res = redisClient.Get<T>(cacheKey);
				if (res != null)
				{
					if(expiresIn.HasValue) redisClient.Set<T>(cacheKey,res, expiresIn.Value);
					return res;
				}
				else
				{
					res= factoryFn(redisClient);
					if (res != null ) redisClient.CacheSet<T>(cacheKey, res, expiresIn);
					return res;
				}
			});
		}
	*/
		
	}
}

