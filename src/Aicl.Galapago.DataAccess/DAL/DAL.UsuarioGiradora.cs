using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceInterface;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.DesignPatterns.Model;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
    public static partial class DAL
    {
        public static UsuarioGiradora GetUsuarioGiradora(DALProxy proxy, int idUsuario, int idPresupuestoItem, int?idTercero)
        {
            return proxy.Execute(dbCmd=>{
                if(idTercero.HasValue)
                    return dbCmd.FirstOrDefault<UsuarioGiradora>(x=>x.IdUsuario== idUsuario 
                                                                 &&  x.IdPresupuestoItem== idPresupuestoItem
                                                                 && x.IdTercero==idTercero.Value);
                else
                    return dbCmd.FirstOrDefault<UsuarioGiradora>(x=>x.IdUsuario== idUsuario 
                                                                 &&  x.IdPresupuestoItem== idPresupuestoItem
                                                                 );
            });
        }
    }
}

