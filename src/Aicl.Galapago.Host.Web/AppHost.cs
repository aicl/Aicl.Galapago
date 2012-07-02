using System;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Redis;
using ServiceStack.Common.Web;
using ServiceStack.Common.Utils;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.Logging.Log4Net ;
using ServiceStack.OrmLite;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface;
using ServiceStack.OrmLite.Firebird;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.DataAccess;
using Aicl.Galapago.Interface;

namespace Aicl.Galapago.Host.Web
{
	public class AppHost:AppHostBase
	{
		private static ILog log;
		
		public AppHost (): base("Aicl.Galapago", typeof(AuthenticationService).Assembly)
		{
			var appSettings = new ConfigurationResourceManager();
			if (appSettings.Get("EnableLog4Net", false))
			{
				var cf="log4net.conf".MapHostAbsolutePath();
				log4net.Config.XmlConfigurator.Configure(
					new System.IO.FileInfo(cf));
				LogManager.LogFactory = new  Log4NetFactory();
			}
			else
				LogManager.LogFactory = new ConsoleLogFactory();
						
			log = LogManager.GetLogger(typeof (AppHost));

		}
		
		public override void Configure(Container container)
		{
			//Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
			base.SetConfig(new EndpointHostConfig
			{
				GlobalResponseHeaders =
					{
						{ "Access-Control-Allow-Origin", "*" },
						{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH" },
					},
				  DefaultContentType = ContentType.Json 
			});
							
									
			ConfigureApp(container);
			log.InfoFormat("AppHost Configured: " + DateTime.Now);
		}
				
		
		
		private void ConfigureApp(Container container){

			var appSettings = new ConfigurationResourceManager();
                    
            double se= appSettings.Get("DefaultSessionExpiry", 480);
            AuthProvider.DefaultSessionExpiry=TimeSpan.FromMinutes(se);         
                                   
            string cacheHost= appSettings.Get("REDISTOGO_URL","localhost:6379").Replace("redis://redistogo-appharbor:","").Replace("/","");

            var p = new BasicRedisClientManager(new string[]{cacheHost});
            
            OrmLiteConfig.DialectProvider= FirebirdOrmLiteDialectProvider.Instance;
            
            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(
                ConfigUtils.GetConnectionString("ApplicationDb"));
                        
            container.Register(appSettings);
            
            container.Register<Factory>(
                new Factory(){
                    DbFactory=dbFactory,
                    RedisClientsManager = p
                }
            );
            
            //container.Register<ICacheClient>(new MemoryCacheClient { FlushOnDispose = false });
            container.Register<IRedisClientsManager>(c => p);
                        
            Plugins.Add(new AuthFeature(
                 () => new AuthUserSession(), // or Use your own typed Custom AuthUserSession type
                new IAuthProvider[]
            {
                new AuthenticationProvider(){SessionExpiry=TimeSpan.FromMinutes(se)}

            })
            {
                IncludeAssignRoleServices=false, 
            });
                            
            
            OrmLiteAuthRepository authRepo = new OrmLiteAuthRepository(
                dbFactory
            );
            
            container.Register<IUserAuthRepository>(
                c => authRepo
            ); 

            
            if(appSettings.Get("EnableRegistrationFeature", false))
                Plugins.Add( new  RegistrationFeature());
						
		}
		
	}
}