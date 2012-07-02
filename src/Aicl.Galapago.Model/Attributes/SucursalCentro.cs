using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/SucursalCentro/create","post")]
	[RestService("/SucursalCentro/read","get")]
	[RestService("/SucursalCentro/read/{Id}","get")]
	[RestService("/SucursalCentro/update/{Id}","put")]
	[RestService("/SucursalCentro/destroy/{Id}","delete")]
	public partial class SucursalCentro
	{
	}
}