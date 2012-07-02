using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/MayorPresupuesto/create","post")]
	[RestService("/MayorPresupuesto/read","get")]
	[RestService("/MayorPresupuesto/read/{Id}","get")]
	[RestService("/MayorPresupuesto/update/{Id}","put")]
	[RestService("/MayorPresupuesto/destroy/{Id}","delete")]
	public partial class MayorPresupuesto
	{
	}
}