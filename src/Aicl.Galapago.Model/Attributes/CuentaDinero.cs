using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/CuentaDinero/create","post")]
	[RestService("/CuentaDinero/read","get")]
	[RestService("/CuentaDinero/read/{Id}","get")]
	[RestService("/CuentaDinero/update/{Id}","put")]
	[RestService("/CuentaDinero/destroy/{Id}","delete")]
	public partial class CuentaDinero
	{
	}
}