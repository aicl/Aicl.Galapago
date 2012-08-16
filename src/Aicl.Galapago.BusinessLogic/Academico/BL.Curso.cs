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
		public static Response<Curso> Get(this Curso request, Factory factory, IHttpRequest httpRequest)
		{
			long? totalCount=null;

			var paginador= new Paginador(httpRequest);
            var queryString= httpRequest.QueryString;

            var predicate=PredicateBuilder.True<Curso>();

			var qv = queryString["Fecha"];
			if(! qv.IsNullOrEmpty())
			{
				DateTime fecha;
				if (DateTime.TryParse(qv, out fecha) )
				{
					var inicio= fecha.AddDays(-40);
					var fin = fecha.AddDays(40);
					predicate= predicate.AndAlso(q=>q.FechaInicio>=inicio &&
					                             q.FechaInicio<=fin);
				}
			}


			var visitor = ReadExtensions.CreateExpression<Curso>();
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
	                                
				visitor.OrderByDescending(r=> r.FechaInicio );

				return new Response<Curso>{
					Data=proxy.Get(visitor),
					TotalCount= totalCount
				};
			});
       
		}
		#endregion get
	}
}

