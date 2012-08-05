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
    [Permission(ApplyTo.Post, "Infante.create")]    
    [Permission(ApplyTo.Put , "Infante.update")]    
    [Permission(ApplyTo.Delete, "Infante.destroy")]
    public class InfanteService:AppRestService<Infante>
    {
        public override object OnGet (Infante request)
        {
            try{
                return request.Get(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Infante>>(e,"GetErrorInfante");
            }
        }

        public override object OnPost (Infante request)
        {
            try{
                return request.Post(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Infante>>(e,"PostErrorInfante");
            }
        }   

        public override object OnPut (Infante request)
        {
            try{
                return request.Put(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Infante>>(e,"PutErrorInfante");
            }
        }

        public override object OnDelete (Infante request)
        {
            try{
                return request.Delete(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Infante>>(e,"DeleteErrorInfante");
            }
        }

    }
}