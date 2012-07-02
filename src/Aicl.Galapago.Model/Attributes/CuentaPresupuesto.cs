using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/CuentaPresupuesto/create","post")]
	[RestService("/CuentaPresupuesto/read","get")]
	[RestService("/CuentaPresupuesto/read/{Id}","get")]
	[RestService("/CuentaPresupuesto/update/{Id}","put")]
	[RestService("/CuentaPresupuesto/destroy/{Id}","delete")]
	public partial class CuentaPresupuesto
	{
	}
}