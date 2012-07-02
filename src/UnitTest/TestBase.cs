using System;
using System.Collections.Generic;
using ServiceStack.Configuration;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.Text;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;


namespace UnitTest
{
	public class TestBase
	{
		//private static readonly string ListeningOn = ConfigUtils.GetAppSetting("ListeningOn");
		protected static readonly string BaseUri = ConfigUtils.GetAppSetting("BaseUri");
		
		//private AppHost appHost;
		
		protected JsonServiceClient Client {get; set;}
		protected int IdAsiento {get; set;}
		
		public TestBase ()
		{
		}
		
		
		[TestFixtureSetUp]
		public void OnTestFixtureSetUp()
		{
			//appHost = new AppHost();
			//appHost.Init();
			//appHost.Start(ListeningOn);
			Client = new JsonServiceClient(BaseUri);
			Client.Post<AuthenticationResponse>("/login", new Authentication(){UserName="test1", Password="test1"});
			//Client.Send<AuthResponse>(new Auth(){UserName="test1", Password="test1"});
		}
		

		[TestFixtureTearDown]
		public void OnTestFixtureTearDown()
		{
			Client.Delete<AuthResponse>("/logout");
			
			//appHost.Dispose();
		}

		
	}
	/*
	public class AsientoResponse:IHasResponseStatus 
	{
		public AsientoResponse ()
		{
			ResponseStatus= new ResponseStatus();
			Data= new List<Asiento>();
		}
		
		public ResponseStatus ResponseStatus { get; set; }
		
		public List<Asiento> Data {get; set;}
		
	}
	*/
}

//http://www.peoi.org/Courses/Coursessp/ac/fram4.html  contabilidad

//http://0.0.0.0:8080/api/auth?UserName=test1&Password=test1&format=json
//http://0.0.0.0:8080/api/auht?UserName=test1&Password=test1
//Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/Asiento/patch/13?action=reversar', method:'PATCH', params:{Id:13} })
//Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/Asiento/destroy/2548', method:'DELETE' })

//http://localhost:8082/login?UserName=test1&Password=test1&format=json
//for(var i =0; i<50; i++){
//Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/Asiento/create', method:'POST', params:{IdSucursal:1, Descripcion:'Test Create ext', Fecha:"\/Date(1337317200000-0500)\/"} })
//console.log('loop: ', i);

/*

for(var i =1; i<120; i++){
Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/Egreso/create', method:'POST', params:{IdSucursal:1,IdTercero:1,CodigoDocumento:'PRFV', Documento:1, Descripcion:'Test Create ext', Fecha:"\/Date(1337317200000-0500)\/", Numero:0} })
console.log('loop: ', i);
}

for(var i =1; i<2; i++){
Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/Egreso/update/357', method:'PUT', params:{Id:357, IdSucursal:1, Saldo:0, CodigoDocumento:'CC', Documento:1, Descripcion:'Updated Test Create ext' } })
console.log('loop: ', i);
}


Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/Egreso/patch/357/anular', method:'PATCH', params:{Id:357, IdSucursal:10, Saldo:10, CodigoDocumento:'CC', Documento:1, Descripcion:'Updated Test Create ext', FechaAsentado:"\/Date(1337317200000-0500)\/" } })

Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/Egreso/patch/357/asentar', method:'PATCH', params:{Id:357, IdSucursal:10, Saldo:10, CodigoDocumento:'CC', Documento:1, Descripcion:'Updated Test Create ext', FechaAsentado:"\/Date(1337317200000-0500)\/" } })

Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/Egreso/patch/357/reversar', method:'PATCH', params:{Id:357, IdSucursal:10, Saldo:10, CodigoDocumento:'CC', Documento:1, Descripcion:'Updated Test Create ext', FechaAsentado:"\/Date(1337317200000-0500)\/" } })

*/





// el primero con i=0 da error porque el idAsiento=0 !
//for(var i =0; i<20; i++){
//Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/AsientoItem/create', method:'POST', params:{IdAsiento:i, IdCuenta:395, IdCentro:1, Valor:7*i, TipoPartida:1 } });
//Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/AsientoItem/create', method:'POST', params:{IdAsiento:i, IdCuenta:108, IdCentro:1, Valor:7*i, TipoPartida:2 } });
//console.log('loop: ', i);
//}

/*
 
 				using (IRedisClient redisClient = cm.GetClient() )
				{		
					string x = redisClient.Get<string>("key5");
					Console.WriteLine("El valor es de key4 es {0}", x);
					
					using (var trans = redisClient.CreateTransaction())
					{
						
						trans.QueueCommand(r =>
						{
							r.Set("key6", "llave1222666");
							Console.WriteLine("hola.....");
							
						});			
							
						
						trans.QueueCommand(r =>
						{
							r.Remove("key6");
							
						});
						trans.Commit();
						
					}
					
					string lockKey= Extensions.GetLockKeyConsecutivoComprobanteContable(request.IdSucursal) ; 
					using (redisClient.AcquireLock(lockKey, TimeSpan.FromSeconds(LockSeconds)))
					{
						return block();
					}
				
				}
	
	
*/
//Aicl.Util.executeAjaxRequest({url:'http://0.0.0.0:8080/api/EgresoItem/create', method:'POST', params:{IdEgreso:361,IdCentro:2,IdTercero:1,Valor:15, IdPresupuestoItem:14, TipoPartida:1} })