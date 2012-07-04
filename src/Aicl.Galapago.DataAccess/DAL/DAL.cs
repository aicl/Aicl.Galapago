using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
       
        public static List<T> Get<T>( this DALProxy proxy, SqlExpressionVisitor<T> visitor)
            where T: new()
        {
            return proxy.Execute(dbCmd=>{
                return dbCmd.Select(visitor);
            });

        }
         
        public static long Count<T>( this DALProxy proxy,
                                    Expression<Func<T,bool>> predicate,
                                    bool excludeJoin=false
                                    )
            where T: IHasId<int>, new()
        {
            var expression= ReadExtensions.CreateExpression<T>();
            expression.ExcludeJoin=excludeJoin;
            expression.Select(r=> Sql.Count(r.Id)).Where(predicate);

            return proxy.Execute(dbCmd=>{
                return dbCmd.GetScalar<T,long>(expression) ;
            });

        }

        public static Consecutivo GetNextConsecutivo(DALProxy proxy,
                                                   int idSucursal, string documento)
        {
                                    
            Consecutivo consecutivo= default(Consecutivo);
                    
            proxy.Execute((redisClient,dbCmd )=>{

                consecutivo=dbCmd.FirstOrDefault<Consecutivo>(q=> q.IdSucursal==idSucursal 
                                                          && q.Documento==documento);
                if(consecutivo==default(Consecutivo))
                {
                    consecutivo=new Consecutivo()
                    {
                        IdSucursal=idSucursal,
                        Documento=documento,
                        Numero=1
                    };
                    dbCmd.InsertAndAssertId(consecutivo);
                }
                else
                {
                    consecutivo.Numero++;
                    dbCmd.Update(consecutivo);
                }
            });

            if(consecutivo==default(Consecutivo))
                new HttpError(System.Net.HttpStatusCode.InternalServerError,
                    string.Format("Imposible Obtener Consecutivo para IdSucursal:'{0}' Documento:'{1}'",
                        idSucursal, documento));
                    
            return consecutivo;
            
        }


        public static CodigoDocumento GetCodigoDocumento(DALProxy proxy, string codigo )
        {
            return proxy.Execute((redisClient, dbCmd)=>
            {
                return redisClient.Get(CodigoDocumento.GetCacheKey(codigo),
                ()=>{
                    return  dbCmd.FirstOrDefault<CodigoDocumento>(q=> q.Codigo==codigo);
                },
                TimeSpan.FromDays( Definiciones.DiasEnCache));                                        
            });

        }


        internal static PeriodoSucursal GetPeriodoSucursal(DALProxy proxy,
                                                             string periodo, int idSucursal)
        {
            var cacheKeySucursal= string.Format("urn:Periodo:Name:{0}:IdSucursal:{1}",periodo, idSucursal);
            var cacheKeyPeriodo=  string.Format("urn:Periodo:Name:{0}",periodo);
            return proxy.Execute((redisClient,dbCmd)=>{

            return redisClient.Get( cacheKeyPeriodo, () =>
            {
                    Periodo pr= dbCmd.FirstOrDefault<Periodo>(q=> q.Name==periodo );
                    
                    if(pr==default(Periodo))
                    { 
                        pr= new Periodo(){Name=periodo};
                        dbCmd.InsertAndAssertId<Periodo>(pr);
                        
                        if(pr.Id==default(int)) pr.Id= Convert.ToInt32(dbCmd.GetLastInsertId());            

                        PeriodoSucursal ps1 = new PeriodoSucursal()
                        {
                            IdSucursal=idSucursal,
                            IdPeriodo= pr.Id
                        };
                        dbCmd.InsertAndAssertId<PeriodoSucursal>(ps1);      
                                        
                        redisClient.Set(cacheKeyPeriodo, pr, TimeSpan.FromDays(Definiciones.DiasEnCache));
                        redisClient.Set(cacheKeySucursal, ps1, TimeSpan.FromDays(Definiciones.DiasEnCache));
                                            
                        return ps1;
                    }
                
                    if(pr.Bloqueado) return new PeriodoSucursal()
                    {
                        IdSucursal=idSucursal,
                        IdPeriodo=pr.Id,
                        Bloqueado=true, 
                    };
                                    
                    return redisClient.Get<PeriodoSucursal>(cacheKeySucursal,()=>
                    {
                        PeriodoSucursal ps2= dbCmd.FirstOrDefault<PeriodoSucursal>(q=> q.IdPeriodo == pr.Id && q.IdSucursal==idSucursal );
                        if(ps2== default(PeriodoSucursal))
                        {
                            ps2= new PeriodoSucursal(){ IdPeriodo= pr.Id, IdSucursal=idSucursal};
                            dbCmd.InsertAndAssertId(ps2);
                        }
                        return ps2;
                    },TimeSpan.FromDays(Definiciones.DiasEnCache));
                
            },TimeSpan.FromDays(Definiciones.DiasEnCache));
            
            });
        }


        public static List<T> GetByIdUsuarioFromCache<T>(DALProxy proxy, int idUsuario)
            where T: IHasIdUsuario, new()
        {
            return proxy.Execute((redisClient,dbCmd)=>{
                var cacheKey = UrnId.Create<T>("IdUsuario", idUsuario.ToString());
                var visitor = ReadExtensions.CreateExpression<T>();
                visitor.Where(q=> q.IdUsuario==idUsuario);
                return dbCmd.Get(redisClient,cacheKey,visitor);
            });
        }

        public static List<T> GetFromCache<T>(DALProxy proxy)
            where T: new()
        {
            return proxy.Execute((redisClient,dbCmd)=>{
                return dbCmd.Get<T>(redisClient);
            });
        }

        public static T FirstOrDefaultById<T>(DALProxy proxy, int id)
            where T: IHasId<int>, new()
        {
            return proxy.Execute((dbCmd)=>{
                return dbCmd.FirstOrDefault<T>(q=>q.Id== id);
            });
        }

        public static T FirstOrDefaultByIdFromCache<T>(DALProxy proxy, int id)
            where T: IHasId<int>, new()
        {

            return proxy.Execute((redisClient, dbCmd)=>{
                return dbCmd.FirstOrDefault<T>(q=>q.Id== id);
                /*
                var cacheKey = id.GetCacheKey<T>();
            
                T result = redisClient.Get(cacheKey, () =>
                {
                    return dbCmd.FirstOrDefault<T>(q=>q.Id== id);
                },
                TimeSpan.FromDays(Definiciones.DiasEnCache));
        
                return result;
                */

            });

        }


        public static string ObtenerPeriodo(this DateTime date){
            return date.Year.ToString() + date.Month.ToString().PadLeft(2,'0');
        }



    }
}

/*
internal static Consecutivo GetNext(this Consecutivo consecutivo, IDbCommand dbCmd, IRedisClient redisClient){
    return GetNextConsecutivo(dbCmd,redisClient,consecutivo.IdSucursal,consecutivo.Documento);
}


internal static void SomeMethod(IDbCommand dbCmd, IRedisClient redisClient){
    Consecutivo c = new Consecutivo();

    c.GetNext(dbCmd, redisClient);
    // o
    DAL.GetNext(c,dbCmd, redisClient);
}
*/

