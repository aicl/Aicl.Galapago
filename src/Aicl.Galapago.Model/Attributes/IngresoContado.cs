using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/IngresoContado/create","post")]
	[RestService("/IngresoContado/read","get")]
	[RestService("/IngresoContado/read/{Id}","get")]
	[RestService("/IngresoContado/update/{Id}","put")]
	[RestService("/IngresoContado/destroy/{Id}","delete")]
	public partial class IngresoContado
	{
	}
}