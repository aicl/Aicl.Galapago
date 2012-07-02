using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/CuentaPorCobrar/create","post")]
	[RestService("/CuentaPorCobrar/read","get")]
	[RestService("/CuentaPorCobrar/read/{Id}","get")]
	[RestService("/CuentaPorCobrar/update/{Id}","put")]
	[RestService("/CuentaPorCobrar/destroy/{Id}","delete")]
	public partial class CuentaPorCobrar
	{
	}
}