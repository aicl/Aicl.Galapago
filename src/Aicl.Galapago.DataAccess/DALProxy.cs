using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Data;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.Common.Web;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceHost;
using ServiceStack.Redis;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.DataAccess
{
    public class DALProxy:IDisposable
    {
        private IDbTransaction dbTransaction=null;
        private IDbCommand dbCmd=null;
        private IDbConnection dbConn;

        private IRedisClient redisClient;

        internal IDbConnectionFactory DbConnectionFactory {private get;set;}
        
        internal IRedisClientsManager RedisClientsManager {private get;set;}

        public DALProxy(){}

        public void BeginDbTransaction()
        {
            if(dbTransaction==null)
                dbTransaction=CreateCommand().BeginTransaction();
        }

        public void CommitDbTransaction()
        {
            if(dbTransaction!=null)
            {
                dbTransaction.Commit();
                dbTransaction.Dispose();
                dbTransaction=null;
            }
        }

        public void RollbackDbTransaction()
        {
            if(dbTransaction!=null)
            {
                dbTransaction.Rollback();
                dbTransaction.Dispose();
                dbTransaction=null;
            }
        }


        public IDisposable AcquireLock(string lockKey, double timeOut)
        {
            return CreateRedisClient().AcquireLock(lockKey, TimeSpan.FromSeconds(timeOut));
        }

        private void Execute(Action<IRedisClient,IDbCommand> commandsFn)
        {
            commandsFn(CreateRedisClient(), CreateCommand());
        }


        private T Execute<T>(Func<IRedisClient,IDbCommand,T> commandsFn)
        {
            return commandsFn(CreateRedisClient(), CreateCommand());
        }


        public T Execute<T>(Func<IDbCommand,T> commandsFn)
        {
            return commandsFn(CreateCommand());
        }


        public void  Execute(Action<IDbCommand> commandsFn)
        {
            commandsFn(CreateCommand());
        }


        #region IDisposable implementation
        public void Dispose ()
        {
            if(redisClient!=null)
            {
                redisClient.Dispose();
            }

            RollbackDbTransaction();

            if(dbCmd!=null) 
            {
                dbCmd.Dispose();
                dbConn.Close();
                dbConn.Dispose();
            }
        }
        #endregion

        private IDbCommand CreateCommand(){
            if(dbCmd==null)
            {
                dbConn = DbConnectionFactory.OpenDbConnection();
                dbCmd  = dbConn.CreateCommand();
            }
            return dbCmd;
        }

        private IRedisClient CreateRedisClient(){
            if(redisClient==null) redisClient= RedisClientsManager.GetClient();
            return redisClient;
        }


        public void ExecuteBeforePost<T>(T newData)
        {
            //llamar triggers....
            //Buscar Aicl.Galapago.BeforePost.dll en el folder Triggers  (namespace= Aicl.Galapago.BeforePost )
            // dentro de ese dll clases que Implementan IBeforePost<T>  
            //con nombre typeof(T)NN  ( TriggerEgreso, TriggerEgreso00, TriggerEgreso01, etc....)
            //y correr el metodo Run<T>(proxy, newData)
            //command(this);
        }

        public void ExecuteBeforePut<T>(T newData, T oldData)
        {

        }

        public void ExecuteBeforePatch<T>(T newData, T oldData, string operation)
        {

        }


        public void ExecuteAfterPost<T>(T newData)
        {

        }

        public void ExecuteAfterPut<T>(T newData, T oldData)
        {

        }

        public void ExecuteAfterPatch<T>(T newData, T oldData, string operation)
        {

        }


		#region metodos

		public List<T> Get<T>(  SqlExpressionVisitor<T> visitor)
            where T: new()
        {
            return Execute(dbCmd=>{
                return dbCmd.Select(visitor);
            });

        }

        public List<T> Get<T>( Expression<Func<T,bool>> predicate)
            where T: new()
        {
            return Execute(dbCmd=>{
                return dbCmd.Select(predicate);
            });

        }
		         
        public List<T> Get<T>()
            where T: new()
        {
            return Execute(dbCmd=>{
                return dbCmd.Select<T>();
            });

        }

		public T FirstOrDefault<T>(Expression<Func<T,bool>> predicate)
            where T: new()
        {
            return Execute(dbCmd=>{
                return dbCmd.FirstOrDefault(predicate);
            });
        }

		public T FirstOrDefault<T>(SqlExpressionVisitor<T> visitor)
            where T: new()
        {
            return Execute(dbCmd=>{
                return dbCmd.FirstOrDefault(visitor);
            });
        }

		public T FirstOrDefault<T>(string tableName, SqlExpressionVisitor<T> visitor)
            where T: new()
        {
            if(visitor==null) visitor= ReadExtensions.CreateExpression<T>();
            string sql= string.Format(visitor.ToSelectStatement(),tableName);
            return Execute(dbCmd=> dbCmd.FirstOrDefault<T>(sql) );
        }


		public T FirstOrDefaultById<T>( int id)
            where T: IHasId<int>, new()
        {
            return Execute((dbCmd)=>{
                return dbCmd.FirstOrDefault<T>(q=>q.Id== id);
            });
        }

        public  T FirstOrDefaultByIdFromCache<T>(int id)
            where T: IHasId<int>, new()
        {
            return Execute((redisClient, dbCmd)=>{
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


		public  List<T> GetByIdUsuarioFromCache<T>(int idUsuario)
            where T: IHasIdUsuario, new()
        {
            return Execute((redisClient,dbCmd)=>{
                var cacheKey = UrnId.Create<T>("IdUsuario", idUsuario.ToString());
                var visitor = ReadExtensions.CreateExpression<T>();
                visitor.Where(q=> q.IdUsuario==idUsuario);
                return dbCmd.Get(redisClient,cacheKey,visitor);
            });
        }

        public  List<T> GetFromCache<T>()
            where T: new()
        {
            return Execute((redisClient,dbCmd)=>{
                return dbCmd.Get<T>(redisClient);
            });
        }


       public long Count<T>(SqlExpressionVisitor<T> expression)
            where T: IHasId<int>, new()
        {
            return Execute(dbCmd=>{
                return dbCmd.GetScalar<T,long>(expression) ;
            });
        }

        public  Consecutivo GetNextConsecutivo(int idSucursal, string documento)
        {                            
            Consecutivo consecutivo= default(Consecutivo);
                    
            Execute((redisClient,dbCmd )=>{
                consecutivo=dbCmd.FirstOrDefault<Consecutivo>(q=> q.IdSucursal==idSucursal && q.Documento==documento);
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


        public CodigoDocumento GetCodigoDocumento(string codigo )
        {
            return Execute((redisClient, dbCmd)=>
            {
                return redisClient.Get(CodigoDocumento.GetCacheKey(codigo),
                ()=>{
                    return  dbCmd.FirstOrDefault<CodigoDocumento>(q=> q.Codigo==codigo);
                },
                TimeSpan.FromDays( Definiciones.DiasEnCache));                                        
            });

        }


        internal  PeriodoSucursal GetPeriodoSucursal(string periodo, int idSucursal)
        {
            var cacheKeySucursal= string.Format("urn:Periodo:Name:{0}:IdSucursal:{1}",periodo, idSucursal);
            var cacheKeyPeriodo=  string.Format("urn:Periodo:Name:{0}",periodo);

            return Execute((redisClient,dbCmd)=>{
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

		#endregion metodos


		public void Create<T>(T request,SqlExpressionVisitor<T> visitor=null) 
			where T: IHasId<System.Int32>, new()
		{
			Execute(dbCmd=>{
				if(visitor==null) 
					dbCmd.Insert<T>(request);
				else 
					dbCmd.InsertOnly<T>(request,visitor);
				dbCmd.AssertId(request);

			});
		}


		public void Create<T>(T request, string tableName, SqlExpressionVisitor<T> expression=null )
        where T: IHasId<System.Int32>, new()
        {
            if(expression==null) expression=ReadExtensions.CreateExpression<T>();
			Execute(dbCmd=>{
            	string sql= OrmLiteConfig.DialectProvider.ToInsertRowStatement(request, 
                                                                           expression.InsertFields,
                                                                           dbCmd);
            	sql=string.Format(sql,tableName);
            	dbCmd.ExecuteSql(sql);
				dbCmd.AssertId(request);
			});
        }

		/// <summary>
        /// Use an expression visitor to select which fields to update and construct the where expression, E.g: 
        /// 
        ///   dbCmd.UpdateOnly(new Person { FirstName = "JJ" }, ev => ev.Update(p => p.FirstName).Where(x => x.FirstName == "Jimi"));
        ///   UPDATE "Person" SET "FirstName" = 'JJ' WHERE ("FirstName" = 'Jimi')
        /// 
        ///   What's not in the update expression doesn't get updated. No where expression updates all rows. E.g:
        /// 
        ///   dbCmd.UpdateOnly(new Person { FirstName = "JJ", LastName = "Hendo" }, ev => ev.Update(p => p.FirstName));
        ///   UPDATE "Person" SET "FirstName" = 'JJ'
        /// </summary>
		public  void Update<T>(T request, Func<SqlExpressionVisitor<T>, SqlExpressionVisitor<T>> updateExpression)
		{
			Execute(dbCmd=>{
				dbCmd.UpdateOnly(request, updateExpression(OrmLiteConfig.DialectProvider.ExpressionVisitor<T>()));
			});
		}


		public void Update<T>(T request,Expression<Func<T,bool>> predicate=null) 
			where T: new()
		{
			Execute(dbCmd=>{
				if(predicate==null) dbCmd.Update(request);
				else dbCmd.Update(request,predicate);
			});
		}

		public void Update<T>(T request, SqlExpressionVisitor<T> expression=null) 
			where T:  new()
		{
			Execute(dbCmd=>{
				if(expression==null) dbCmd.Update(request);
				else dbCmd.UpdateOnly(request,expression);
			});
		}

		public void Update<T>(T request, string tableName, SqlExpressionVisitor<T> expression=null )
            where T : new()
        {
            if(expression==null) expression=ReadExtensions.CreateExpression<T>();
            string sql = OrmLiteConfig.DialectProvider.ToUpdateRowStatement(request,expression.UpdateFields);
            sql= string.Format(sql,tableName);
            sql = sql +( !expression.WhereExpression.IsNullOrEmpty()?  expression.WhereExpression:"" );     
            Execute(dbCmd=>dbCmd.ExecuteSql( sql));  
        }


		public void Delete<T>(Expression<Func<T,bool>> predicate) 
			where T: new()
		{
			Execute(dbCmd=>{
                dbCmd.Delete(predicate);
			});
		}

    }
}

/*
public bool RedisTransactionStarted{
    get { return RedisTransaction==null?false:true;}
}

public void BeginRedisTransaction(){

    if(RedisTransaction==null)
    {
        redisClientTr= RedisClientsManager.GetClient();
        RedisTransaction=  redisClientTr.CreateTransaction();
    }
}


public void CommitRedisTransaction(){

    if(RedisTransaction!=null)
    {
        RedisTransaction.Commit();
        RedisTransaction.Dispose();
        redisClientTr.Dispose();
        RedisTransaction=null;
    }
}

public void RollbackRedisTransaction(){

    if(RedisTransaction!=null)
    {
        RedisTransaction.Rollback();
        RedisTransaction.Dispose();
        redisClientTr.Dispose();
        RedisTransaction=null;
    }
}

*/

/*
public void ExecuteUpdate<T>(Action<DALProxy> command, T newData, T oldData)
{
    //llamar triggers before....
    command(this);
    //llamar triggers after
}
*/