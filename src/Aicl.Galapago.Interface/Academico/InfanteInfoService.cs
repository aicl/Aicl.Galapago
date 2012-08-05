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
    public class InfanteInfoService:AppRestService<InfanteInfo>
    {
        public override object OnGet (InfanteInfo request)
        {
            try{
                return request.Get(Factory, RequestContext.Get<IHttpRequest>());
            }
            catch(Exception e){
                return HttpResponse.ErrorResult<Response<InfanteInfo>>(e,"GetErrorInfanteInfo");
            }
        }
    }
}
