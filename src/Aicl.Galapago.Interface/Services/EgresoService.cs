using System;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	[RequiresAuthenticate]
	[Permission("Egreso.read")]
	[Permission(ApplyTo.Post, "Egreso.create")]	
	[Permission(ApplyTo.Put , "Egreso.update")]	
	[Permission(ApplyTo.Delete, "Egreso.destroy")]
	public class EgresoService:AppRestService<Egreso>
	{

        public override object OnGet (Egreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       

            Paginador paginador= new Paginador(httpRequest);

            string nombreTerecero= httpRequest.QueryString["NombreTercero"];
            if(!nombreTerecero.IsNullOrEmpty()) request.NombreTercero=nombreTerecero;

            string docTercero= httpRequest.QueryString["DocumentoTercero"];
            if(!docTercero.IsNullOrEmpty()) request.DocumentoTercero=docTercero;

            string periodo= httpRequest.QueryString["Periodo"];
            if(periodo.IsNullOrEmpty()) periodo=DateTime.Today.ObtenerPeriodo();
            request.Periodo=periodo;

            string asentado= httpRequest.QueryString["Asentado"];
            if(!asentado.IsNullOrEmpty())
            {
                bool tomarSoloAsentado;
                if( bool.TryParse(asentado,out tomarSoloAsentado))
                {
                    request.FechaAsentado= tomarSoloAsentado?DateTime.Today:default(DateTime);
                }
            }


            return request.Get(Factory, httpRequest.GetSession(), paginador);
        }

		public override object OnPost (Egreso request)
		{
            var httpRequest = RequestContext.Get<IHttpRequest>();       
			return request.Post(Factory, httpRequest.GetSession());
		}	

        public override object OnPut (Egreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       
            return request.Put(Factory,httpRequest.GetSession());
        }


        public override object OnPatch (Egreso request)
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();       

            var action= httpRequest.PathInfo.Substring(httpRequest.PathInfo.LastIndexOf("/")+1);
            return request.Patch(Factory, httpRequest.GetSession(), action);
        }

    //TODO : borrar Mayor_Presupuesto para acomodar Saldo_anterior_08 default 0

    

	}
}