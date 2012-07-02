using System;
using System.Data;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceHost;
using ServiceStack.Redis;

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

        internal void Execute(Action<IRedisClient,IDbCommand> commandsFn)
        {
            commandsFn(CreateRedisClient(), CreateCommand());
        }


        internal T Execute<T>(Func<IRedisClient,IDbCommand,T> commandsFn)
        {
            return commandsFn(CreateRedisClient(), CreateCommand());
        }


        internal T Execute<T>(Func<IDbCommand,T> commandsFn)
        {
            return commandsFn(CreateCommand());
        }


        internal void  Execute(Action<IDbCommand> commandsFn)
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