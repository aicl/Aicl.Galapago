using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceInterface;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.DesignPatterns.Model;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.DataAccess;

namespace ServiceStack.OrmLite
{
	public static class OrmLiteExtensions
	{		
		
		internal static void InsertAndAssertId<T>(this IDbCommand dbCmd,T request, 
		                                     SqlExpressionVisitor<T> visitor=null) 
			where T: IHasId<System.Int32>, new()
		{
			if(visitor==null) dbCmd.Insert<T>(request);
			else dbCmd.Insert<T>(request,visitor);
		
            dbCmd.AssertId(request);	
		}

        private static void AssertId<T>(this IDbCommand dbCmd,T request) 
            where T: IHasId<System.Int32>, new()
        {

            if(request.Id==default(int))
            {
                Type type = typeof(T);
                PropertyInfo pi= ReflectionUtils.GetPropertyInfo(type, OrmLiteConfig.IdField);
                var li = dbCmd.GetLastInsertId();
                ReflectionUtils.SetProperty(request, pi, Convert.ToInt32(li));  
            }
            
        }
        				
        internal static T FirstOrDefault<T>(this IDbCommand dbCmd, string tableName, SqlExpressionVisitor<T> visitor)
            where T: new()
        {
            if(visitor==null) visitor= ReadExtensions.CreateExpression<T>();
            string sql= string.Format(visitor.ToSelectStatement(),tableName);
            return dbCmd.FirstOrDefault<T>(sql);
        }


        internal static void Insert<T>(this IDbCommand dbCmd, T obj, string tableName, SqlExpressionVisitor<T> expression=null )
        where T: IHasId<System.Int32>, new()
        {
            if(expression==null) expression=ReadExtensions.CreateExpression<T>();
            string sql= OrmLiteConfig.DialectProvider.ToInsertRowStatement(obj, 
                                                                           expression.InsertFields,
                                                                           dbCmd);
            sql=string.Format(sql,tableName);
            dbCmd.ExecuteSql(sql);
            dbCmd.AssertId(obj);
        }

        internal static int Update<T>(this IDbCommand dbCmd, T obj, string tableName, SqlExpressionVisitor<T> expression=null )
            where T : new()
        {
            if(expression==null) expression=ReadExtensions.CreateExpression<T>();
            string sql = OrmLiteConfig.DialectProvider.ToUpdateRowStatement( obj, expression.UpdateFields);
            sql= string.Format(sql,tableName);
            sql = sql +( !expression.WhereExpression.IsNullOrEmpty()?  expression.WhereExpression:"" );     
            return dbCmd.ExecuteSql( sql);  
        }
        

        internal static List<T>  Get<T>(this IDbCommand dbCmd, IRedisClient redisClient)
            where T: new()
        {
            return dbCmd.Get<T>(redisClient, string.Format("urn:{0}", typeof(T).Name), null);
        }

        internal static List<T>  Get<T>(this IDbCommand dbCmd, IRedisClient redisClient, string cacheKey, SqlExpressionVisitor<T> visitor)
            where T: new()
        {
            
            return redisClient.Get(cacheKey, () =>
            {
                return visitor==null?  dbCmd.Select<T>():dbCmd.Select<T>(visitor) ;
            },
            TimeSpan.FromDays(Definiciones.DiasEnCache));
        }


		/*
		public static void Create<T,TKey>(this IDbCommand dbCmd, T obj,
		                             Expression<Func<T, TKey>> fields)
			where T : new()
		{
			var ev = OrmLiteConfig.DialectProvider.ExpressionVisitor<T>();
			ev.Insert(fields);
			
			string sql= OrmLiteConfig.DialectProvider.ToInsertRowStatement(obj, 
		    	ev.InsertFields,
			 	dbCmd);
			
			dbCmd.ExecuteSql(sql);	
			
		}
		*/
	}
}