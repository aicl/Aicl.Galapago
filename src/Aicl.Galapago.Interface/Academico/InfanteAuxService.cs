using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
    [RequiresAuthenticate]
    [Permission("Infante.read")]
    public class InfanteAuxService:AppRestService<InfanteAux>
    {
        public override object OnGet (InfanteAux request)
        {
            try{
                return request.Get(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<InfanteAux>>(e,"GetErrorInfanteAux");
            }
        }
		/*
        public override object OnPost (InfanteAux request)
        {
            try{
                return request.Post(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<InfanteAux>>(e,"PostErrorInfanteAux");
            }
        }   

        public override object OnPut (InfanteAux request)
        {
            try{
                return request.Put(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<InfanteAux>>(e,"PutErrorInfanteAux");
            }
        }

        public override object OnDelete (InfanteAux request)
        {
            try{
                return request.Delete(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<InfanteAux>>(e,"DeleteErrorInfanteAux");
            }
        }
        */

    }
}