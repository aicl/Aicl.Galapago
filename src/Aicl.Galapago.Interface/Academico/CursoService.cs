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
    public class CursoService:AppRestService<Curso>
    {
        public override object OnGet (Curso request)
        {
            try{
                return request.Get(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Curso>>(e,"GetErrorCurso");
            }
        }
		/*
        public override object OnPost (Curso request)
        {
            try{
                return request.Post(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Curso>>(e,"PostErrorCurso");
            }
        }   

        public override object OnPut (Curso request)
        {
            try{
                return request.Put(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Curso>>(e,"PutErrorCurso");
            }
        }

        public override object OnDelete (Curso request)
        {
            try{
                return request.Delete(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Curso>>(e,"DeleteErrorCurso");
            }
        }
        */

    }
}