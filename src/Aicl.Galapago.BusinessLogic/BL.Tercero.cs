using System;
using System.Data;
using System.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using Mono.Linq.Expressions;
namespace Aicl.Galapago.BusinessLogic
{
	public static partial class BL
	{	
        #region Get
        public static Response<Tercero> Get(this Tercero request,
                                           Factory factory,
                                           IHttpRequest httpRequest)
        {

            var paginador= new Paginador(httpRequest);
            var queryString= httpRequest.QueryString;

            long? totalCount=null;

            var data = factory.Execute(proxy=>{


                Expression<Func<Tercero, bool>> predicate;
                bool activo;
                if (bool.TryParse( queryString["Activo"], out activo))
                    predicate=q=>q.Activo== activo;                          
                else
                    predicate= PredicateBuilder.True<Tercero>();

                var documento= queryString["Documento"];
                if(!documento.IsNullOrEmpty())
                    predicate= predicate.AndAlso(q=>q.Documento.StartsWith(documento));

                var nombre= queryString["Nombre"];
                if(!nombre.IsNullOrEmpty())
                    predicate= predicate.AndAlso(q=>q.Nombre.Contains(nombre));

                bool esAuto;
                if(bool.TryParse(queryString["EsAutoRetenedor"],out esAuto))
                    predicate=predicate.AndAlso( q=>q.EsAutoRetenedor==esAuto);

                Expression<Func<Tercero, bool>> predicate2=null;

                bool esCliente;
                if(bool.TryParse(queryString["EsCliente"],out esCliente))
                    predicate2= q=>q.EsCliente==esCliente;

                bool esEmpleado;
                if(bool.TryParse(queryString["EsEmpleado"],out esEmpleado))
                {
                    if(predicate2==null) predicate2= q=>q.EsEmpleado==esEmpleado;
                    else predicate2= predicate2.OrElse(q=>q.EsEmpleado==esEmpleado);
                }

                bool esEps;
                if(bool.TryParse(queryString["EsEps"],out esEps))
                {
                    if(predicate2==null) predicate2= q=>q.EsEps==esEps;
                    else predicate2= predicate2.OrElse(q=>q.EsEps==esEps);
                }

                bool esFp;
                if(bool.TryParse(queryString["EsFp"],out esFp))
                {
                    if(predicate2==null) predicate2= q=>q.EsFp==esFp;
                    else predicate2= predicate2.OrElse(q=>q.EsFp==esFp);
                }

                bool esPF;
                if(bool.TryParse(queryString["EsParafiscal"],out esPF))
                {
                    if(predicate2==null) predicate2= q=>q.EsParafiscal==esPF;
                    else predicate2= predicate2.OrElse(q=>q.EsParafiscal==esPF);
                }

                bool esPr;
                if(bool.TryParse(queryString["EsProveedor"],out esPr))
                {
                    if(predicate2==null) predicate2= q=>q.EsProveedor==esPr;
                    else predicate2= predicate2.OrElse(q=>q.EsProveedor==esPr);
                }

                if(predicate2!=null) 
                    predicate= predicate.AndAlso(predicate2);


                var visitor = ReadExtensions.CreateExpression<Tercero>();


                if(paginador.PageNumber.HasValue)
                {
                    totalCount= proxy.Count(predicate);
                    int rows= paginador.PageSize.HasValue? paginador.PageSize.Value:BL.PageSize;
                    visitor.Limit(paginador.PageNumber.Value*rows, rows);
                }
                                
                visitor.Where(predicate).OrderBy(r=>r.Nombre);
                
                return proxy.Get(visitor);
            });

                        
            return new Response<Tercero>(){
                Data=data,
                TotalCount=totalCount
            };

        }
        #endregion Get


		public static void ValidateAndThrowHttpError(this Tercero request, string ruleSet)
		{
			TerceroValidator av = new TerceroValidator();
			av.ValidateAndThrowHttpError(request, ruleSet);
		}
		
		
	}
}

