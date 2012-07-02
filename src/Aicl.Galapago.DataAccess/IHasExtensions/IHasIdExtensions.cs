using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.FluentValidation;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;

namespace Aicl.Galapago.DataAccess
{
	public static class IHasIdExtensions
	{
			
		
		public static string GetLockKey<T>(this T request)
			where T:IHasId<System.Int32>, new()
		{
			return string.Format("urn:lock:{0}:Id:{1}", typeof(T).Name, request.Id);
		}
		
		public static string GetLockKey<T>(this int id)
			where T:IHasId<System.Int32>, new()
		{
			return string.Format("urn:lock:{0}:Id:{1}", typeof(T).Name, id);
		}
		
		
		public static string GetLockKey<T>(this T request, Type items)
			where T:IHasId<System.Int32>, new()
		{
			return string.Format("urn:lock:{0}:Id:{1}:{2}", typeof(T).Name, request.Id, items.Name);
		}
		
		public static string GetLockKey<T>(this int id, Type items)
			where T:IHasId<System.Int32>, new()
		{
			return string.Format("urn:lock:{0}:Id:{1}:{2}", typeof(T).Name, id, items.Name);
		}
		
		public static string GetCacheKey<T>(this T request)
			where T:IHasId<System.Int32>, new()
		{
			return UrnId.Create<T>("Id", request.Id.ToString());
		}
		
		public static string GetCacheKey<T>(this object id)
		{
            return  UrnId.Create<T>("Id", id.ToString()); //UrnId.Create<T>("Id", id.ToString());
		}
		
		//// urn:Asiento:Id:N:AsientoItem//
		public static string GetCacheKey<T>(this T request, Type items)
			where T:IHasId<System.Int32>, new()
		{
			return string.Format("{0}:{1}", UrnId.Create<T>("Id", request.Id.ToString()), items.Name);
		}
				
		public static string GetCacheKey<T>(this int id, Type items)
			where T:IHasId<System.Int32>, new()
		{
			return string.Format("{0}:{1}", UrnId.Create<T>("Id", id.ToString()), items.Name);
		}
		
		
		public static void CheckId<T>(this T request, string ruleSet)
			where T:IHasId<System.Int32>, new()
		{
			IdValidator<T> av = new IdValidator<T>();
			av.ValidateAndThrowHttpError<T>(request, ruleSet);
		}
		

        public static void AssertExists<T>(this T request, int id)
            where T:IHasId<System.Int32>, new()
        {
            if(object.Equals(request,default(T)))
                throw new HttpError(
                    string.Format("No existe registro:'{0}' con Id:'{1}'", typeof(T).Name, id));            

        }
		
		
	}
}