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
    public static  partial class DAL
    {
        public static MayorPresupuesto GetMayorPresupuesto(DALProxy proxy, string periodo, int idPresupuestoItem, int? idTercero=null)
        {
            var visitor= ReadExtensions.CreateExpression<MayorPresupuesto>();
            if(idTercero.HasValue) visitor.Where(r=>r.IdPresupuestoItem== idPresupuestoItem && r.IdTercero ==idTercero.Value);
            else visitor.Where(r=>r.IdPresupuestoItem== idPresupuestoItem && r.IdTercero==null);
            return proxy.FirstOrDefault(periodo.Substring(0,4), visitor);
        }


        public static void Update(this MayorPresupuesto item, DALProxy proxy, string periodo, short tipoPartida, decimal valor)
        {
            item.UpdateSaldos(periodo, tipoPartida==1?valor:0, tipoPartida==2? valor:0);
            SqlExpressionVisitor<MayorPresupuesto> expression = ReadExtensions.CreateExpression<MayorPresupuesto>();
            expression.Where(r=>r.Id==item.Id);
            proxy.Update<MayorPresupuesto>(item,periodo.Substring(0,4),expression);
        }


        public static void Insert(this MayorPresupuesto item, DALProxy proxy, string periodo, short tipoPartida, decimal valor)
        {
            item.UpdateSaldos(periodo, tipoPartida==1?valor:0, tipoPartida==2? valor:0);
            proxy.Create<MayorPresupuesto>(item,periodo.Substring(0,4));
        }
    }
}

