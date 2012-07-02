/*
using System;
using System.Net;
using System.IO;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.Text;
using System.Collections.Generic;
using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;

namespace UnitTest
{
	[TestFixture()]
	public class AsientoTest:TestBase
	{
		[Test]
		public void ASecure()
		{
			var request = new Secured { Name = "test" };
			for(int i=0; i<100; i++){
			var response = Client.Send<SecuredResponse>(request);
			Assert.That(response.Result, Is.EqualTo(request.Name));
			Console.WriteLine("loop :{0}",i);	
			}
		}
	
		
		
		[Test]
		public void CanCreateAsiento ()
		{
			
			//for(int i=0;i<50;i++){
				
			var asientoResponse= 
				Client.Post<Response<Asiento>>("/Asiento/create", 
			new Asiento()
			{
				Descripcion="Test CanCreateAsiento",
				Fecha= DateTime.Today,
				IdSucursal=1,
				//Periodo="201205"
			});
			
			Console.WriteLine(asientoResponse.Dump());
			Assert.That(asientoResponse.Data[0].Fecha ,Is.EqualTo(DateTime.Today));			
			Assert.IsNull(asientoResponse.Data[0].FechaAnulado);			
			Assert.IsNull(asientoResponse.Data[0].FechaAsentado);			
			Assert.That(asientoResponse.Data[0].Creditos ,Is.EqualTo(0));			
			Assert.That(asientoResponse.Data[0].Debitos ,Is.EqualTo(0));			
			Assert.False(asientoResponse.Data[0].Externo); 
			//	Console.WriteLine("loop :{0}",i);
			//}
		}
	
		
		[Test]
		public void CanDestroyAsiento ()
		{
			var asientoResponse= Client.Post<Response<Asiento>>("/Asiento/create", 
			new Asiento()
			{
				Descripcion="Test CanDestroyAsiento",
				Fecha= DateTime.Today,
				IdSucursal=1
			});
			IdAsiento=asientoResponse.Data[0].Id;
			Assert.That(asientoResponse.Data[0].Fecha ,Is.EqualTo(DateTime.Today));			
			
			asientoResponse= Client.Delete<Response<Asiento>>(string.Format("/Asiento/destroy/{0}", IdAsiento));
				
			Assert.IsNullOrEmpty(asientoResponse.ResponseStatus.ErrorCode);
		}
	
		
		[Test]
		public void CanUpdateAsiento ()
		{
			var asientoResponse= Client.Post<Response<Asiento>>("/Asiento/create",
			new Asiento()
			{
				Descripcion="Test CanUpdateAsiento",
				Fecha= DateTime.Today,
				IdSucursal=1
			});
			IdAsiento=asientoResponse.Data[0].Id;
						
			asientoResponse= Client.Put<Response<Asiento>>(string.Format("/Asiento/update/{0}", IdAsiento),
			                                       new Asiento{Id=IdAsiento, Descripcion="Actualizado"});
			
			Assert.That(asientoResponse.Data[0].Descripcion,Is.EqualTo("Actualizado"));
			Assert.IsNull(asientoResponse.Data[0].FechaAnulado);			
			Assert.IsNull(asientoResponse.Data[0].FechaAsentado);			
			Assert.That(asientoResponse.Data[0].Creditos ,Is.EqualTo(0));			
			Assert.That(asientoResponse.Data[0].Debitos ,Is.EqualTo(0));			
			Assert.False(asientoResponse.Data[0].Externo);
		}
		
		[Test]
		public void CanPatchAsiento ()
		{
			
			var asiento= Client.Send<Response<Asiento>>("PATCH", "/Asiento/patch/12?action=asentar",new Asiento(){Id=12});
			Console.WriteLine(asiento.Dump());
*/

			/*
			var httpReq = (HttpWebRequest)WebRequest.Create(BaseUri+"/Asiento/patch/1");
			httpReq.Method = "PATCH";
			httpReq.ContentType = httpReq.Accept = "application/json";
			httpReq.CookieContainer= Client.CookieContainer;
			
			using (var stream = httpReq.GetRequestStream())
			using (var sw = new StreamWriter(stream))
			{
				//sw.Write("{\"IdSucursal\":1}");
				sw.Write( (new Asiento(){Id=1}).ToJson());
			}

			using (var response = httpReq.GetResponse())
			using (var stream = response.GetResponseStream())
			using (var reader = new StreamReader(stream))
			{
				Console.WriteLine(reader.ReadToEnd());
			}
			*/
