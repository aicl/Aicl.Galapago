using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using Mono.Linq.Expressions;


namespace Aicl.Galapago.BusinessLogic
{
	public static partial class BL
	{
		#region get
		public static Response<Clase> Get(this Clase request, Factory factory, IHttpRequest httpRequest)
		{
			long? totalCount=null;

			var paginador= new Paginador(httpRequest);
            var queryString= httpRequest.QueryString;

            var predicate=PredicateBuilder.True<Clase>();

			var qv = queryString["Activo"];
			if(! qv.IsNullOrEmpty())
			{
				bool activo;
				if (bool.TryParse(qv, out activo) )
					predicate= predicate.AndAlso(q=>q.Activo==activo);
			}


			var visitor = ReadExtensions.CreateExpression<Clase>();
			visitor.Where(predicate);

			return factory.Execute(proxy=>{
				if(paginador.PageNumber.HasValue)
	            {
					visitor.Select(r=> Sql.Count(r.Id));
					totalCount= proxy.Count(visitor);
					visitor.Select();
	                int rows= paginador.PageSize.HasValue? paginador.PageSize.Value:BL.PageSize;
	                visitor.Limit(paginador.PageNumber.Value*rows, rows);
	            }
	                                
				visitor.OrderByDescending(r=> r.Nombre );

				return new Response<Clase>{
					Data=proxy.Get(visitor),
					TotalCount= totalCount
				};
			});
       
		}
		#endregion get
	}
}