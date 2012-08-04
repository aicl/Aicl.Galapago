using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Auth;
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
		public static Response<Infante> Get(this Infante request, Factory factory, IHttpRequest httpRequest)
        {
			long? totalCount=null;

			var paginador= new Paginador(httpRequest);
            var queryString= httpRequest.QueryString;

            var predicate=PredicateBuilder.True<Infante>();

			var documento = queryString["Documento"];
			if(! documento.IsNullOrEmpty())
			{
				predicate= predicate.AndAlso(q=>q.Documento.StartsWith(documento));
			}

			var nombres= queryString["Nombres"];
			if(!nombres.IsNullOrEmpty())
				predicate= predicate.AndAlso(q=>q.Nombres.Contains(nombres));

			var apellidos= queryString["Apellidos"];
			if(!apellidos.IsNullOrEmpty())
				predicate= predicate.AndAlso(q=>q.Apellidos.Contains(apellidos));

			var visitor = ReadExtensions.CreateExpression<Infante>();
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
	                                
				visitor.OrderByDescending(r=> new { r.Nombres, r.Apellidos});

				return new Response<Infante>{
					Data=proxy.Get(visitor),
					TotalCount= totalCount
				};
			});
            
		}
		#endregion get

		#region post
		public static Response<Infante> Post(this Infante request,
                                            Factory factory,
                                            IAuthSession authSession)
		{
          
            factory.Execute(proxy=>{
                
            });
		
			List<Infante> data = new List<Infante>();
			data.Add(request);
			
			return new Response<Infante>(){
				Data=data
			};	
			
		}
		#endregion post
	}
}

