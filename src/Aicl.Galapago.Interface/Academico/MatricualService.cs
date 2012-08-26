using System;
﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
    [RequiresAuthenticate]
    [Permission("Matricula.read")]
    [Permission(ApplyTo.Post, "Matricula.create")]    
    [Permission(ApplyTo.Put , "Matricula.update")]    
    [Permission(ApplyTo.Delete, "Matricula.destroy")]
    public class MatriculaService:AppRestService<Matricula>
    {
     /*   public override object OnGet (Matricula request)
        {
            try{
                return request.Get(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Matricula>>(e,"GetErrorMatricula");
            }
        }
     */

        public override object OnPost (Matricula request)
        {
            try{
                return request.Post(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Matricula>>(e,"PostErrorMatricula");
            }
        }   
		/*
        public override object OnPut (Matricula request)
        {
            try{
                return request.Put(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Matricula>>(e,"PutErrorMatricula");
            }
        }

        public override object OnDelete (Matricula request)
        {
            try{
                return request.Delete(Factory,RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<Matricula>>(e,"DeleteErrorMatricula");
            }
        }
        */

    }
}