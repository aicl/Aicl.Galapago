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
	[TestFixture]
	public class AsientoItemTest:TestBase
	{
		
		[Test]
		public void CanCreateAsientoItem ()
		{
			var asientoResponse= Client.Post<Response<Asiento>>("/Asiento/create", new Asiento(){IdSucursal=1});
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
		
		
	}
}

*/