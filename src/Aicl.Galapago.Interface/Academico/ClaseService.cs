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
    [Permission(ApplyTo.Post, "Infante.update")]    
    [Permission(ApplyTo.Put , "Infante.update")]    
    [Permission(ApplyTo.Delete, "Infante.update")]
    public class ClaseService:AppRestService<Clase>
    {
        public override object OnGet (Clase request)
        {
            try{
                return request.Get(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Clase>>(e,"GetErrorClase");
            }
        }
		/*
        public override object OnPost (Clase request)
        {
            try{
                return request.Post(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Clase>>(e,"PostErrorClase");
            }
        }   

        public override object OnPut (Clase request)
        {
            try{
                return request.Put(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Clase>>(e,"PutErrorClase");
            }
        }

        public override object OnDelete (Clase request)
        {
            try{
                return request.Delete(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Clase>>(e,"DeleteErrorClase");
            }
        }
        */

    }
}