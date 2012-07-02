using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Presupuesto/create","post")]
	[RestService("/Presupuesto/read","get")]
	[RestService("/Presupuesto/read/{Id}","get")]
	[RestService("/Presupuesto/update/{Id}","put")]
	[RestService("/Presupuesto/destroy/{Id}","delete")]
	public partial class Presupuesto
	{
	}
}