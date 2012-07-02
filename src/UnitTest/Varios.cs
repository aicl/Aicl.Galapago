using System;
using NUnit.Framework;
using ServiceStack.Redis;
namespace UnitTest
{
    [TestFixture()]
    public class Varios
    {
        [Test]
        public void TestAssertExist ()
        {
            Simple simple= new Simple();
            simple.AssertExists();
            simple=default(Simple);
            try{
                simple.AssertExists();
                Assert.Fail("Fail");
            }
            catch{
                Console.WriteLine("first pass");
            }


            simple=null;
            try{
                simple.AssertExists();
                Assert.Fail("Fail");
            }
            catch{

                Console.WriteLine("second pass");
            }

        }


        [Test]
        public void TestRedis1()
        {
            string cacheHost= "localhost:6379";
            int cacheDb= 8;

            PooledRedisClientManager pm = new PooledRedisClientManager(new string[]{cacheHost},
                        new string[]{cacheHost},
                        cacheDb); 
            using (IRedisClient redisClient1 = pm.GetClient() )
            using (IRedisClient redisClient = pm.GetClient() )
            {       

                redisClient1.RemoveAll(new string[]{"key1", "key2", "key3"});

                redisClient.Set("key1","llave1");
                redisClient.Set("key2","llave2");
                
                using (var trans = redisClient.CreateTransaction())
                {
                    
                    trans.QueueCommand(r =>
                    {
                        r.Set("key1", "llave1Actualizada");
                        Assert.That(redisClient1.Get<string>("key1"), Is.EqualTo( "llave1") );
                    });         
                    Assert.That(redisClient1.Get<string>("key1"), Is.EqualTo( "llave1") );


                    trans.QueueCommand(r =>
                    {
                        //Console.WriteLine("key2 before queue '{0}'",r.Get<string>("key2")); // just on r please
                        Assert.IsNull(r.Get<string>("key2"));
                        Assert.That(redisClient1.Get<string>("key2"), Is.EqualTo( "llave2") );

                    });


                    trans.QueueCommand(r =>
                    {
                        //Console.WriteLine("key2 before queue '{0}'",r.Get<string>("key2"));//fail !!
                        r.Set("key2", "llave2Actualizada");
                        Assert.That(redisClient1.Get<string>("key2"), Is.EqualTo( "llave2") );
                    });
                    Assert.That(redisClient1.Get<string>("key2"), Is.EqualTo( "llave2") );

                    trans.QueueCommand(r =>
                    {
                        r.Set("key3", "llave3");
                        Assert.IsNull(redisClient1.Get<string>("key3"));
                        
                    });
                    Assert.IsNull(redisClient1.Get<string>("key3"));


                    trans.Commit();
                    Assert.That(redisClient1.Get<string>("key1"), Is.EqualTo( "llave1Actualizada") );
                    Assert.That(redisClient1.Get<string>("key2"), Is.EqualTo( "llave2Actualizada") );
                    Assert.That(redisClient1.Get<string>("key3"), Is.EqualTo( "llave3") );

                }

                    
            }
        }

    }

    public class Simple{};

    public static class Extensiones{
        public static void AssertExists(this Simple simple){
            if(simple==default(Simple))
                throw new Exception("simple no existe");
        }
    }

}

