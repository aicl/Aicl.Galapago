using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/PresupuestoItem/create","post")]
	[RestService("/PresupuestoItem/read","get")]
	[RestService("/PresupuestoItem/read/{Id}","get")]
	[RestService("/PresupuestoItem/update/{Id}","put")]
	[RestService("/PresupuestoItem/destroy/{Id}","delete")]
	public partial class PresupuestoItem
	{
	}
}