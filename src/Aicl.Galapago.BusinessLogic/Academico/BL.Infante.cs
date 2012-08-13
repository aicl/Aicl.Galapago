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
		public static Response<Infante> Post(this Infante request, Factory factory,IHttpRequest httpRequest)
		{  
            request.CheckId(Operaciones.Create);
            factory.Execute(proxy=>{

                if(request.IdTerceroFactura.HasValue && request.IdTerceroFactura.Value!=default(int))
                {
                    var tercero= proxy.FirstOrDefault<Tercero>(q=>q.Id==request.IdTerceroFactura.Value);
                    tercero.AssertExists(request.IdTerceroFactura.Value);
                    request.NombreTercero=tercero.Nombre;
                    request.DocumentoTercero=tercero.Documento;
                    request.DVTercero=tercero.DigitoVerificacion;
					request.TelefonoTercero= tercero.Telefono;
					request.MailTercero= tercero.Mail;
                }

                proxy.Create<Infante>(request);
            });
		
			List<Infante> data = new List<Infante>();
			data.Add(request);
			
			return new Response<Infante>(){
				Data=data
			};	
			
		}
		#endregion post


        #region put
        public static Response<Infante> Put(this Infante request,Factory factory,IHttpRequest httpRequest)
        {  
            factory.Execute(proxy=>{
                request.CheckId(Operaciones.Update);
                var oldData= proxy.FirstOrDefault<Infante>(q=>q.Id==request.Id);
                oldData.AssertExists(request.Id);

                if(request.IdTerceroFactura.HasValue )
                {
                    if(request.IdTerceroFactura.Value==default(int) ) 
                    {
                        request.IdTerceroFactura=null;
                        request.NombreTercero=string.Empty;
                        request.DocumentoTercero=string.Empty;
                        request.DVTercero=string.Empty;
						request.TelefonoTercero=string.Empty;
						request.MailTercero=string.Empty;
                    }
                    else
                    {
                        if(!oldData.IdTerceroFactura.HasValue ||
                           (oldData.IdTerceroFactura.HasValue && 
                            oldData.IdTerceroFactura.Value!=request.IdTerceroFactura.Value))
                        {
                            var tercero= proxy.FirstOrDefault<Tercero>(q=>q.Id==request.IdTerceroFactura.Value);
                            tercero.AssertExists(request.IdTerceroFactura.Value);
                            request.NombreTercero=tercero.Nombre;
                            request.DocumentoTercero=tercero.Documento;
                            request.DVTercero=tercero.DigitoVerificacion;
							request.TelefonoTercero= tercero.Telefono;
							request.MailTercero= tercero.Mail;
                        }
                    }
                }
                proxy.Update<Infante>(request);
            });
        
            List<Infante> data = new List<Infante>();
            data.Add(request);
            
            return new Response<Infante>(){
                Data=data
            };  
            
        }
        #endregion put

        #region delete
        public static Response<Infante> Delete(this Infante request, Factory factory,IHttpRequest httpRequest)
        {
            request.CheckId(Operaciones.Destroy);

            factory.Execute(proxy=>
            {
                var oldData= proxy.FirstOrDefault<Infante>(q=>q.Id==request.Id);
                oldData.AssertExists(request.Id);
                proxy.Delete<Infante>(q=>q.Id==request.Id);
            });

            List<Infante> data = new List<Infante>();
            data.Add(request);
            
            return new Response<Infante>(){
                Data=data
            }; 
        }
        #endregion delete
	}
}

