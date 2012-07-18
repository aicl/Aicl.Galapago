using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceInterface;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
namespace Aicl.Galapago.BusinessLogic
{
	public static class CodigoDocumentoExtensiones
	{
		
        public static void CheckCodigoDocumento<T>(this T request, DALProxy proxy)
            where T:IHasCodigoDocumento, new()
        {
           
            var cd= proxy.GetCodigoDocumento( request.CodigoDocumento);

            cd.AssertExists(request.CodigoDocumento);
            cd.AssertEstaActivo();

        }

        public static void AssertExists(this CodigoDocumento codigoDocumento, string codigo)

        {
            if( codigoDocumento== default(CodigoDocumento))
                throw new HttpError(
                        string.Format("No existe Codigo Documento:'{0}'", codigo));          

        }


        public static void AssertEstaActivo(this CodigoDocumento codigoDocumento)

        {
            if(!codigoDocumento.Activo)
                throw new HttpError(
                        string.Format("Codigo Documento:'{0}' esta INACTIVO", codigoDocumento.Codigo));          

        }

        public static void CheckDebitos(this CodigoDocumento codigoDocumento, string codigoItemPresupuesto)
        {

            if(!codigoDocumento.DebitosPermitidos.Contains(codigoItemPresupuesto))
                throw new HttpError(
                    string.Format("Codigo de Presupuesto:'{0}' no permitido como debito para:'{1}'",
                              codigoItemPresupuesto,
                              codigoDocumento.Codigo));          

        }
    
        public static void CheckCreditos(this CodigoDocumento codigoDocumento, string codigoItemPresupuesto)
        {

            if(!codigoDocumento.CreditosPermitidos.Contains(codigoItemPresupuesto))
                throw new HttpError(
                    string.Format("Codigo de Presupuesto:'{0}' no permitido como credito para:'{1}'",
                              codigoItemPresupuesto,
                              codigoDocumento.Codigo));          

        }


	}
}

