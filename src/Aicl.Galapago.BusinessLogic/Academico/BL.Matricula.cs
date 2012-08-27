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

		#region post
		public static Response<Matricula> Post(this Matricula request, Factory factory,IHttpRequest httpRequest)
		{  
            request.CheckId(Operaciones.Create);

			if( request.IdIngreso.HasValue && request.IdIngreso.Value==default(int)) request.IdIngreso=null;
			if( request.IdClase.HasValue && request.IdClase.Value==default(int)) request.IdClase=null;

			var mr = new MatriculaResponse ();

			var queryString= httpRequest.QueryString;

            factory.Execute(proxy=>{

				proxy.Create<Matricula>(request);

				var data = proxy.Get<Matricula>(q=>	q.Id== request.Id);

				bool crearItems;
                if (bool.TryParse( queryString["CrearItems"], out crearItems) && crearItems){
					var tarifas = proxy.Get<Tarifa>(q=>q.IdSucursal== request.IdSucursal &&
					                                q.IdCentro==request.IdCentro && 
					                                q.Activo==true &&
					                                q.IncluirEnMatricula==true);


					foreach(Tarifa tarifa in tarifas){
						var mi = new MatriculaItem(){
							IdMatricula=request.Id,
							IdTarifa= tarifa.Id,
							Valor= tarifa.Valor,
							Descripcion= tarifa.Descripcion
						};

						proxy.Create<MatriculaItem>(mi);
						mr.MatriculaItemList.Add(mi);
					}
					mr.TarifaList= tarifas;
				}

				mr.Data=data;

            });
			return mr;
		}
		#endregion post

		/*
        #region put
        public static Response<Matricula> Put(this Matricula request,Factory factory,IHttpRequest httpRequest)
        {  
            factory.Execute(proxy=>{
                request.CheckId(Operaciones.Update);
                var oldData= proxy.FirstOrDefault<Matricula>(q=>q.Id==request.Id);
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
                proxy.Update<Matricula>(request);
            });
        
            List<Matricula> data = new List<Matricula>();
            data.Add(request);
            
            return new Response<Matricula>(){
                Data=data
            };  
            
        }
        #endregion put

        #region delete
        public static Response<Matricula> Delete(this Matricula request, Factory factory,IHttpRequest httpRequest)
        {
            request.CheckId(Operaciones.Destroy);

            factory.Execute(proxy=>
            {
                var oldData= proxy.FirstOrDefault<Matricula>(q=>q.Id==request.Id);
                oldData.AssertExists(request.Id);
                proxy.Delete<Matricula>(q=>q.Id==request.Id);
            });

            List<Matricula> data = new List<Matricula>();
            data.Add(request);
            
            return new Response<Matricula>(){
                Data=data
            }; 
        }
        #endregion delete
        */
	}
}

