/*
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
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
	public static class IHasFechaAnuladoExtensiones
	{
	
		public static void CheckFechaAnulado<T>(this T request)
			where T:IHasFechaAnulado, IHasId<Int32>
		{
			if(request.FechaAnulado.HasValue) 
				throw new HttpError(
						string.Format("Documento<0> con Id:'{1}' se encuentra anulado", typeof(T).Name,
				              request.Id));						
			
		}
		
	}
}
*/