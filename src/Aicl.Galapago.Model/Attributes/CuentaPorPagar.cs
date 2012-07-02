using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/CuentaPorPagar/create","post")]
	[RestService("/CuentaPorPagar/read","get")]
	[RestService("/CuentaPorPagar/read/{Id}","get")]
	[RestService("/CuentaPorPagar/update/{Id}","put")]
	[RestService("/CuentaPorPagar/destroy/{Id}","delete")]
	public partial class CuentaPorPagar
	{
	}
}