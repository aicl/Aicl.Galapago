/*
using System;
using System.Data;
using System.Text;
using System.IO;
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
    public static class UsuarioGiradoraExtensiones
    {
        public static void AssertExists(this UsuarioGiradora usuarioGiradora, int idUsuario,  int idPresupuestoItem, int? idTercero)
        {
            if(usuarioGiradora==default(UsuarioGiradora))
               throw new HttpError(
                    string.Format("No existe Cuenta Giradora autorizada para:Idusuario'{0}'  IdPresupuestoItem:'{1}' idTercero:{2}",
                              idUsuario, idPresupuestoItem, idTercero.HasValue? idTercero.Value: 0));            
        }
    }
}

*/