/*
		}
		
		
		[Test]
		public void CanCreateAsientoItem ()
		{
			var asientoResponse= Client.Post<Response<Asiento>>("/Asiento/create",
			new Asiento()
			{
				Descripcion="Test CanCreateAsientoItem",
				Fecha= DateTime.Today,
				IdSucursal=1
			});
			Assert.That(asientoResponse.Data[0].Fecha ,Is.EqualTo(DateTime.Today));			
			Assert.IsNull(asientoResponse.Data[0].FechaAnulado);			
			Assert.IsNull(asientoResponse.Data[0].FechaAsentado);			
			Assert.That(asientoResponse.Data[0].Creditos ,Is.EqualTo(0));			
			Assert.That(asientoResponse.Data[0].Debitos ,Is.EqualTo(0));			
			Assert.False(asientoResponse.Data[0].Externo);
			
			
			// REGISTRAR UNA COMPRA
			var itemResponse = Client.Post<Response<AsientoItem>>("AsientoItem/create",
			new AsientoItem()
			{
				IdAsiento=asientoResponse.Data[0].Id,
				IdCuenta=395,
				IdCentro=1,
				Valor=18,
				TipoPartida=1
			});
			
			Assert.IsNotNull(itemResponse.Data[0].Id);
			
			itemResponse = Client.Post<Response<AsientoItem>>("AsientoItem/create",
			new AsientoItem()
			{
				IdAsiento=asientoResponse.Data[0].Id,
				IdCuenta=108,
				IdCentro=1,
				Valor=18,
				TipoPartida=2
			});
			
			Assert.IsNotNull(itemResponse.Data[0].Id);
						
			Console.WriteLine(itemResponse.Dump());
			
		}
		
		[Test]
		public void CanNotCreateAsientoItemWithWrongData ()
		{
			
			
			var asientoResponse= Client.Post<Response<Asiento>>("/Asiento/create",
			new Asiento()
			{
				Descripcion="Test CanNotCreateAsientoItemWithWrongData",
				Fecha= DateTime.Today,
				IdSucursal=1
			});
			Assert.That(asientoResponse.Data[0].Fecha ,Is.EqualTo(DateTime.Today));			
			Assert.IsNull(asientoResponse.Data[0].FechaAnulado);			
			Assert.IsNull(asientoResponse.Data[0].FechaAsentado);			
			Assert.That(asientoResponse.Data[0].Creditos ,Is.EqualTo(0));			
			Assert.That(asientoResponse.Data[0].Debitos ,Is.EqualTo(0));			
			Assert.False(asientoResponse.Data[0].Externo);
			
			try{
			
				Client.Post<Response<AsientoItem>>("AsientoItem/create",
				new AsientoItem()
				{
					IdAsiento=asientoResponse.Data[0].Id,
					IdCuenta=0,
					IdCentro=0,
					Valor=18,
					TipoPartida=1
				});
				
				Assert.False(true);
			}
			catch(Exception e ){
				
				Assert.True(true);
				Console.WriteLine("este el el error : ");
				Console.WriteLine( e.Message);
			}
			
		}
		
		
		[Test]
		public void Post_JSON_to_HelloWorld()
		{
			
			for(int i=0;i<50;i++){
			
			var httpReq = (HttpWebRequest)WebRequest.Create(BaseUri+"/Asiento/create");
			httpReq.Method = "POST";
			httpReq.ContentType = httpReq.Accept = "application/json";
			httpReq.CookieContainer= Client.CookieContainer;
			
			
			
			using (var stream = httpReq.GetRequestStream())
			using (var sw = new StreamWriter(stream))
			{
				sw.Write( (new Asiento(){IdSucursal=1,Descripcion="Test Post_JSON_to_HelloWorld",
				Fecha= DateTime.Today,}).ToJson());
			}

			using (var response = httpReq.GetResponse())
			using (var stream = response.GetResponseStream())
			using (var reader = new StreamReader(stream))
			{
				Console.WriteLine(reader.ReadToEnd());
			}	
			
			Console.WriteLine("loop :{0}",i);
			}
		}
		
		
	}
		

	
}

*/