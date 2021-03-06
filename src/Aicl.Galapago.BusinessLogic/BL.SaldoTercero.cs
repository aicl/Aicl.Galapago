using System;
using System.Linq.Expressions;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using Mono.Linq.Expressions;

namespace Aicl.Galapago.BusinessLogic
{
	public static  partial class BL
	{
		#region Get
        public static Response<SaldoTercero> Get(this SaldoTercero request,
		                                              Factory factory,
		                                              IHttpRequest httpRequest)
        {
            return factory.Execute(proxy=>{

				long? totalCount=null;

				var paginador= new Paginador(httpRequest);
            	var queryString= httpRequest.QueryString;

                var predicate = PredicateBuilder.True<SaldoTercero>();

				var nombre= queryString["Nombre"];
                if(!nombre.IsNullOrEmpty())
                    predicate= predicate.AndAlso(q=>q.Nombre.Contains(nombre));

				var sucursal= queryString["NombreSucursal"];
                if(!sucursal.IsNullOrEmpty())
                    predicate= predicate.AndAlso(q=>q.NombreSucursal.Contains(sucursal));

				string p= queryString["IdSucursal"];
            	if(!p.IsNullOrEmpty())
           		{
					int idSucursal;
	                if( int.TryParse(p,out idSucursal))
	                {
						if(idSucursal!=default(int))
							predicate= predicate.AndAlso(q=>q.IdSucursal==idSucursal);
	                }
	            }


				p= queryString["IdTercero"];
            	if(!p.IsNullOrEmpty())
           		{
					int idTercero;
	                if( int.TryParse(p,out idTercero))
	                {
						if(idTercero!=default(int))
							predicate= predicate.AndAlso(q=>q.IdTercero==idTercero);
	                }
	            }

				p= queryString["Grupo"];
				if(!p.IsNullOrEmpty())
           		{
					if(p=="CuentasPorPagar" || p=="CxP")
						predicate=predicate.AndAlso(q=>q.CodigoItem.StartsWith(Definiciones.GrupoCuentasPorPagar));
					else if(p=="CuentasPorCobrar" || p=="CxC")
						predicate=predicate.AndAlso(q=>q.CodigoItem.StartsWith(Definiciones.GrupoCuentasPorCobrar));						
	            }


				predicate= predicate.AndAlso(q=> (q.SaldoInicial+q.Debitos-q.Creditos)!=0);

                var visitor = ReadExtensions.CreateExpression<SaldoTercero>();
				visitor.Where(predicate);

                if(paginador.PageNumber.HasValue)
                {
					visitor.Select(r=> Sql.Count(r.Id));
                    totalCount= proxy.Count(visitor);
					visitor.Select();
                    int rows= paginador.PageSize.HasValue? paginador.PageSize.Value:BL.PageSize;
                    visitor.Limit(paginador.PageNumber.Value*rows, rows);
                }
                                
                visitor.OrderBy(r=>r.Nombre);      

				return new Response<SaldoTercero>(){
                	Data=proxy.Get(visitor),
                	TotalCount=totalCount
            	};

            });

        }
        #endregion Get

	}
}

